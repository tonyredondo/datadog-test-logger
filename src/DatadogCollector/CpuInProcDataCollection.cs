using System.Collections.Concurrent;
using System.Diagnostics;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Newtonsoft.Json;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollector.InProcDataCollector;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.InProcDataCollector;

namespace DatadogCollector;

internal class CpuInProcDataCollection : InProcDataCollection
{
    private readonly ConcurrentDictionary<Guid, List<CpuUsagePair>> _currentCpuValues = new();
    private readonly ConcurrentDictionary<Guid, List<CpuUsagePair>> _finalValues = new();
    private CancellationTokenSource? _cpuUsageCancellationTokenSource;
    private Thread? _cpuCollectorThread;
    private double _lastCpuPercentageValue;
    private double _lastSystemCpuPercentageValue;

    public void Initialize(IDataCollectionSink dataCollectionSink)
    {
        _cpuUsageCancellationTokenSource = new CancellationTokenSource();
        _cpuCollectorThread = new Thread(state => StartCollectingCpuUsageForProcess((CpuInProcDataCollection?)state))
        {
            IsBackground = true,
            Name = "CpuCollectorThread"
        };
        _cpuCollectorThread.Start(this);
    }

    public void TestSessionStart(TestSessionStartArgs testSessionStartArgs)
    {
        // Let's ensure we start collecting cpu percentage values before everything else.
        var retries = 4;
        while (Interlocked.CompareExchange(ref _lastCpuPercentageValue, 0, 0) == 0 && retries-- > 0)
        {
            Thread.Sleep(500);
        }
    }

    public void TestCaseStart(TestCaseStartArgs testCaseStartArgs)
    {
        if (testCaseStartArgs?.TestCase?.Id is { } id)
        {
            _currentCpuValues.GetOrAdd(id, new List<CpuUsagePair> { new(_lastCpuPercentageValue, GetTotalCpuUsage()) });
        }
    }

    public void TestCaseEnd(TestCaseEndArgs testCaseEndArgs)
    {
        if (testCaseEndArgs?.DataCollectionContext?.TestCase is { } testCase)
        {
            if (_currentCpuValues.TryRemove(testCase.Id, out var values) &&
                _finalValues.TryAdd(testCase.Id, values))
            {
                lock (values)
                {
                    values.Add(new(_lastCpuPercentageValue, GetTotalCpuUsage()));
                }
            }
        }
    }

    public void TestSessionEnd(TestSessionEndArgs testSessionEndArgs)
    {
        try
        {
            // Cancel cpu collection.
            _cpuUsageCancellationTokenSource?.Cancel();
            
            // Move current cpu values to the final values.
            foreach (var item in _currentCpuValues)
            {
                _finalValues.TryAdd(item.Key, item.Value);
            }
            
            _currentCpuValues.Clear();
        }
        catch
        {
            // .
        }
        
        try
        {
            // Try to write the json file.
            using var sw = new StreamWriter($"cpu_values.json", false);
            var serializer = JsonSerializer.CreateDefault();
            serializer.Serialize(sw, _finalValues);
        }
        catch
        {
            // .
        }
    }
    
    private static void StartCollectingCpuUsageForProcess(CpuInProcDataCollection? dataCollection)
    {
        if (dataCollection?._cpuUsageCancellationTokenSource is { Token: { } token })
        {
            _ = TotalCpuUsage.GetUsage();

            // Get current Process
            var process = Process.GetCurrentProcess();
            // Create a high precision clock
            var watch = new Stopwatch();

            while (!token.IsCancellationRequested)
            {
                try
                {
                    // Collect values
                    watch.Restart();
                    var startCpuUsage = process.TotalProcessorTime;
                    Thread.Sleep(500);
                    var ticksPassed = watch.Elapsed.Ticks;
                    var endCpuUsage = process.TotalProcessorTime;

                    // Calculation
                    var cpuUsedTicks = (endCpuUsage - startCpuUsage).Ticks;
                    var cpuUsagePercent = ((double)cpuUsedTicks / (double)ticksPassed) * 100;
                    cpuUsagePercent = cpuUsagePercent / Environment.ProcessorCount;
                    var result = Math.Round(cpuUsagePercent, 1, MidpointRounding.AwayFromZero);

                    // If after the calculation we have a value > 100 then we assume the cpu/timer is having a bad time
                    // due to usage, so we clip the value to a 100%
                    if (result > 100)
                    {
                        result = 100;
                    }

                    // Add value to current executing tests
                    dataCollection._lastCpuPercentageValue = result;
                    foreach(var values in dataCollection._currentCpuValues.Values)
                    {
                        if (token.IsCancellationRequested)
                        {
                            break;
                        }

                        lock (values)
                        {
                            values.Add(new CpuUsagePair(dataCollection._lastCpuPercentageValue, GetTotalCpuUsage()));
                        }
                    }
                }
                catch
                {
                    Thread.Sleep(100);
                }
            }
        }
    }

    private static double GetTotalCpuUsage()
    {
        return Math.Round(TotalCpuUsage.GetUsage(), 1, MidpointRounding.AwayFromZero);
    }

    private readonly struct CpuUsagePair
    {
        [JsonProperty("process")]
        public readonly double Process;
        [JsonProperty("system")]
        public readonly double System;

        public CpuUsagePair(double process, double system)
        {
            Process = process;
            System = system;
        }
    }
}

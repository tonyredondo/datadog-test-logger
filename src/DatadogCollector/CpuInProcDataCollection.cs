using System.Collections.Concurrent;
using System.Diagnostics;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Newtonsoft.Json;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollector.InProcDataCollector;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.InProcDataCollector;

namespace DatadogCollector;

internal class CpuInProcDataCollection : InProcDataCollection
{
    private readonly ConcurrentDictionary<Guid, List<double>> _currentCpuValues = new();
    private readonly ConcurrentDictionary<Guid, List<double>> _finalValues = new();
    private CancellationTokenSource? _cpuUsageCancellationTokenSource;
    private Thread? _cpuCollectorThread;
    private double _lastCpuPercentageValue;

    public void Initialize(IDataCollectionSink dataCollectionSink)
    {
        _cpuUsageCancellationTokenSource = new CancellationTokenSource();
        _cpuCollectorThread = new Thread(state => StartCollectingCpuUsageForProcess((CpuInProcDataCollection?)state));
        _cpuCollectorThread.IsBackground = true;
        _cpuCollectorThread.Start(this);

        // Let's ensure we start collecting cpu percentage values before everything else.
        var retries = 4;
        while (Interlocked.CompareExchange(ref _lastCpuPercentageValue, 0, 0) == 0 && retries-- > 0)
        {
            Thread.Sleep(250);
        }
    }

    public void TestSessionStart(TestSessionStartArgs testSessionStartArgs)
    {
    }

    public void TestCaseStart(TestCaseStartArgs testCaseStartArgs)
    {
        if (testCaseStartArgs?.TestCase?.Id is { } id)
        {
            var values = _currentCpuValues.GetOrAdd(id, new List<double>());
            lock (values)
            {
                values.Add(_lastCpuPercentageValue);
            }
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
                    values.Add(_lastCpuPercentageValue);
                }
            }
        }
    }

    public void TestSessionEnd(TestSessionEndArgs testSessionEndArgs)
    {
        try
        {
            // Cancel collection and current values.
            _cpuUsageCancellationTokenSource?.Cancel();
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
        Process? process = null;
        if (dataCollection is null)
        {
            return;
        }

        if (dataCollection._cpuUsageCancellationTokenSource is { Token: { } token })
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    process ??= Process.GetCurrentProcess();
                    var startTime = DateTime.UtcNow;
                    var startCpuUsage = process.TotalProcessorTime;
                    Thread.Sleep(100);
                    var endTime = DateTime.UtcNow;
                    var endCpuUsage = process.TotalProcessorTime;
                    var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
                    var totalMsPassed = (endTime - startTime).TotalMilliseconds;
                    var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
                    var result = cpuUsageTotal * 100;
                    Interlocked.Exchange(ref dataCollection._lastCpuPercentageValue, result);
                    foreach(var values in dataCollection._currentCpuValues.Values)
                    {
                        if (token.IsCancellationRequested)
                        {
                            break;
                        }

                        lock (values)
                        {
                            values.Add(dataCollection._lastCpuPercentageValue);
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
}
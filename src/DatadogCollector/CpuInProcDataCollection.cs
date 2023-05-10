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
    private readonly Process _process = Process.GetCurrentProcess();
    private CancellationTokenSource? _cpuUsageCancellationTokenSource;
    private double _lastCpuPercentageValue;

    public void Initialize(IDataCollectionSink dataCollectionSink)
    {
        _cpuUsageCancellationTokenSource = new CancellationTokenSource();
        Task.Run(() => StartCollectingCpuUsageForProcess(_process, _cpuUsageCancellationTokenSource.Token));
        
        // Let's ensure we start collecting cpu percentage values before everything else.
        while (_lastCpuPercentageValue == 0)
        {
            Thread.Sleep(250);
        }
    }

    public void TestSessionStart(TestSessionStartArgs testSessionStartArgs)
    {
    }

    public void TestCaseStart(TestCaseStartArgs testCaseStartArgs)
    {
        if (testCaseStartArgs.TestCase?.Id is { } id)
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
        if (testCaseEndArgs.DataCollectionContext?.TestCase is { } testCase)
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
        _cpuUsageCancellationTokenSource?.Cancel();
        _currentCpuValues.Clear();
        using var sw = new StreamWriter($"cpu_values.json", false);
        var serializer = JsonSerializer.CreateDefault();
        serializer.Serialize(sw, _finalValues);
        _finalValues.Clear();
    }
    
    private async Task StartCollectingCpuUsageForProcess(Process process, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var startTime = DateTime.UtcNow;
            var startCpuUsage = process.TotalProcessorTime;
            await Task.Delay(250, cancellationToken).ConfigureAwait(false);
            var endTime = DateTime.UtcNow;
            var endCpuUsage = process.TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
            var result = cpuUsageTotal * 100;
            _lastCpuPercentageValue = result;
            foreach(var values in _currentCpuValues.Values)
            {
                lock (values)
                {
                    values.Add(_lastCpuPercentageValue);
                }
            }
        }
    }
}
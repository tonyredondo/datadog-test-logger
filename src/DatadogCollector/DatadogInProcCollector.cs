// <copyright file="DatadogInProcCollector.cs" company="PlaceholderCompany">
// Copyright (c) Tony Redondo. All rights reserved.
// </copyright>

using DatadogTestLogger.Vendors.Datadog.Trace.Coverage.Collector;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollector.InProcDataCollector;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.InProcDataCollector;

namespace DatadogCollector;

/// <summary>
/// Datadog inproc coverage collector
/// </summary>
[DataCollectorTypeUri("InProcDataCollector://Datadog/collector/1.0")]
[DataCollectorFriendlyName("datadoginproc")]
public class DatadogInProcCollector : InProcDataCollection
{
    private readonly CpuInProcDataCollection? _cpuInProcDataCollection;
    private readonly InProcCoverageCollector? _coverageCollector;

    public DatadogInProcCollector()
    {
        if (Configuration.Instance.CpuUsageEnabled)
        {
            _cpuInProcDataCollection = new();
        }

        if (Configuration.Instance.CoverageEnabled)
        {
            _coverageCollector = new();
        }
    }
    
    public void Initialize(IDataCollectionSink dataCollectionSink)
    {
        _cpuInProcDataCollection?.Initialize(dataCollectionSink);
        _coverageCollector?.Initialize(dataCollectionSink);
    }

    public void TestSessionStart(TestSessionStartArgs testSessionStartArgs)
    {
        _cpuInProcDataCollection?.TestSessionStart(testSessionStartArgs);
        _coverageCollector?.TestSessionStart(testSessionStartArgs);
    }

    public void TestCaseStart(TestCaseStartArgs testCaseStartArgs)
    {
        _cpuInProcDataCollection?.TestCaseStart(testCaseStartArgs);
        _coverageCollector?.TestCaseStart(testCaseStartArgs);
    }

    public void TestCaseEnd(TestCaseEndArgs testCaseEndArgs)
    {
        _cpuInProcDataCollection?.TestCaseEnd(testCaseEndArgs);
        _coverageCollector?.TestCaseEnd(testCaseEndArgs);
    }

    public void TestSessionEnd(TestSessionEndArgs testSessionEndArgs)
    {
        _cpuInProcDataCollection?.TestSessionEnd(testSessionEndArgs);
        _coverageCollector?.TestSessionEnd(testSessionEndArgs);
    }
}
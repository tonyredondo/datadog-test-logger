// <copyright file="DatadogTestCoverageCollector.cs" company="PlaceholderCompany">
// Copyright (c) Tony Redondo. All rights reserved.
// </copyright>

using System.Xml;
using DatadogTestLogger.Vendors.Datadog.Trace.Coverage.Collector;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace DatadogTestLogger;

/// <summary>
/// Datadog coverage collector
/// </summary>
[DataCollectorTypeUri("datacollector://Datadog/CoverageCollector/1.0")]
[DataCollectorFriendlyName("DatadogCoverage")]
public class DatadogTestCoverageCollector : DataCollector
{
    private readonly CoverageCollector _coverageCollector;

    public DatadogTestCoverageCollector()
    {
        _coverageCollector = new CoverageCollector();
    }
    
    public override void Initialize(XmlElement? configurationElement, DataCollectionEvents events, DataCollectionSink dataSink,
        DataCollectionLogger logger, DataCollectionEnvironmentContext? environmentContext)
    {
        _coverageCollector.Initialize(configurationElement, events, dataSink, logger, environmentContext);
    }

    protected override void Dispose(bool disposing)
    {
        _coverageCollector.Dispose();
        base.Dispose(disposing);
    }
}
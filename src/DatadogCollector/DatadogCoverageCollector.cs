// <copyright file="DatadogCoverageCollector.cs" company="PlaceholderCompany">
// Copyright (c) Tony Redondo. All rights reserved.
// </copyright>

using System.Xml;
using DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.Coverage.Collector;
using DatadogTestLogger.Vendors.Datadog.Trace.Util;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace DatadogCollector;

/// <summary>
/// Datadog coverage collector
/// </summary>
[DataCollectorTypeUri("datacollector://Datadog/collector/1.0")]
[DataCollectorFriendlyName("datadog")]
public class DatadogCoverageCollector : DataCollector
{
    private readonly CoverageCollector _coverageCollector;

    public DatadogCoverageCollector()
    {
        EnvironmentHelpers.SetEnvironmentVariable(ConfigurationKeys.DebugEnabled, "1");
        EnvironmentHelpers.SetEnvironmentVariable("DD_DOTNET_TRACER_HOME", "");
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
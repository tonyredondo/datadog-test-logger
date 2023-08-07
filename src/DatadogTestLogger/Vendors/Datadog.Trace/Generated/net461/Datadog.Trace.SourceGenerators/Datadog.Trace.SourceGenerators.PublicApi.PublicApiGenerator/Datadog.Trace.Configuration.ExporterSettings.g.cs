//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NETFRAMEWORK
// <auto-generated/>
#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
partial class ExporterSettings
{

#pragma warning disable SA1624 // Documentation summary should begin with "Gets" - the documentation is primarily for public property
        /// <summary>
        /// Gets or sets the windows pipe name where the Tracer can connect to the Agent.
        /// Default is <c>null</c>.
        /// </summary>
        /// <seealso cref="ConfigurationKeys.TracesPipeName"/>
    [Datadog.Trace.SourceGenerators.PublicApi]
    public string? TracesPipeName
    {
        get
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)46);
            return TracesPipeNameInternal;
        }
        set
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)47);
            TracesPipeNameInternal = value;
        }
    }

        /// <summary>
        /// Gets or sets the timeout in milliseconds for the windows named pipe requests.
        /// Default is <c>100</c>.
        /// </summary>
        /// <seealso cref="ConfigurationKeys.TracesPipeTimeoutMs"/>
    [Datadog.Trace.SourceGenerators.PublicApi]
    public int TracesPipeTimeoutMs
    {
        get
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)48);
            return TracesPipeTimeoutMsInternal;
        }
        set
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)49);
            TracesPipeTimeoutMsInternal = value;
        }
    }

        /// <summary>
        /// Gets or sets the windows pipe name where the Tracer can send stats.
        /// Default is <c>null</c>.
        /// </summary>
        /// <seealso cref="ConfigurationKeys.MetricsPipeName"/>
    [Datadog.Trace.SourceGenerators.PublicApi]
    public string? MetricsPipeName
    {
        get
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)38);
            return MetricsPipeNameInternal;
        }
        set
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)39);
            MetricsPipeNameInternal = value;
        }
    }

        /// <summary>
        /// Gets or sets the unix domain socket path where the Tracer can connect to the Agent.
        /// This parameter is deprecated and shall be removed. Consider using AgentUri instead
        /// </summary>
    [Datadog.Trace.SourceGenerators.PublicApi]
    public string? TracesUnixDomainSocketPath
    {
        get
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)50);
            return TracesUnixDomainSocketPathInternal;
        }
        set
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)51);
            TracesUnixDomainSocketPathInternal = value;
        }
    }

        /// <summary>
        /// Gets or sets the unix domain socket path where the Tracer can send stats.
        /// </summary>
        /// <seealso cref="ConfigurationKeys.MetricsUnixDomainSocketPath"/>
    [Datadog.Trace.SourceGenerators.PublicApi]
    public string? MetricsUnixDomainSocketPath
    {
        get
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)40);
            return MetricsUnixDomainSocketPathInternal;
        }
        set
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)41);
            MetricsUnixDomainSocketPathInternal = value;
        }
    }

        /// <summary>
        /// Gets or sets the port where the DogStatsd server is listening for connections.
        /// Default is <c>8125</c>.
        /// </summary>
        /// <seealso cref="ConfigurationKeys.DogStatsdPort"/>
    [Datadog.Trace.SourceGenerators.PublicApi]
    public int DogStatsdPort
    {
        get
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)36);
            return DogStatsdPortInternal;
        }
        set
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)37);
            DogStatsdPortInternal = value;
        }
    }

        /// <summary>
        /// Gets or sets a value indicating whether partial flush is enabled
        /// </summary>
    [Datadog.Trace.SourceGenerators.PublicApi]
    public bool PartialFlushEnabled
    {
        get
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)42);
            return PartialFlushEnabledInternal;
        }
        set
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)43);
            PartialFlushEnabledInternal = value;
        }
    }
}
#endif
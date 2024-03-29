//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if !NETFRAMEWORK && !NETCOREAPP3_1_OR_GREATER
// <auto-generated />

using DatadogTestLogger.Vendors.Datadog.Trace.Configuration.Telemetry;

#nullable enable
#pragma warning disable CS0618 // Type is obsolete

namespace DatadogTestLogger.Vendors.Datadog.Trace.Configuration;

internal partial class ExporterSettingsSnapshot : SettingsSnapshotBase
{
    internal ExporterSettingsSnapshot(Datadog.Trace.Configuration.ExporterSettings settings)
    {
        TracesPipeNameInternal = settings.TracesPipeNameInternal;
        TracesPipeTimeoutMsInternal = settings.TracesPipeTimeoutMsInternal;
        MetricsPipeNameInternal = settings.MetricsPipeNameInternal;
        TracesUnixDomainSocketPathInternal = settings.TracesUnixDomainSocketPathInternal;
        MetricsUnixDomainSocketPathInternal = settings.MetricsUnixDomainSocketPathInternal;
        DogStatsdPortInternal = settings.DogStatsdPortInternal;
        PartialFlushEnabledInternal = settings.PartialFlushEnabledInternal;
        AdditionalInitialization(settings);
    }

    private string? TracesPipeNameInternal { get; }
    private int TracesPipeTimeoutMsInternal { get; }
    private string? MetricsPipeNameInternal { get; }
    private string? TracesUnixDomainSocketPathInternal { get; }
    private string? MetricsUnixDomainSocketPathInternal { get; }
    private int DogStatsdPortInternal { get; }
    private bool PartialFlushEnabledInternal { get; }

    internal void RecordChanges(Datadog.Trace.Configuration.ExporterSettings settings, IConfigurationTelemetry telemetry)
    {
        RecordIfChanged(telemetry, "DD_TRACE_PIPE_NAME", TracesPipeNameInternal, settings.TracesPipeNameInternal);
        RecordIfChanged(telemetry, "DD_TRACE_PIPE_TIMEOUT_MS", TracesPipeTimeoutMsInternal, settings.TracesPipeTimeoutMsInternal);
        RecordIfChanged(telemetry, "DD_DOGSTATSD_PIPE_NAME", MetricsPipeNameInternal, settings.MetricsPipeNameInternal);
        RecordIfChanged(telemetry, "DD_APM_RECEIVER_SOCKET", TracesUnixDomainSocketPathInternal, settings.TracesUnixDomainSocketPathInternal);
        RecordIfChanged(telemetry, "DD_DOGSTATSD_SOCKET", MetricsUnixDomainSocketPathInternal, settings.MetricsUnixDomainSocketPathInternal);
        RecordIfChanged(telemetry, "DD_DOGSTATSD_PORT", DogStatsdPortInternal, settings.DogStatsdPortInternal);
        RecordIfChanged(telemetry, "DD_TRACE_PARTIAL_FLUSH_ENABLED", PartialFlushEnabledInternal, settings.PartialFlushEnabledInternal);
        RecordAdditionalChanges(settings, telemetry);
    }

    partial void AdditionalInitialization(Datadog.Trace.Configuration.ExporterSettings settings);
    partial void RecordAdditionalChanges(Datadog.Trace.Configuration.ExporterSettings settings, IConfigurationTelemetry telemetry);
}
#endif
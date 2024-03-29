//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NETCOREAPP3_1_OR_GREATER && !NET6_0_OR_GREATER
// <auto-generated />

using DatadogTestLogger.Vendors.Datadog.Trace.Configuration.Telemetry;

#nullable enable
#pragma warning disable CS0618 // Type is obsolete

namespace DatadogTestLogger.Vendors.Datadog.Trace.Configuration;

internal partial class TracerSettingsSnapshot : SettingsSnapshotBase
{
    internal TracerSettingsSnapshot(Datadog.Trace.Configuration.TracerSettings settings)
    {
        EnvironmentInternal = settings.EnvironmentInternal;
        ServiceNameInternal = settings.ServiceNameInternal;
        ServiceVersionInternal = settings.ServiceVersionInternal;
        TraceEnabledInternal = settings.TraceEnabledInternal;
        DisabledIntegrationNamesInternal = GetHashSet(settings.DisabledIntegrationNamesInternal);
        AnalyticsEnabledInternal = settings.AnalyticsEnabledInternal;
        MaxTracesSubmittedPerSecondInternal = settings.MaxTracesSubmittedPerSecondInternal;
        CustomSamplingRulesInternal = settings.CustomSamplingRulesInternal;
        GlobalSamplingRateInternal = settings.GlobalSamplingRateInternal;
        GlobalTagsInternal = GetDictionary(settings.GlobalTagsInternal);
        HeaderTagsInternal = GetDictionary(settings.HeaderTagsInternal);
        GrpcTagsInternal = GetDictionary(settings.GrpcTagsInternal);
        TracerMetricsEnabledInternal = settings.TracerMetricsEnabledInternal;
        StatsComputationEnabledInternal = settings.StatsComputationEnabledInternal;
        KafkaCreateConsumerScopeEnabledInternal = settings.KafkaCreateConsumerScopeEnabledInternal;
        StartupDiagnosticLogEnabledInternal = settings.StartupDiagnosticLogEnabledInternal;
        AdditionalInitialization(settings);
    }

    private string? EnvironmentInternal { get; }
    private string? ServiceNameInternal { get; }
    private string? ServiceVersionInternal { get; }
    private bool TraceEnabledInternal { get; }
    private System.Collections.Generic.HashSet<string>? DisabledIntegrationNamesInternal { get; }
    private bool AnalyticsEnabledInternal { get; }
    private int MaxTracesSubmittedPerSecondInternal { get; }
    private string? CustomSamplingRulesInternal { get; }
    private double? GlobalSamplingRateInternal { get; }
    private System.Collections.Generic.IDictionary<string, string>? GlobalTagsInternal { get; }
    private System.Collections.Generic.IDictionary<string, string>? HeaderTagsInternal { get; }
    private System.Collections.Generic.IDictionary<string, string>? GrpcTagsInternal { get; }
    private bool TracerMetricsEnabledInternal { get; }
    private bool StatsComputationEnabledInternal { get; }
    private bool KafkaCreateConsumerScopeEnabledInternal { get; }
    private bool StartupDiagnosticLogEnabledInternal { get; }

    internal void RecordChanges(Datadog.Trace.Configuration.TracerSettings settings, IConfigurationTelemetry telemetry)
    {
        RecordIfChanged(telemetry, "DD_ENV", EnvironmentInternal, settings.EnvironmentInternal);
        RecordIfChanged(telemetry, "DD_SERVICE", ServiceNameInternal, settings.ServiceNameInternal);
        RecordIfChanged(telemetry, "DD_VERSION", ServiceVersionInternal, settings.ServiceVersionInternal);
        RecordIfChanged(telemetry, "DD_TRACE_ENABLED", TraceEnabledInternal, settings.TraceEnabledInternal);
        RecordIfChanged(telemetry, "DD_DISABLED_INTEGRATIONS", DisabledIntegrationNamesInternal, GetHashSet(settings.DisabledIntegrationNamesInternal));
        RecordIfChanged(telemetry, "DD_TRACE_ANALYTICS_ENABLED", AnalyticsEnabledInternal, settings.AnalyticsEnabledInternal);
        RecordIfChanged(telemetry, "DD_TRACE_RATE_LIMIT", MaxTracesSubmittedPerSecondInternal, settings.MaxTracesSubmittedPerSecondInternal);
        RecordIfChanged(telemetry, "DD_TRACE_SAMPLING_RULES", CustomSamplingRulesInternal, settings.CustomSamplingRulesInternal);
        RecordIfChanged(telemetry, "DD_TRACE_SAMPLE_RATE", GlobalSamplingRateInternal, settings.GlobalSamplingRateInternal);
        RecordIfChanged(telemetry, "DD_TAGS", GlobalTagsInternal, GetDictionary(settings.GlobalTagsInternal));
        RecordIfChanged(telemetry, "DD_TRACE_HEADER_TAGS", HeaderTagsInternal, GetDictionary(settings.HeaderTagsInternal));
        RecordIfChanged(telemetry, "DD_TRACE_GRPC_TAGS", GrpcTagsInternal, GetDictionary(settings.GrpcTagsInternal));
        RecordIfChanged(telemetry, "DD_TRACE_METRICS_ENABLED", TracerMetricsEnabledInternal, settings.TracerMetricsEnabledInternal);
        RecordIfChanged(telemetry, "DD_TRACE_STATS_COMPUTATION_ENABLED", StatsComputationEnabledInternal, settings.StatsComputationEnabledInternal);
        RecordIfChanged(telemetry, "DD_TRACE_KAFKA_CREATE_CONSUMER_SCOPE_ENABLED", KafkaCreateConsumerScopeEnabledInternal, settings.KafkaCreateConsumerScopeEnabledInternal);
        RecordIfChanged(telemetry, "DD_TRACE_STARTUP_LOGS", StartupDiagnosticLogEnabledInternal, settings.StartupDiagnosticLogEnabledInternal);
        RecordAdditionalChanges(settings, telemetry);
    }

    partial void AdditionalInitialization(Datadog.Trace.Configuration.TracerSettings settings);
    partial void RecordAdditionalChanges(Datadog.Trace.Configuration.TracerSettings settings, IConfigurationTelemetry telemetry);
}
#endif
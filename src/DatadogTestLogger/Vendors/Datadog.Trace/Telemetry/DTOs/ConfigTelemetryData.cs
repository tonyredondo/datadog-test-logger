//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="ConfigTelemetryData.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.Telemetry
{
    internal static class ConfigTelemetryData
    {
        public const string Platform = "platform";
        public const string Enabled = "enabled";
        public const string AgentUrl = "agent_url";
        public const string AgentTraceTransport = "agent_transport";
        public const string Debug = "debug";
        public const string NativeTracerVersion = "native_tracer_version";
        public const string ManagedTracerTfm = "managed_tracer_framework";
        public const string AnalyticsEnabled = "analytics_enabled";
        public const string SampleRate = "sample_rate";
        public const string SamplingRules = "sampling_rules";
        public const string LogInjectionEnabled = "logInjection_enabled";
        public const string RuntimeMetricsEnabled = "runtimemetrics_enabled";
        public const string RoutetemplateResourcenamesEnabled = "routetemplate_resourcenames_enabled";
        public const string RoutetemplateExpansionEnabled = "routetemplate_expansion_enabled";
        public const string PartialflushEnabled = "partialflush_enabled";
        public const string PartialflushMinspans = "partialflush_minspans";
        public const string TracerInstanceCount = "tracer_instance_count";
        public const string AasConfigurationError = "aas_configuration_error";
        public const string SecurityEnabled = "security_enabled";
        public const string IastEnabled = "iast_enabled";
        public const string FullTrustAppDomain = "environment_fulltrust_appdomain";
        public const string TraceMethods = "trace_methods";
        public const string StatsComputationEnabled = "stats_computation_enabled";
        public const string WcfObfuscationEnabled = "wcf_obfuscation_enabled";
        public const string DataStreamsMonitoringEnabled = "data_streams_enabled";
        public const string SpanSamplingRules = "span_sampling_rules";
        public const string PropagationStyleExtract = "DD_TRACE_PROPAGATION_STYLE_EXTRACT";
        public const string PropagationStyleInject = "DD_TRACE_PROPAGATION_STYLE_INJECT";

        public const string CloudHosting = "cloud_hosting";
        public const string AasSiteExtensionVersion = "aas_siteextensions_version";
        public const string AasAppType = "aas_app_type";
        public const string AasFunctionsRuntimeVersion = "aas_functions_runtime_version";

        public const string OpenTelemetryEnabled = "otel_enabled";

        public const string ProfilerLoaded = "profiler_loaded";
        public const string CodeHotspotsEnabled = "code_hotspots_enabled";

        // We intentionally are using specific values here, not OR_GREATER_THAN
#if NET6_0
        public const string ManagedTracerTfmValue = "net6.0";
#elif NETCOREAPP3_1
        public const string ManagedTracerTfmValue = "netcoreapp3.1";
#elif NETSTANDARD2_0
        public const string ManagedTracerTfmValue = "netstandard2.0";
#elif NETFRAMEWORK
        public const string ManagedTracerTfmValue = "net461";
#elif NETCOREAPP2_1
        public const string ManagedTracerTfmValue = "netcoreapp2.1";
#elif NETCOREAPP2_2
        public const string ManagedTracerTfmValue = "netcoreapp2.2";
#elif NETCOREAPP3_0
        public const string ManagedTracerTfmValue = "netcoreapp3.0";
#elif NET5_0
        public const string ManagedTracerTfmValue = "net5.0";
#elif NET7_0
        public const string ManagedTracerTfmValue = "net7.0";
#else
#error Unexpected TFM
#endif
    }
}

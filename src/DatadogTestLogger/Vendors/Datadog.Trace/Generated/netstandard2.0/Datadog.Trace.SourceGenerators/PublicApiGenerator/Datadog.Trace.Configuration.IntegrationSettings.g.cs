//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if !NETFRAMEWORK && !NETCOREAPP3_1_OR_GREATER
// <copyright company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
// <auto-generated/>

#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
partial class IntegrationSettings
{

        /// <summary>
        /// Gets the name of the integration. Used to retrieve integration-specific settings.
        /// </summary>
    [Datadog.Trace.SourceGenerators.PublicApi]
    public string IntegrationName
    {
        get
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)74);
            return IntegrationNameInternal;
        }
    }

#pragma warning disable SA1624 // Documentation summary should begin with "Gets" - the documentation is primarily for public property
        /// <summary>
        /// Gets or sets a value indicating whether
        /// this integration is enabled.
        /// </summary>
    [Datadog.Trace.SourceGenerators.PublicApi]
    public bool? Enabled
    {
        get
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)72);
            return EnabledInternal;
        }
        set
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)73);
            EnabledInternal = value;
        }
    }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// Analytics are enabled for this integration.
        /// </summary>
    [Datadog.Trace.SourceGenerators.PublicApi]
    public bool? AnalyticsEnabled
    {
        get
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)68);
            return AnalyticsEnabledInternal;
        }
        set
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)69);
            AnalyticsEnabledInternal = value;
        }
    }

        /// <summary>
        /// Gets or sets a value between 0 and 1 (inclusive)
        /// that determines the sampling rate for this integration.
        /// </summary>
    [Datadog.Trace.SourceGenerators.PublicApi]
    public double AnalyticsSampleRate
    {
        get
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)70);
            return AnalyticsSampleRateInternal;
        }
        set
        {
            Datadog.Trace.Telemetry.TelemetryFactory.Metrics.Record(
                (Datadog.Trace.Telemetry.Metrics.PublicApiUsage)71);
            AnalyticsSampleRateInternal = value;
        }
    }
}
#endif
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NETFRAMEWORK
// <copyright company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
// <auto-generated/>

#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.Telemetry.Metrics;
internal static partial class DistributionCIVisibilityExtensions
{
    /// <summary>
    /// The number of separate metrics in the <see cref="Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility" /> metric.
    /// </summary>
    public const int Length = 13;

    /// <summary>
    /// Gets the metric name for the provided metric
    /// </summary>
    /// <param name="metric">The metric to get the name for</param>
    /// <returns>The datadog metric name</returns>
    public static string GetName(this Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility metric)
        => metric switch
        {
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.EndpointPayloadBytes => "endpoint_payload.bytes",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.EndpointPayloadRequestsMs => "endpoint_payload.requests_ms",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.EndpointPayloadEventsCount => "endpoint_payload.events_count",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.EndpointEventsSerializationMs => "endpoint_payload.events_serialization_ms",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.GitCommandMs => "git.command_ms",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.GitRequestsSearchCommitsMs => "git_requests.search_commits_ms",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.GitRequestsObjectsPackMs => "git_requests.objects_pack_ms",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.GitRequestsObjectsPackBytes => "git_requests.objects_pack_bytes",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.GitRequestsObjectsPackFiles => "git_requests.objects_pack_files",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.GitRequestsSettingsMs => "git_requests.settings_ms",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.ITRSkippableTestsRequestMs => "itr_skippable_tests.request_ms",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.ITRSkippableTestsResponseBytes => "itr_skippable_tests.response_bytes",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.CodeCoverageFiles => "code_coverage.files",
            _ => null!,
        };

    /// <summary>
    /// Gets whether the metric is a "common" metric, used by all tracers
    /// </summary>
    /// <param name="metric">The metric to check</param>
    /// <returns>True if the metric is a "common" metric, used by all languages</returns>
    public static bool IsCommon(this Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility metric)
        => metric switch
        {
            _ => true,
        };

    /// <summary>
    /// Gets the custom namespace for the provided metric
    /// </summary>
    /// <param name="metric">The metric to get the name for</param>
    /// <returns>The datadog metric name</returns>
    public static string? GetNamespace(this Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility metric)
        => metric switch
        {
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.EndpointPayloadBytes => "civisibility",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.EndpointPayloadRequestsMs => "civisibility",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.EndpointPayloadEventsCount => "civisibility",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.EndpointEventsSerializationMs => "civisibility",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.GitCommandMs => "civisibility",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.GitRequestsSearchCommitsMs => "civisibility",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.GitRequestsObjectsPackMs => "civisibility",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.GitRequestsObjectsPackBytes => "civisibility",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.GitRequestsObjectsPackFiles => "civisibility",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.GitRequestsSettingsMs => "civisibility",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.ITRSkippableTestsRequestMs => "civisibility",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.ITRSkippableTestsResponseBytes => "civisibility",
            Datadog.Trace.Telemetry.Metrics.DistributionCIVisibility.CodeCoverageFiles => "civisibility",
            _ => null,
        };
}
#endif
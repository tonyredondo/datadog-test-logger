//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NET6_0_OR_GREATER
// <copyright company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
// <auto-generated/>

#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.Telemetry.Metrics;

/// <summary>
/// Extension methods for <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources" />
/// </summary>
internal static partial class IastInstrumentedSourcesExtensions
{
    /// <summary>
    /// The number of members in the enum.
    /// This is a non-distinct count of defined names.
    /// </summary>
    public const int Length = 11;

    /// <summary>
    /// Returns the string representation of the <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources"/> value.
    /// If the attribute is decorated with a <c>[Description]</c> attribute, then
    /// uses the provided value. Otherwise uses the name of the member, equivalent to
    /// calling <c>ToString()</c> on <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to retrieve the string value for</param>
    /// <returns>The string representation of the value</returns>
    public static string ToStringFast(this Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources value)
        => value switch
        {
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestBody => "source_type:http.request.body",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestPath => "source_type:http.request.path",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestParameterName => "source_type:http.request.parameter.name",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestParameterValue => "source_type:http.request.parameter",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RoutedParameterValue => "source_type:http.request.path.parameter",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestHeaderValue => "source_type:http.request.header",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestHeaderName => "source_type:http.request.header.name",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestQuery => "source_type:http.request.query",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.CookieName => "source_type:http.cookie.name",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.CookieValue => "source_type:http.cookie.value",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.MatrixParameter => "source_type:http.request.matrix.parameter",
            _ => value.ToString(),
        };

    /// <summary>
    /// Retrieves an array of the values of the members defined in
    /// <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources" />.
    /// Note that this returns a new array with every invocation, so
    /// should be cached if appropriate.
    /// </summary>
    /// <returns>An array of the values defined in <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources" /></returns>
    public static Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources[] GetValues()
        => new []
        {
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestBody,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestPath,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestParameterName,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestParameterValue,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RoutedParameterValue,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestHeaderValue,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestHeaderName,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestQuery,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.CookieName,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.CookieValue,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.MatrixParameter,
        };

    /// <summary>
    /// Retrieves an array of the names of the members defined in
    /// <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources" />.
    /// Note that this returns a new array with every invocation, so
    /// should be cached if appropriate.
    /// Ignores <c>[Description]</c> definitions.
    /// </summary>
    /// <returns>An array of the names of the members defined in <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources" /></returns>
    public static string[] GetNames()
        => new []
        {
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestBody),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestPath),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestParameterName),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestParameterValue),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RoutedParameterValue),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestHeaderValue),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestHeaderName),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.RequestQuery),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.CookieName),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.CookieValue),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources.MatrixParameter),
        };

    /// <summary>
    /// Retrieves an array of the names of the members defined in
    /// <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources" />.
    /// Note that this returns a new array with every invocation, so
    /// should be cached if appropriate.
    /// Uses <c>[Description]</c> definition if available, otherwise uses the name of the property
    /// </summary>
    /// <returns>An array of the names of the members defined in <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSources" /></returns>
    public static string[] GetDescriptions()
        => new []
        {
            "source_type:http.request.body",
            "source_type:http.request.path",
            "source_type:http.request.parameter.name",
            "source_type:http.request.parameter",
            "source_type:http.request.path.parameter",
            "source_type:http.request.header",
            "source_type:http.request.header.name",
            "source_type:http.request.query",
            "source_type:http.cookie.name",
            "source_type:http.cookie.value",
            "source_type:http.request.matrix.parameter",
        };
}
#endif
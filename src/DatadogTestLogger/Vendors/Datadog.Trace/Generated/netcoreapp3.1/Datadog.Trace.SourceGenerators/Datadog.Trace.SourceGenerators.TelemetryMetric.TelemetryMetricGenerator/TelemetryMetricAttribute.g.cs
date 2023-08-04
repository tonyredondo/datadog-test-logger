//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NETCOREAPP3_1_OR_GREATER && !NET6_0_OR_GREATER
// <auto-generated/>
#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.SourceGenerators;

/// <summary>/
/// Used to designate a property as corresponding to the provided
/// <see cref="MetricType"/>. Should only be used in ITags classes.
/// Used for source generation.
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Enum, AllowMultiple = false)]
internal sealed class TelemetryMetricTypeAttribute : System.Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TelemetryMetricTypeAttribute"/> class.
    /// </summary>
    /// <param name="metricType">The type of the metric, e.g. Count, Gauge</param>
    public TelemetryMetricTypeAttribute(string metricType) =>
        this.MetricType = metricType;

    /// <summary>
    /// Gets the type of the metric the enum corresponds to
    /// </summary>
    public string MetricType { get; }
}

/// <summary>
/// Used to describe a specific metric defined as a field
/// inside an enum decorated with <see cref="TelemetryMetricTypeAttribute"/>
/// which has no tags
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
internal class TelemetryMetricAttribute : System.Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TelemetryMetricAttribute"/> class.
    /// </summary>
    /// <param name="metricName">The name of the metric, as reported to Datadog</param>
    /// <param name="isCommon">Is the metric a "common" metric, shared across languages?</param>
    /// <param name="nameSpace">The namespace of the metric, if not the default (Tracer)</param>
    public TelemetryMetricAttribute(string metricName, bool isCommon, string nameSpace)
    {
        MetricName = metricName;
        IsCommon = isCommon;
        NameSpace = nameSpace;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TelemetryMetricAttribute"/> class.
    /// Uses the default namespace
    /// </summary>
    /// <param name="metricName">The name of the metric, as reported to Datadog</param>
    /// <param name="isCommon">Is the metric a "common" metric, shared across languages?</param>
    public TelemetryMetricAttribute(string metricName, bool isCommon)
        : this(metricName, isCommon, null!)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TelemetryMetricAttribute"/> class.
    /// Uses the default namespace and sets <see cref="IsCommon"/> to <c>true</c>
    /// </summary>
    /// <param name="metricName">The name of the metric, as reported to Datadog</param>
    public TelemetryMetricAttribute(string metricName)
        : this(metricName, isCommon: true, null!)
    {
    }

    /// <summary>
    /// Gets the name of the metric, as reported to Datadog
    /// </summary>
    public string MetricName { get; }

    /// <summary>
    /// Gets a value indicating whether the metric a "common" metric, shared across languages?
    /// </summary>
    public bool IsCommon { get; }

    /// <summary>
    /// Gets the namespace of the metric, if not the default (Tracer)
    /// </summary>
    public string? NameSpace { get; }
}

/// <summary>
/// Used to describe a specific metric defined as a field
/// inside an enum decorated with <see cref="TelemetryMetricTypeAttribute"/>
/// which has a single tag
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
internal class TelemetryMetricAttribute<TTag> : System.Attribute
    where TTag : System.Enum
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TelemetryMetricAttribute"/> class.
    /// </summary>
    /// <param name="metricName">The name of the metric, as reported to Datadog</param>
    /// <param name="isCommon">Is the metric a "common" metric, shared across languages?</param>
    /// <param name="nameSpace">The namespace of the metric, if not the default (Tracer)</param>
    public TelemetryMetricAttribute(string metricName, bool isCommon, string nameSpace)
    {
        MetricName = metricName;
        IsCommon = isCommon;
        NameSpace = nameSpace;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TelemetryMetricAttribute"/> class.
    /// Uses the default namespace
    /// </summary>
    /// <param name="metricName">The name of the metric, as reported to Datadog</param>
    /// <param name="isCommon">Is the metric a "common" metric, shared across languages?</param>
    public TelemetryMetricAttribute(string metricName, bool isCommon)
        : this(metricName, isCommon, null!)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TelemetryMetricAttribute"/> class.
    /// Uses the default namespace and sets <see cref="IsCommon"/> to <c>true</c>
    /// </summary>
    /// <param name="metricName">The name of the metric, as reported to Datadog</param>
    public TelemetryMetricAttribute(string metricName)
        : this(metricName, isCommon: true, null!)
    {
    }

    /// <summary>
    /// Gets the name of the metric, as reported to Datadog
    /// </summary>
    public string MetricName { get; }

    /// <summary>
    /// Gets a value indicating whether the metric a "common" metric, shared across languages?
    /// </summary>
    public bool IsCommon { get; }

    /// <summary>
    /// Gets the namespace of the metric, if not the default (Tracer)
    /// </summary>
    public string? NameSpace { get; }
}

/// <summary>
/// Used to describe a specific metric defined as a field
/// inside an enum decorated with <see cref="TelemetryMetricTypeAttribute"/>
/// which has two tags
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
internal class TelemetryMetricAttribute<TTag1, TTag2> : System.Attribute
    where TTag1 : System.Enum
    where TTag2 : System.Enum
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TelemetryMetricAttribute"/> class.
    /// </summary>
    /// <param name="metricName">The name of the metric, as reported to Datadog</param>
    /// <param name="isCommon">Is the metric a "common" metric, shared across languages?</param>
    /// <param name="nameSpace">The namespace of the metric, if not the default (Tracer)</param>
    public TelemetryMetricAttribute(string metricName, bool isCommon, string nameSpace)
    {
        MetricName = metricName;
        IsCommon = isCommon;
        NameSpace = nameSpace;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TelemetryMetricAttribute"/> class.
    /// Uses the default namespace
    /// </summary>
    /// <param name="metricName">The name of the metric, as reported to Datadog</param>
    /// <param name="isCommon">Is the metric a "common" metric, shared across languages?</param>
    public TelemetryMetricAttribute(string metricName, bool isCommon)
        : this(metricName, isCommon, null!)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TelemetryMetricAttribute"/> class.
    /// Uses the default namespace and sets <see cref="IsCommon"/> to <c>true</c>
    /// </summary>
    /// <param name="metricName">The name of the metric, as reported to Datadog</param>
    public TelemetryMetricAttribute(string metricName)
        : this(metricName, isCommon: true, null!)
    {
    }

    /// <summary>
    /// Gets the name of the metric, as reported to Datadog
    /// </summary>
    public string MetricName { get; }

    /// <summary>
    /// Gets a value indicating whether the metric a "common" metric, shared across languages?
    /// </summary>
    public bool IsCommon { get; }

    /// <summary>
    /// Gets the namespace of the metric, if not the default (Tracer)
    /// </summary>
    public string? NameSpace { get; }
}
#endif
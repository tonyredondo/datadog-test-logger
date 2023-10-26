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

using System.Threading;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Telemetry;
internal partial class MetricsTelemetryCollector
{
    private const int GaugeLength = 8;

    /// <summary>
    /// Creates the buffer for the <see cref="Datadog.Trace.Telemetry.Metrics.Gauge" /> values.
    /// </summary>
    private static AggregatedMetric[] GetGaugeBuffer()
        => new AggregatedMetric[]
        {
            // stats_buckets, index = 0
            new(null),
            // instrumentations, index = 1
            new(new[] { "component_name:calltarget" }),
            new(new[] { "component_name:calltarget_derived" }),
            new(new[] { "component_name:calltarget_interfaces" }),
            new(new[] { "component_name:iast" }),
            new(new[] { "component_name:iast_derived" }),
            new(new[] { "component_name:iast_aspects" }),
            // direct_log_queue.length, index = 7
            new(null),
        };

    /// <summary>
    /// Gets an array of metric counts, indexed by integer value of the <see cref="Datadog.Trace.Telemetry.Metrics.Gauge" />.
    /// Each value represents the number of unique entries in the buffer returned by <see cref="GetGaugeBuffer()" />
    /// It is equal to the cardinality of the tag combinations (or 1 if there are no tags)
    /// </summary>
    private static int[] GaugeEntryCounts { get; }
        = new int[]{ 1, 6, 1, };

    public void RecordGaugeStatsBuckets(int value)
    {
        Interlocked.Exchange(ref _buffer.Gauge[0], value);
    }

    public void RecordGaugeInstrumentations(Datadog.Trace.Telemetry.Metrics.MetricTags.InstrumentationComponent tag, int value)
    {
        var index = 1 + (int)tag;
        Interlocked.Exchange(ref _buffer.Gauge[index], value);
    }

    public void RecordGaugeDirectLogQueue(int value)
    {
        Interlocked.Exchange(ref _buffer.Gauge[7], value);
    }
}
#endif
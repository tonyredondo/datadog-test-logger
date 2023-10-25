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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DatadogTestLogger.Vendors.Datadog.Trace.Telemetry.Metrics;
using DatadogTestLogger.Vendors.Datadog.Trace.Util;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Telemetry;

internal partial class MetricsTelemetryCollector
{
    private readonly Lazy<AggregatedMetrics> _aggregated = new();
    private MetricBuffer _buffer = new();
    private MetricBuffer _reserveBuffer = new();

    public void Record(PublicApiUsage publicApi)
    {
        // This can technically overflow, but it's _very_ unlikely as we reset every 10s
        // Negative values are normalized during polling
        Interlocked.Increment(ref _buffer.PublicApiCounts[(int)publicApi]);
    }

    internal override void Clear()
    {
        _reserveBuffer.Clear();
        var buffer = Interlocked.Exchange(ref _buffer, _reserveBuffer);
        buffer.Clear();
    }

    public MetricResults GetMetrics()
    {
        List<MetricData>? metricData;
        List<DistributionMetricData>? distributionData;

        var aggregated = _aggregated.Value;
        lock (aggregated)
        {
            metricData = GetMetricData(aggregated.PublicApiCounts, aggregated.Count, aggregated.CountShared, aggregated.Gauge);
            distributionData = GetDistributionData(aggregated.DistributionShared);
        }

        return new(metricData, distributionData);
    }

    /// <summary>
    /// Internal for testing
    /// </summary>
    internal override void AggregateMetrics()
    {
        var buffer = Interlocked.Exchange(ref _buffer, _reserveBuffer);

        var aggregated = _aggregated.Value;
        // _aggregated, containing the aggregated metrics, is not thread-safe
        // and is also used when getting the metrics for serialization.
        lock (aggregated)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            AggregateMetric(buffer.PublicApiCounts, timestamp, aggregated.PublicApiCounts);
            AggregateMetric(buffer.Count, timestamp, aggregated.Count);
            AggregateMetric(buffer.CountShared, timestamp, aggregated.CountShared);
            AggregateMetric(buffer.Gauge, timestamp, aggregated.Gauge);
            AggregateDistribution(buffer.DistributionShared, aggregated.DistributionShared);
        }

        // prepare the buffer for next time
        buffer.Clear();
        Interlocked.Exchange(ref _reserveBuffer, buffer);
    }

    /// <summary>
    /// Loop through the aggregated data, looking for any metrics that have points
    /// </summary>
    private List<MetricData>? GetMetricData(AggregatedMetric[] publicApis, AggregatedMetric[] count, AggregatedMetric[] countshared, AggregatedMetric[] gauge)
    {
        var apiLength = publicApis.Count(x => x.HasValues);
        var countLength = count.Count(x => x.HasValues);
        var countsharedLength = countshared.Count(x => x.HasValues);
        var gaugeLength = gauge.Count(x => x.HasValues);

        var totalLength = apiLength + countLength + countsharedLength + gaugeLength;
        if (totalLength == 0)
        {
            return null;
        }

        var data = new List<MetricData>(totalLength);

        if (apiLength > 0)
        {
            AddPublicApiMetricData(publicApis, data);
        }

        if (countLength > 0)
        {
            AddMetricData("count", count, data, CountEntryCounts, GetCountDetails);
        }

        if (countsharedLength > 0)
        {
            AddMetricData("count", countshared, data, CountSharedEntryCounts, GetCountSharedDetails);
        }

        if (gaugeLength > 0)
        {
            AddMetricData("gauge", gauge, data, GaugeEntryCounts, GetGaugeDetails);
        }

        return data;
    }

    private List<DistributionMetricData>? GetDistributionData(AggregatedDistribution[] distributionshared)
    {
        var distributionsharedLength = distributionshared.Count(x => x.HasValues);

        var totalLength = 0 + distributionsharedLength;
        if (totalLength == 0)
        {
            return null;
        }

        var data = new List<DistributionMetricData>(totalLength);

        if (distributionsharedLength > 0)
        {
            AddDistributionData(distributionshared, data, DistributionSharedEntryCounts, GetDistributionSharedDetails);
        }

        return data;
    }

    private static MetricDetails GetCountDetails(int i)
    {
        var metric = (Count)i;
        return new MetricDetails(metric.GetName(), metric.GetNamespace(), metric.IsCommon());
    }

    private static MetricDetails GetCountSharedDetails(int i)
    {
        var metric = (CountShared)i;
        return new MetricDetails(metric.GetName(), metric.GetNamespace(), metric.IsCommon());
    }

    private static MetricDetails GetGaugeDetails(int i)
    {
        var metric = (Gauge)i;
        return new MetricDetails(metric.GetName(), metric.GetNamespace(), metric.IsCommon());
    }

    private static MetricDetails GetDistributionSharedDetails(int i)
    {
        var metric = (DistributionShared)i;
        return new MetricDetails(metric.GetName(), metric.GetNamespace(), metric.IsCommon());
    }

    private class AggregatedMetrics
    {
        public readonly AggregatedMetric[] PublicApiCounts;
        public readonly AggregatedMetric[] Count;
        public readonly AggregatedMetric[] CountShared;
        public readonly AggregatedMetric[] Gauge;
        public readonly AggregatedDistribution[] DistributionShared;

        public AggregatedMetrics()
        {
            PublicApiCounts = GetPublicApiCountBuffer();
            Count = GetCountBuffer();
            CountShared = GetCountSharedBuffer();
            Gauge = GetGaugeBuffer();
            DistributionShared = GetDistributionSharedBuffer();
        }
    }

    protected class MetricBuffer
    {
        public readonly int[] PublicApiCounts;
        public readonly int[] Count;
        public readonly int[] CountShared;
        public readonly int[] Gauge;
        public readonly BoundedConcurrentQueue<double>[] DistributionShared;

        public MetricBuffer()
        {
            PublicApiCounts = new int[PublicApiUsageExtensions.Length];
            Count = new int[CountLength];
            CountShared = new int[CountSharedLength];
            Gauge = new int[GaugeLength];
            DistributionShared = new BoundedConcurrentQueue<double>[DistributionSharedLength];

            for (var i = DistributionShared.Length - 1; i >= 0; i--)
            {
                DistributionShared[i] = new BoundedConcurrentQueue<double>(queueLimit: 1000);
            }

        }

        public void Clear()
        {
            for (var i = 0; i < PublicApiCounts.Length; i++)
            {
                PublicApiCounts[i] = 0;
            }

            for (var i = 0; i < Count.Length; i++)
            {
                Count[i] = 0;
            }

            for (var i = 0; i < CountShared.Length; i++)
            {
                CountShared[i] = 0;
            }

            for (var i = 0; i < Gauge.Length; i++)
            {
                Gauge[i] = 0;
            }

            for (var i = 0; i < DistributionShared.Length; i++)
            {
                while (DistributionShared[i].TryDequeue(out _)) { }
            }
        }
    }
}
#endif
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
using System;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient.Bufferize;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient.Aggregator
{
    internal class MetricAggregatorParameters
    {
        public MetricAggregatorParameters(
            MetricSerializer serializer,
            BufferBuilder bufferBuilder,
            int maxUniqueStatsBeforeFlush,
            TimeSpan flushInterval)
        {
            Serializer = serializer;
            BufferBuilder = bufferBuilder;
            MaxUniqueStatsBeforeFlush = maxUniqueStatsBeforeFlush;
            FlushInterval = flushInterval;
        }

        public MetricSerializer Serializer { get; }

        public BufferBuilder BufferBuilder { get; }

        public int MaxUniqueStatsBeforeFlush { get; }

        public TimeSpan FlushInterval { get; }
    }
}

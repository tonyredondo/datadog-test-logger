//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Vendor.Datadog.Trace.Vendors.StatsdClient.Bufferize;
using Vendor.Datadog.Trace.Vendors.StatsdClient.Statistic;

namespace Vendor.Datadog.Trace.Vendors.StatsdClient.Aggregator
{
    /// <summary>
    /// AggregatorFlusher is responsible for flushing the aggregated `MetricStats` instances.
    /// </summary>
    internal class AggregatorFlusher<T>
    {
        private readonly MetricSerializer _serializer;
        private readonly BufferBuilder _bufferBuilder;
        private readonly Dictionary<MetricStatsKey, T> _values = new Dictionary<MetricStatsKey, T>();
        private readonly System.Diagnostics.Stopwatch _stopWatch = System.Diagnostics.Stopwatch.StartNew();
        private readonly int _maxUniqueStatsBeforeFlush;
        private readonly long _flushIntervalMilliseconds;
        private readonly SerializedMetric _serializedMetric = new SerializedMetric();
        private readonly MetricType _expectedMetricType;

        public AggregatorFlusher(MetricAggregatorParameters parameters, MetricType expectedMetricType)
        {
            _serializer = parameters.Serializer;
            _bufferBuilder = parameters.BufferBuilder;
            _flushIntervalMilliseconds = (long)parameters.FlushInterval.TotalMilliseconds;
            _maxUniqueStatsBeforeFlush = parameters.MaxUniqueStatsBeforeFlush;
            _expectedMetricType = expectedMetricType;
        }

        public bool TryGetValue(ref MetricStatsKey key, out T v)
        {
            return this._values.TryGetValue(key, out v);
        }

        public void Add(ref MetricStatsKey key, T v)
        {
            this._values.Add(key, v);
        }

        public void Update(ref MetricStatsKey key, T v)
        {
            this._values[key] = v;
        }

        public void TryFlush(Action<Dictionary<MetricStatsKey, T>> addSerializedMetric, bool force)
        {
            if (force
            || _stopWatch.ElapsedMilliseconds > _flushIntervalMilliseconds
            || _values.Count >= _maxUniqueStatsBeforeFlush)
            {
                addSerializedMetric(_values);
                _bufferBuilder.HandleBufferAndReset();
                this._stopWatch.Restart();
                _values.Clear();
            }
        }

        public void FlushStatsMetric(StatsMetric metric)
        {
            _serializer.SerializeTo(ref metric, _serializedMetric);
            _bufferBuilder.Add(_serializedMetric);
        }

        public MetricStatsKey CreateKey(StatsMetric metric)
        {
            if (metric.MetricType != _expectedMetricType)
            {
                throw new ArgumentException($"Metric type is {metric.MetricType} instead of {_expectedMetricType}.");
            }

            return new MetricStatsKey(metric.StatName, metric.Tags);
        }
    }
}

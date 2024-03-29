//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient.Statistic;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient
{
    internal class MetricSerializer
    {
        private static readonly Dictionary<MetricType, string> _commandToUnit = new Dictionary<MetricType, string>
                                                                {
                                                                    { MetricType.Count, "c" },
                                                                    { MetricType.Timing, "ms" },
                                                                    { MetricType.Gauge, "g" },
                                                                    { MetricType.Histogram, "h" },
                                                                    { MetricType.Distribution, "d" },
                                                                    { MetricType.Meter, "m" },
                                                                    { MetricType.Set, "s" },
                                                                };

        private readonly SerializerHelper _serializerHelper;
        private readonly string _prefix;

        internal MetricSerializer(SerializerHelper serializerHelper, string prefix)
        {
            _serializerHelper = serializerHelper;
            _prefix = string.IsNullOrEmpty(prefix) ? string.Empty : prefix + ".";
        }

        public void SerializeTo(ref StatsMetric metricStats, SerializedMetric serializedMetric)
        {
            serializedMetric.Reset();

            var builder = serializedMetric.Builder;
            var unit = _commandToUnit[metricStats.MetricType];

            builder.Append(_prefix);
            builder.Append(metricStats.StatName);
            builder.Append(':');
            switch (metricStats.MetricType)
            {
                case MetricType.Set: builder.Append(metricStats.StringValue); break;
                default: builder.AppendFormat(CultureInfo.InvariantCulture, "{0}", metricStats.NumericValue); break;
            }

            builder.Append('|');
            builder.Append(unit);

            if (metricStats.SampleRate != 1.0)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, "|@{0}", metricStats.SampleRate);
            }

            _serializerHelper.AppendTags(builder, metricStats.Tags);
        }
    }
}

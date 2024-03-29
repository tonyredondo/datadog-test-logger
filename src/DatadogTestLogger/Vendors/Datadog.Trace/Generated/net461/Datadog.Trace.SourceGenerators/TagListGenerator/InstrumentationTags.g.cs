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

using DatadogTestLogger.Vendors.Datadog.Trace.Processors;
using DatadogTestLogger.Vendors.Datadog.Trace.Tagging;
using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Tagging
{
    partial class InstrumentationTags
    {
        // AnalyticsSampleRateBytes = MessagePack.Serialize("_dd1.sr.eausr");
#if NETCOREAPP
        private static ReadOnlySpan<byte> AnalyticsSampleRateBytes => new byte[] { 173, 95, 100, 100, 49, 46, 115, 114, 46, 101, 97, 117, 115, 114 };
#else
        private static readonly byte[] AnalyticsSampleRateBytes = new byte[] { 173, 95, 100, 100, 49, 46, 115, 114, 46, 101, 97, 117, 115, 114 };
#endif

        public override double? GetMetric(string key)
        {
            return key switch
            {
                "_dd1.sr.eausr" => AnalyticsSampleRate,
                _ => base.GetMetric(key),
            };
        }

        public override void SetMetric(string key, double? value)
        {
            switch(key)
            {
                case "_dd1.sr.eausr": 
                    AnalyticsSampleRate = value;
                    break;
                default: 
                    base.SetMetric(key, value);
                    break;
            }
        }

        public override void EnumerateMetrics<TProcessor>(ref TProcessor processor)
        {
            if (AnalyticsSampleRate is not null)
            {
                processor.Process(new TagItem<double>("_dd1.sr.eausr", AnalyticsSampleRate.Value, AnalyticsSampleRateBytes));
            }

            base.EnumerateMetrics(ref processor);
        }

        protected override void WriteAdditionalMetrics(System.Text.StringBuilder sb)
        {
            if (AnalyticsSampleRate is not null)
            {
                sb.Append("_dd1.sr.eausr (metric):")
                  .Append(AnalyticsSampleRate.Value)
                  .Append(',');
            }

            base.WriteAdditionalMetrics(sb);
        }
    }
}

#endif
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NETFRAMEWORK
// <auto-generated/>
#nullable enable

using DatadogTestLogger.Vendors.Datadog.Trace.Processors;
using DatadogTestLogger.Vendors.Datadog.Trace.Tagging;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Tagging
{
    partial class CommonTags
    {
        // SamplingLimitDecisionBytes = System.Text.Encoding.UTF8.GetBytes("_dd.limit_psr");
        private static readonly byte[] SamplingLimitDecisionBytes = new byte[] { 95, 100, 100, 46, 108, 105, 109, 105, 116, 95, 112, 115, 114 };
        // TracesKeepRateBytes = System.Text.Encoding.UTF8.GetBytes("_dd.tracer_kr");
        private static readonly byte[] TracesKeepRateBytes = new byte[] { 95, 100, 100, 46, 116, 114, 97, 99, 101, 114, 95, 107, 114 };
        // SamplingAgentDecisionBytes = System.Text.Encoding.UTF8.GetBytes("_dd.agent_psr");
        private static readonly byte[] SamplingAgentDecisionBytes = new byte[] { 95, 100, 100, 46, 97, 103, 101, 110, 116, 95, 112, 115, 114 };

        public override double? GetMetric(string key)
        {
            return key switch
            {
                "_dd.limit_psr" => SamplingLimitDecision,
                "_dd.tracer_kr" => TracesKeepRate,
                "_dd.agent_psr" => SamplingAgentDecision,
                _ => base.GetMetric(key),
            };
        }

        public override void SetMetric(string key, double? value)
        {
            switch(key)
            {
                case "_dd.limit_psr": 
                    SamplingLimitDecision = value;
                    break;
                case "_dd.tracer_kr": 
                    TracesKeepRate = value;
                    break;
                case "_dd.agent_psr": 
                    SamplingAgentDecision = value;
                    break;
                default: 
                    base.SetMetric(key, value);
                    break;
            }
        }

        public override void EnumerateMetrics<TProcessor>(ref TProcessor processor)
        {
            if (SamplingLimitDecision is not null)
            {
                processor.Process(new TagItem<double>("_dd.limit_psr", SamplingLimitDecision.Value, SamplingLimitDecisionBytes));
            }

            if (TracesKeepRate is not null)
            {
                processor.Process(new TagItem<double>("_dd.tracer_kr", TracesKeepRate.Value, TracesKeepRateBytes));
            }

            if (SamplingAgentDecision is not null)
            {
                processor.Process(new TagItem<double>("_dd.agent_psr", SamplingAgentDecision.Value, SamplingAgentDecisionBytes));
            }

            base.EnumerateMetrics(ref processor);
        }

        protected override void WriteAdditionalMetrics(System.Text.StringBuilder sb)
        {
            if (SamplingLimitDecision is not null)
            {
                sb.Append("_dd.limit_psr (metric):")
                  .Append(SamplingLimitDecision.Value)
                  .Append(',');
            }

            if (TracesKeepRate is not null)
            {
                sb.Append("_dd.tracer_kr (metric):")
                  .Append(TracesKeepRate.Value)
                  .Append(',');
            }

            if (SamplingAgentDecision is not null)
            {
                sb.Append("_dd.agent_psr (metric):")
                  .Append(SamplingAgentDecision.Value)
                  .Append(',');
            }

            base.WriteAdditionalMetrics(sb);
        }
    }
}

#endif
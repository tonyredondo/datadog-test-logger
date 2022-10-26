﻿// <auto-generated/>
#nullable enable

using Vendor.Datadog.Trace.Processors;
using Vendor.Datadog.Trace.Tagging;

namespace Vendor.Datadog.Trace.Tagging
{
    partial class ProcessCommandStartTags
    {
        // SpanKindBytes = System.Text.Encoding.UTF8.GetBytes("span.kind");
        private static readonly byte[] SpanKindBytes = new byte[] { 115, 112, 97, 110, 46, 107, 105, 110, 100 };
        // EnvironmentVariablesBytes = System.Text.Encoding.UTF8.GetBytes("cmd.environment_variables");
        private static readonly byte[] EnvironmentVariablesBytes = new byte[] { 99, 109, 100, 46, 101, 110, 118, 105, 114, 111, 110, 109, 101, 110, 116, 95, 118, 97, 114, 105, 97, 98, 108, 101, 115 };

        public override string? GetTag(string key)
        {
            return key switch
            {
                "span.kind" => SpanKind,
                "cmd.environment_variables" => EnvironmentVariables,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "cmd.environment_variables": 
                    EnvironmentVariables = value;
                    break;
                default: 
                    base.SetTag(key, value);
                    break;
            }
        }

        public override void EnumerateTags<TProcessor>(ref TProcessor processor)
        {
            if (SpanKind is not null)
            {
                processor.Process(new TagItem<string>("span.kind", SpanKind, SpanKindBytes));
            }

            if (EnvironmentVariables is not null)
            {
                processor.Process(new TagItem<string>("cmd.environment_variables", EnvironmentVariables, EnvironmentVariablesBytes));
            }

            base.EnumerateTags(ref processor);
        }

        protected override void WriteAdditionalTags(System.Text.StringBuilder sb)
        {
            if (SpanKind is not null)
            {
                sb.Append("span.kind (tag):")
                  .Append(SpanKind)
                  .Append(',');
            }

            if (EnvironmentVariables is not null)
            {
                sb.Append("cmd.environment_variables (tag):")
                  .Append(EnvironmentVariables)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
    }
}

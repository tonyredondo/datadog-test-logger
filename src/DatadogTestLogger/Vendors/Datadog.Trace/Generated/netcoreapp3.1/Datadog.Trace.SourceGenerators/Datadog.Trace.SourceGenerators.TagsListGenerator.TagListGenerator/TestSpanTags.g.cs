//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NETCOREAPP3_1_OR_GREATER && !NET6_0_OR_GREATER
// <auto-generated/>
#nullable enable

using DatadogTestLogger.Vendors.Datadog.Trace.Processors;
using DatadogTestLogger.Vendors.Datadog.Trace.Tagging;
using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Ci.Tagging
{
    partial class TestSpanTags
    {
        // SourceStartBytes = MessagePack.Serialize("test.source.start");
#if NETCOREAPP
        private static ReadOnlySpan<byte> SourceStartBytes => new byte[] { 177, 116, 101, 115, 116, 46, 115, 111, 117, 114, 99, 101, 46, 115, 116, 97, 114, 116 };
#else
        private static readonly byte[] SourceStartBytes = new byte[] { 177, 116, 101, 115, 116, 46, 115, 111, 117, 114, 99, 101, 46, 115, 116, 97, 114, 116 };
#endif
        // SourceEndBytes = MessagePack.Serialize("test.source.end");
#if NETCOREAPP
        private static ReadOnlySpan<byte> SourceEndBytes => new byte[] { 175, 116, 101, 115, 116, 46, 115, 111, 117, 114, 99, 101, 46, 101, 110, 100 };
#else
        private static readonly byte[] SourceEndBytes = new byte[] { 175, 116, 101, 115, 116, 46, 115, 111, 117, 114, 99, 101, 46, 101, 110, 100 };
#endif
        // NameBytes = MessagePack.Serialize("test.name");
#if NETCOREAPP
        private static ReadOnlySpan<byte> NameBytes => new byte[] { 169, 116, 101, 115, 116, 46, 110, 97, 109, 101 };
#else
        private static readonly byte[] NameBytes = new byte[] { 169, 116, 101, 115, 116, 46, 110, 97, 109, 101 };
#endif
        // ParametersBytes = MessagePack.Serialize("test.parameters");
#if NETCOREAPP
        private static ReadOnlySpan<byte> ParametersBytes => new byte[] { 175, 116, 101, 115, 116, 46, 112, 97, 114, 97, 109, 101, 116, 101, 114, 115 };
#else
        private static readonly byte[] ParametersBytes = new byte[] { 175, 116, 101, 115, 116, 46, 112, 97, 114, 97, 109, 101, 116, 101, 114, 115 };
#endif
        // SourceFileBytes = MessagePack.Serialize("test.source.file");
#if NETCOREAPP
        private static ReadOnlySpan<byte> SourceFileBytes => new byte[] { 176, 116, 101, 115, 116, 46, 115, 111, 117, 114, 99, 101, 46, 102, 105, 108, 101 };
#else
        private static readonly byte[] SourceFileBytes = new byte[] { 176, 116, 101, 115, 116, 46, 115, 111, 117, 114, 99, 101, 46, 102, 105, 108, 101 };
#endif
        // CodeOwnersBytes = MessagePack.Serialize("test.codeowners");
#if NETCOREAPP
        private static ReadOnlySpan<byte> CodeOwnersBytes => new byte[] { 175, 116, 101, 115, 116, 46, 99, 111, 100, 101, 111, 119, 110, 101, 114, 115 };
#else
        private static readonly byte[] CodeOwnersBytes = new byte[] { 175, 116, 101, 115, 116, 46, 99, 111, 100, 101, 111, 119, 110, 101, 114, 115 };
#endif
        // TraitsBytes = MessagePack.Serialize("test.traits");
#if NETCOREAPP
        private static ReadOnlySpan<byte> TraitsBytes => new byte[] { 171, 116, 101, 115, 116, 46, 116, 114, 97, 105, 116, 115 };
#else
        private static readonly byte[] TraitsBytes = new byte[] { 171, 116, 101, 115, 116, 46, 116, 114, 97, 105, 116, 115 };
#endif
        // SkipReasonBytes = MessagePack.Serialize("test.skip_reason");
#if NETCOREAPP
        private static ReadOnlySpan<byte> SkipReasonBytes => new byte[] { 176, 116, 101, 115, 116, 46, 115, 107, 105, 112, 95, 114, 101, 97, 115, 111, 110 };
#else
        private static readonly byte[] SkipReasonBytes = new byte[] { 176, 116, 101, 115, 116, 46, 115, 107, 105, 112, 95, 114, 101, 97, 115, 111, 110 };
#endif
        // SkippedByIntelligentTestRunnerBytes = MessagePack.Serialize("test.skipped_by_itr");
#if NETCOREAPP
        private static ReadOnlySpan<byte> SkippedByIntelligentTestRunnerBytes => new byte[] { 179, 116, 101, 115, 116, 46, 115, 107, 105, 112, 112, 101, 100, 95, 98, 121, 95, 105, 116, 114 };
#else
        private static readonly byte[] SkippedByIntelligentTestRunnerBytes = new byte[] { 179, 116, 101, 115, 116, 46, 115, 107, 105, 112, 112, 101, 100, 95, 98, 121, 95, 105, 116, 114 };
#endif

        public override string? GetTag(string key)
        {
            return key switch
            {
                "test.name" => Name,
                "test.parameters" => Parameters,
                "test.source.file" => SourceFile,
                "test.codeowners" => CodeOwners,
                "test.traits" => Traits,
                "test.skip_reason" => SkipReason,
                "test.skipped_by_itr" => SkippedByIntelligentTestRunner,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "test.name": 
                    Name = value;
                    break;
                case "test.parameters": 
                    Parameters = value;
                    break;
                case "test.source.file": 
                    SourceFile = value;
                    break;
                case "test.codeowners": 
                    CodeOwners = value;
                    break;
                case "test.traits": 
                    Traits = value;
                    break;
                case "test.skip_reason": 
                    SkipReason = value;
                    break;
                case "test.skipped_by_itr": 
                    SkippedByIntelligentTestRunner = value;
                    break;
                default: 
                    base.SetTag(key, value);
                    break;
            }
        }

        public override void EnumerateTags<TProcessor>(ref TProcessor processor)
        {
            if (Name is not null)
            {
                processor.Process(new TagItem<string>("test.name", Name, NameBytes));
            }

            if (Parameters is not null)
            {
                processor.Process(new TagItem<string>("test.parameters", Parameters, ParametersBytes));
            }

            if (SourceFile is not null)
            {
                processor.Process(new TagItem<string>("test.source.file", SourceFile, SourceFileBytes));
            }

            if (CodeOwners is not null)
            {
                processor.Process(new TagItem<string>("test.codeowners", CodeOwners, CodeOwnersBytes));
            }

            if (Traits is not null)
            {
                processor.Process(new TagItem<string>("test.traits", Traits, TraitsBytes));
            }

            if (SkipReason is not null)
            {
                processor.Process(new TagItem<string>("test.skip_reason", SkipReason, SkipReasonBytes));
            }

            if (SkippedByIntelligentTestRunner is not null)
            {
                processor.Process(new TagItem<string>("test.skipped_by_itr", SkippedByIntelligentTestRunner, SkippedByIntelligentTestRunnerBytes));
            }

            base.EnumerateTags(ref processor);
        }

        protected override void WriteAdditionalTags(System.Text.StringBuilder sb)
        {
            if (Name is not null)
            {
                sb.Append("test.name (tag):")
                  .Append(Name)
                  .Append(',');
            }

            if (Parameters is not null)
            {
                sb.Append("test.parameters (tag):")
                  .Append(Parameters)
                  .Append(',');
            }

            if (SourceFile is not null)
            {
                sb.Append("test.source.file (tag):")
                  .Append(SourceFile)
                  .Append(',');
            }

            if (CodeOwners is not null)
            {
                sb.Append("test.codeowners (tag):")
                  .Append(CodeOwners)
                  .Append(',');
            }

            if (Traits is not null)
            {
                sb.Append("test.traits (tag):")
                  .Append(Traits)
                  .Append(',');
            }

            if (SkipReason is not null)
            {
                sb.Append("test.skip_reason (tag):")
                  .Append(SkipReason)
                  .Append(',');
            }

            if (SkippedByIntelligentTestRunner is not null)
            {
                sb.Append("test.skipped_by_itr (tag):")
                  .Append(SkippedByIntelligentTestRunner)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
        public override double? GetMetric(string key)
        {
            return key switch
            {
                "test.source.start" => SourceStart,
                "test.source.end" => SourceEnd,
                _ => base.GetMetric(key),
            };
        }

        public override void SetMetric(string key, double? value)
        {
            switch(key)
            {
                case "test.source.start": 
                    SourceStart = value;
                    break;
                case "test.source.end": 
                    SourceEnd = value;
                    break;
                default: 
                    base.SetMetric(key, value);
                    break;
            }
        }

        public override void EnumerateMetrics<TProcessor>(ref TProcessor processor)
        {
            if (SourceStart is not null)
            {
                processor.Process(new TagItem<double>("test.source.start", SourceStart.Value, SourceStartBytes));
            }

            if (SourceEnd is not null)
            {
                processor.Process(new TagItem<double>("test.source.end", SourceEnd.Value, SourceEndBytes));
            }

            base.EnumerateMetrics(ref processor);
        }

        protected override void WriteAdditionalMetrics(System.Text.StringBuilder sb)
        {
            if (SourceStart is not null)
            {
                sb.Append("test.source.start (metric):")
                  .Append(SourceStart.Value)
                  .Append(',');
            }

            if (SourceEnd is not null)
            {
                sb.Append("test.source.end (metric):")
                  .Append(SourceEnd.Value)
                  .Append(',');
            }

            base.WriteAdditionalMetrics(sb);
        }
    }
}

#endif
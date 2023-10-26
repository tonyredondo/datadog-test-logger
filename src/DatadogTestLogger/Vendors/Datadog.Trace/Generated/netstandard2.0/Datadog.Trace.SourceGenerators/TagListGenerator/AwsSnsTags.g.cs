//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if !NETFRAMEWORK && !NETCOREAPP3_1_OR_GREATER
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
    partial class AwsSnsTags
    {
        // AwsTopicNameBytes = MessagePack.Serialize("aws.topic.name");
#if NETCOREAPP
        private static ReadOnlySpan<byte> AwsTopicNameBytes => new byte[] { 174, 97, 119, 115, 46, 116, 111, 112, 105, 99, 46, 110, 97, 109, 101 };
#else
        private static readonly byte[] AwsTopicNameBytes = new byte[] { 174, 97, 119, 115, 46, 116, 111, 112, 105, 99, 46, 110, 97, 109, 101 };
#endif
        // TopicNameBytes = MessagePack.Serialize("topicname");
#if NETCOREAPP
        private static ReadOnlySpan<byte> TopicNameBytes => new byte[] { 169, 116, 111, 112, 105, 99, 110, 97, 109, 101 };
#else
        private static readonly byte[] TopicNameBytes = new byte[] { 169, 116, 111, 112, 105, 99, 110, 97, 109, 101 };
#endif
        // TopicArnBytes = MessagePack.Serialize("aws.topic.arn");
#if NETCOREAPP
        private static ReadOnlySpan<byte> TopicArnBytes => new byte[] { 173, 97, 119, 115, 46, 116, 111, 112, 105, 99, 46, 97, 114, 110 };
#else
        private static readonly byte[] TopicArnBytes = new byte[] { 173, 97, 119, 115, 46, 116, 111, 112, 105, 99, 46, 97, 114, 110 };
#endif
        // SpanKindBytes = MessagePack.Serialize("span.kind");
#if NETCOREAPP
        private static ReadOnlySpan<byte> SpanKindBytes => new byte[] { 169, 115, 112, 97, 110, 46, 107, 105, 110, 100 };
#else
        private static readonly byte[] SpanKindBytes = new byte[] { 169, 115, 112, 97, 110, 46, 107, 105, 110, 100 };
#endif

        public override string? GetTag(string key)
        {
            return key switch
            {
                "aws.topic.name" => AwsTopicName,
                "topicname" => TopicName,
                "aws.topic.arn" => TopicArn,
                "span.kind" => SpanKind,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "topicname": 
                    TopicName = value;
                    break;
                case "aws.topic.arn": 
                    TopicArn = value;
                    break;
                case "aws.topic.name": 
                case "span.kind": 
                    Logger.Value.Warning("Attempted to set readonly tag {TagName} on {TagType}. Ignoring.", key, nameof(AwsSnsTags));
                    break;
                default: 
                    base.SetTag(key, value);
                    break;
            }
        }

        public override void EnumerateTags<TProcessor>(ref TProcessor processor)
        {
            if (AwsTopicName is not null)
            {
                processor.Process(new TagItem<string>("aws.topic.name", AwsTopicName, AwsTopicNameBytes));
            }

            if (TopicName is not null)
            {
                processor.Process(new TagItem<string>("topicname", TopicName, TopicNameBytes));
            }

            if (TopicArn is not null)
            {
                processor.Process(new TagItem<string>("aws.topic.arn", TopicArn, TopicArnBytes));
            }

            if (SpanKind is not null)
            {
                processor.Process(new TagItem<string>("span.kind", SpanKind, SpanKindBytes));
            }

            base.EnumerateTags(ref processor);
        }

        protected override void WriteAdditionalTags(System.Text.StringBuilder sb)
        {
            if (AwsTopicName is not null)
            {
                sb.Append("aws.topic.name (tag):")
                  .Append(AwsTopicName)
                  .Append(',');
            }

            if (TopicName is not null)
            {
                sb.Append("topicname (tag):")
                  .Append(TopicName)
                  .Append(',');
            }

            if (TopicArn is not null)
            {
                sb.Append("aws.topic.arn (tag):")
                  .Append(TopicArn)
                  .Append(',');
            }

            if (SpanKind is not null)
            {
                sb.Append("span.kind (tag):")
                  .Append(SpanKind)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
    }
}

#endif
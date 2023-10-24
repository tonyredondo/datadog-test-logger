//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NETCOREAPP3_1_OR_GREATER && !NET6_0_OR_GREATER
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
    partial class AzureServiceBusTags
    {
        // AnalyticsSampleRateBytes = MessagePack.Serialize("_dd1.sr.eausr");
#if NETCOREAPP
        private static ReadOnlySpan<byte> AnalyticsSampleRateBytes => new byte[] { 173, 95, 100, 100, 49, 46, 115, 114, 46, 101, 97, 117, 115, 114 };
#else
        private static readonly byte[] AnalyticsSampleRateBytes = new byte[] { 173, 95, 100, 100, 49, 46, 115, 114, 46, 101, 97, 117, 115, 114 };
#endif
        // MessageQueueTimeMsBytes = MessagePack.Serialize("message.queue_time_ms");
#if NETCOREAPP
        private static ReadOnlySpan<byte> MessageQueueTimeMsBytes => new byte[] { 181, 109, 101, 115, 115, 97, 103, 101, 46, 113, 117, 101, 117, 101, 95, 116, 105, 109, 101, 95, 109, 115 };
#else
        private static readonly byte[] MessageQueueTimeMsBytes = new byte[] { 181, 109, 101, 115, 115, 97, 103, 101, 46, 113, 117, 101, 117, 101, 95, 116, 105, 109, 101, 95, 109, 115 };
#endif
        // MessagingSourceNameBytes = MessagePack.Serialize("messaging.source.name");
#if NETCOREAPP
        private static ReadOnlySpan<byte> MessagingSourceNameBytes => new byte[] { 181, 109, 101, 115, 115, 97, 103, 105, 110, 103, 46, 115, 111, 117, 114, 99, 101, 46, 110, 97, 109, 101 };
#else
        private static readonly byte[] MessagingSourceNameBytes = new byte[] { 181, 109, 101, 115, 115, 97, 103, 105, 110, 103, 46, 115, 111, 117, 114, 99, 101, 46, 110, 97, 109, 101 };
#endif
        // MessagingDestinationNameBytes = MessagePack.Serialize("messaging.destination.name");
#if NETCOREAPP
        private static ReadOnlySpan<byte> MessagingDestinationNameBytes => new byte[] { 186, 109, 101, 115, 115, 97, 103, 105, 110, 103, 46, 100, 101, 115, 116, 105, 110, 97, 116, 105, 111, 110, 46, 110, 97, 109, 101 };
#else
        private static readonly byte[] MessagingDestinationNameBytes = new byte[] { 186, 109, 101, 115, 115, 97, 103, 105, 110, 103, 46, 100, 101, 115, 116, 105, 110, 97, 116, 105, 111, 110, 46, 110, 97, 109, 101 };
#endif
        // MessagingOperationBytes = MessagePack.Serialize("messaging.operation");
#if NETCOREAPP
        private static ReadOnlySpan<byte> MessagingOperationBytes => new byte[] { 179, 109, 101, 115, 115, 97, 103, 105, 110, 103, 46, 111, 112, 101, 114, 97, 116, 105, 111, 110 };
#else
        private static readonly byte[] MessagingOperationBytes = new byte[] { 179, 109, 101, 115, 115, 97, 103, 105, 110, 103, 46, 111, 112, 101, 114, 97, 116, 105, 111, 110 };
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
                "messaging.source.name" => MessagingSourceName,
                "messaging.destination.name" => MessagingDestinationName,
                "messaging.operation" => MessagingOperation,
                "span.kind" => SpanKind,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "messaging.source.name": 
                    MessagingSourceName = value;
                    break;
                case "messaging.destination.name": 
                    MessagingDestinationName = value;
                    break;
                case "messaging.operation": 
                    MessagingOperation = value;
                    break;
                case "span.kind": 
                    SpanKind = value;
                    break;
                default: 
                    base.SetTag(key, value);
                    break;
            }
        }

        public override void EnumerateTags<TProcessor>(ref TProcessor processor)
        {
            if (MessagingSourceName is not null)
            {
                processor.Process(new TagItem<string>("messaging.source.name", MessagingSourceName, MessagingSourceNameBytes));
            }

            if (MessagingDestinationName is not null)
            {
                processor.Process(new TagItem<string>("messaging.destination.name", MessagingDestinationName, MessagingDestinationNameBytes));
            }

            if (MessagingOperation is not null)
            {
                processor.Process(new TagItem<string>("messaging.operation", MessagingOperation, MessagingOperationBytes));
            }

            if (SpanKind is not null)
            {
                processor.Process(new TagItem<string>("span.kind", SpanKind, SpanKindBytes));
            }

            base.EnumerateTags(ref processor);
        }

        protected override void WriteAdditionalTags(System.Text.StringBuilder sb)
        {
            if (MessagingSourceName is not null)
            {
                sb.Append("messaging.source.name (tag):")
                  .Append(MessagingSourceName)
                  .Append(',');
            }

            if (MessagingDestinationName is not null)
            {
                sb.Append("messaging.destination.name (tag):")
                  .Append(MessagingDestinationName)
                  .Append(',');
            }

            if (MessagingOperation is not null)
            {
                sb.Append("messaging.operation (tag):")
                  .Append(MessagingOperation)
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
        public override double? GetMetric(string key)
        {
            return key switch
            {
                "_dd1.sr.eausr" => AnalyticsSampleRate,
                "message.queue_time_ms" => MessageQueueTimeMs,
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
                case "message.queue_time_ms": 
                    MessageQueueTimeMs = value;
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

            if (MessageQueueTimeMs is not null)
            {
                processor.Process(new TagItem<double>("message.queue_time_ms", MessageQueueTimeMs.Value, MessageQueueTimeMsBytes));
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

            if (MessageQueueTimeMs is not null)
            {
                sb.Append("message.queue_time_ms (metric):")
                  .Append(MessageQueueTimeMs.Value)
                  .Append(',');
            }

            base.WriteAdditionalMetrics(sb);
        }
    }
}

#endif
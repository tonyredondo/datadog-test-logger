//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if !NETFRAMEWORK && !NETCOREAPP3_1_OR_GREATER
// <auto-generated/>
#nullable enable

using DatadogTestLogger.Vendors.Datadog.Trace.Processors;
using DatadogTestLogger.Vendors.Datadog.Trace.Tagging;
using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Tagging
{
    partial class RabbitMQTags
    {
        // SpanKindBytes = MessagePack.Serialize("span.kind");
#if NETCOREAPP
        private static ReadOnlySpan<byte> SpanKindBytes => new byte[] { 169, 115, 112, 97, 110, 46, 107, 105, 110, 100 };
#else
        private static readonly byte[] SpanKindBytes = new byte[] { 169, 115, 112, 97, 110, 46, 107, 105, 110, 100 };
#endif
        // InstrumentationNameBytes = MessagePack.Serialize("component");
#if NETCOREAPP
        private static ReadOnlySpan<byte> InstrumentationNameBytes => new byte[] { 169, 99, 111, 109, 112, 111, 110, 101, 110, 116 };
#else
        private static readonly byte[] InstrumentationNameBytes = new byte[] { 169, 99, 111, 109, 112, 111, 110, 101, 110, 116 };
#endif
        // CommandBytes = MessagePack.Serialize("amqp.command");
#if NETCOREAPP
        private static ReadOnlySpan<byte> CommandBytes => new byte[] { 172, 97, 109, 113, 112, 46, 99, 111, 109, 109, 97, 110, 100 };
#else
        private static readonly byte[] CommandBytes = new byte[] { 172, 97, 109, 113, 112, 46, 99, 111, 109, 109, 97, 110, 100 };
#endif
        // DeliveryModeBytes = MessagePack.Serialize("amqp.delivery_mode");
#if NETCOREAPP
        private static ReadOnlySpan<byte> DeliveryModeBytes => new byte[] { 178, 97, 109, 113, 112, 46, 100, 101, 108, 105, 118, 101, 114, 121, 95, 109, 111, 100, 101 };
#else
        private static readonly byte[] DeliveryModeBytes = new byte[] { 178, 97, 109, 113, 112, 46, 100, 101, 108, 105, 118, 101, 114, 121, 95, 109, 111, 100, 101 };
#endif
        // ExchangeBytes = MessagePack.Serialize("amqp.exchange");
#if NETCOREAPP
        private static ReadOnlySpan<byte> ExchangeBytes => new byte[] { 173, 97, 109, 113, 112, 46, 101, 120, 99, 104, 97, 110, 103, 101 };
#else
        private static readonly byte[] ExchangeBytes = new byte[] { 173, 97, 109, 113, 112, 46, 101, 120, 99, 104, 97, 110, 103, 101 };
#endif
        // RoutingKeyBytes = MessagePack.Serialize("amqp.routing_key");
#if NETCOREAPP
        private static ReadOnlySpan<byte> RoutingKeyBytes => new byte[] { 176, 97, 109, 113, 112, 46, 114, 111, 117, 116, 105, 110, 103, 95, 107, 101, 121 };
#else
        private static readonly byte[] RoutingKeyBytes = new byte[] { 176, 97, 109, 113, 112, 46, 114, 111, 117, 116, 105, 110, 103, 95, 107, 101, 121 };
#endif
        // MessageSizeBytes = MessagePack.Serialize("message.size");
#if NETCOREAPP
        private static ReadOnlySpan<byte> MessageSizeBytes => new byte[] { 172, 109, 101, 115, 115, 97, 103, 101, 46, 115, 105, 122, 101 };
#else
        private static readonly byte[] MessageSizeBytes = new byte[] { 172, 109, 101, 115, 115, 97, 103, 101, 46, 115, 105, 122, 101 };
#endif
        // QueueBytes = MessagePack.Serialize("amqp.queue");
#if NETCOREAPP
        private static ReadOnlySpan<byte> QueueBytes => new byte[] { 170, 97, 109, 113, 112, 46, 113, 117, 101, 117, 101 };
#else
        private static readonly byte[] QueueBytes = new byte[] { 170, 97, 109, 113, 112, 46, 113, 117, 101, 117, 101 };
#endif
        // OutHostBytes = MessagePack.Serialize("out.host");
#if NETCOREAPP
        private static ReadOnlySpan<byte> OutHostBytes => new byte[] { 168, 111, 117, 116, 46, 104, 111, 115, 116 };
#else
        private static readonly byte[] OutHostBytes = new byte[] { 168, 111, 117, 116, 46, 104, 111, 115, 116 };
#endif

        public override string? GetTag(string key)
        {
            return key switch
            {
                "span.kind" => SpanKind,
                "component" => InstrumentationName,
                "amqp.command" => Command,
                "amqp.delivery_mode" => DeliveryMode,
                "amqp.exchange" => Exchange,
                "amqp.routing_key" => RoutingKey,
                "message.size" => MessageSize,
                "amqp.queue" => Queue,
                "out.host" => OutHost,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "component": 
                    InstrumentationName = value;
                    break;
                case "amqp.command": 
                    Command = value;
                    break;
                case "amqp.delivery_mode": 
                    DeliveryMode = value;
                    break;
                case "amqp.exchange": 
                    Exchange = value;
                    break;
                case "amqp.routing_key": 
                    RoutingKey = value;
                    break;
                case "message.size": 
                    MessageSize = value;
                    break;
                case "amqp.queue": 
                    Queue = value;
                    break;
                case "out.host": 
                    OutHost = value;
                    break;
                case "span.kind": 
                    Logger.Value.Warning("Attempted to set readonly tag {TagName} on {TagType}. Ignoring.", key, nameof(RabbitMQTags));
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

            if (InstrumentationName is not null)
            {
                processor.Process(new TagItem<string>("component", InstrumentationName, InstrumentationNameBytes));
            }

            if (Command is not null)
            {
                processor.Process(new TagItem<string>("amqp.command", Command, CommandBytes));
            }

            if (DeliveryMode is not null)
            {
                processor.Process(new TagItem<string>("amqp.delivery_mode", DeliveryMode, DeliveryModeBytes));
            }

            if (Exchange is not null)
            {
                processor.Process(new TagItem<string>("amqp.exchange", Exchange, ExchangeBytes));
            }

            if (RoutingKey is not null)
            {
                processor.Process(new TagItem<string>("amqp.routing_key", RoutingKey, RoutingKeyBytes));
            }

            if (MessageSize is not null)
            {
                processor.Process(new TagItem<string>("message.size", MessageSize, MessageSizeBytes));
            }

            if (Queue is not null)
            {
                processor.Process(new TagItem<string>("amqp.queue", Queue, QueueBytes));
            }

            if (OutHost is not null)
            {
                processor.Process(new TagItem<string>("out.host", OutHost, OutHostBytes));
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

            if (InstrumentationName is not null)
            {
                sb.Append("component (tag):")
                  .Append(InstrumentationName)
                  .Append(',');
            }

            if (Command is not null)
            {
                sb.Append("amqp.command (tag):")
                  .Append(Command)
                  .Append(',');
            }

            if (DeliveryMode is not null)
            {
                sb.Append("amqp.delivery_mode (tag):")
                  .Append(DeliveryMode)
                  .Append(',');
            }

            if (Exchange is not null)
            {
                sb.Append("amqp.exchange (tag):")
                  .Append(Exchange)
                  .Append(',');
            }

            if (RoutingKey is not null)
            {
                sb.Append("amqp.routing_key (tag):")
                  .Append(RoutingKey)
                  .Append(',');
            }

            if (MessageSize is not null)
            {
                sb.Append("message.size (tag):")
                  .Append(MessageSize)
                  .Append(',');
            }

            if (Queue is not null)
            {
                sb.Append("amqp.queue (tag):")
                  .Append(Queue)
                  .Append(',');
            }

            if (OutHost is not null)
            {
                sb.Append("out.host (tag):")
                  .Append(OutHost)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
    }
}

#endif
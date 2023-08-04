//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NET6_0_OR_GREATER
// <auto-generated/>
#nullable enable

using DatadogTestLogger.Vendors.Datadog.Trace.Processors;
using DatadogTestLogger.Vendors.Datadog.Trace.Tagging;
using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Tagging
{
    partial class AerospikeTags
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
        // KeyBytes = MessagePack.Serialize("aerospike.key");
#if NETCOREAPP
        private static ReadOnlySpan<byte> KeyBytes => new byte[] { 173, 97, 101, 114, 111, 115, 112, 105, 107, 101, 46, 107, 101, 121 };
#else
        private static readonly byte[] KeyBytes = new byte[] { 173, 97, 101, 114, 111, 115, 112, 105, 107, 101, 46, 107, 101, 121 };
#endif
        // NamespaceBytes = MessagePack.Serialize("aerospike.namespace");
#if NETCOREAPP
        private static ReadOnlySpan<byte> NamespaceBytes => new byte[] { 179, 97, 101, 114, 111, 115, 112, 105, 107, 101, 46, 110, 97, 109, 101, 115, 112, 97, 99, 101 };
#else
        private static readonly byte[] NamespaceBytes = new byte[] { 179, 97, 101, 114, 111, 115, 112, 105, 107, 101, 46, 110, 97, 109, 101, 115, 112, 97, 99, 101 };
#endif
        // SetNameBytes = MessagePack.Serialize("aerospike.setname");
#if NETCOREAPP
        private static ReadOnlySpan<byte> SetNameBytes => new byte[] { 177, 97, 101, 114, 111, 115, 112, 105, 107, 101, 46, 115, 101, 116, 110, 97, 109, 101 };
#else
        private static readonly byte[] SetNameBytes = new byte[] { 177, 97, 101, 114, 111, 115, 112, 105, 107, 101, 46, 115, 101, 116, 110, 97, 109, 101 };
#endif
        // UserKeyBytes = MessagePack.Serialize("aerospike.userkey");
#if NETCOREAPP
        private static ReadOnlySpan<byte> UserKeyBytes => new byte[] { 177, 97, 101, 114, 111, 115, 112, 105, 107, 101, 46, 117, 115, 101, 114, 107, 101, 121 };
#else
        private static readonly byte[] UserKeyBytes = new byte[] { 177, 97, 101, 114, 111, 115, 112, 105, 107, 101, 46, 117, 115, 101, 114, 107, 101, 121 };
#endif

        public override string? GetTag(string key)
        {
            return key switch
            {
                "span.kind" => SpanKind,
                "component" => InstrumentationName,
                "aerospike.key" => Key,
                "aerospike.namespace" => Namespace,
                "aerospike.setname" => SetName,
                "aerospike.userkey" => UserKey,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "aerospike.key": 
                    Key = value;
                    break;
                case "aerospike.namespace": 
                    Namespace = value;
                    break;
                case "aerospike.setname": 
                    SetName = value;
                    break;
                case "aerospike.userkey": 
                    UserKey = value;
                    break;
                case "span.kind": 
                case "component": 
                    Logger.Value.Warning("Attempted to set readonly tag {TagName} on {TagType}. Ignoring.", key, nameof(AerospikeTags));
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

            if (Key is not null)
            {
                processor.Process(new TagItem<string>("aerospike.key", Key, KeyBytes));
            }

            if (Namespace is not null)
            {
                processor.Process(new TagItem<string>("aerospike.namespace", Namespace, NamespaceBytes));
            }

            if (SetName is not null)
            {
                processor.Process(new TagItem<string>("aerospike.setname", SetName, SetNameBytes));
            }

            if (UserKey is not null)
            {
                processor.Process(new TagItem<string>("aerospike.userkey", UserKey, UserKeyBytes));
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

            if (Key is not null)
            {
                sb.Append("aerospike.key (tag):")
                  .Append(Key)
                  .Append(',');
            }

            if (Namespace is not null)
            {
                sb.Append("aerospike.namespace (tag):")
                  .Append(Namespace)
                  .Append(',');
            }

            if (SetName is not null)
            {
                sb.Append("aerospike.setname (tag):")
                  .Append(SetName)
                  .Append(',');
            }

            if (UserKey is not null)
            {
                sb.Append("aerospike.userkey (tag):")
                  .Append(UserKey)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
    }
}

#endif
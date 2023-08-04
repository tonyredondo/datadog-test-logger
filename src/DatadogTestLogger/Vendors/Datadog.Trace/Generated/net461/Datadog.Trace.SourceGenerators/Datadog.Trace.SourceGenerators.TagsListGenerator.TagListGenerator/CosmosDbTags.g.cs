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
using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Tagging
{
    partial class CosmosDbTags
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
        // DbTypeBytes = MessagePack.Serialize("db.type");
#if NETCOREAPP
        private static ReadOnlySpan<byte> DbTypeBytes => new byte[] { 167, 100, 98, 46, 116, 121, 112, 101 };
#else
        private static readonly byte[] DbTypeBytes = new byte[] { 167, 100, 98, 46, 116, 121, 112, 101 };
#endif
        // ContainerIdBytes = MessagePack.Serialize("cosmosdb.container");
#if NETCOREAPP
        private static ReadOnlySpan<byte> ContainerIdBytes => new byte[] { 178, 99, 111, 115, 109, 111, 115, 100, 98, 46, 99, 111, 110, 116, 97, 105, 110, 101, 114 };
#else
        private static readonly byte[] ContainerIdBytes = new byte[] { 178, 99, 111, 115, 109, 111, 115, 100, 98, 46, 99, 111, 110, 116, 97, 105, 110, 101, 114 };
#endif
        // DatabaseIdBytes = MessagePack.Serialize("db.name");
#if NETCOREAPP
        private static ReadOnlySpan<byte> DatabaseIdBytes => new byte[] { 167, 100, 98, 46, 110, 97, 109, 101 };
#else
        private static readonly byte[] DatabaseIdBytes = new byte[] { 167, 100, 98, 46, 110, 97, 109, 101 };
#endif
        // HostBytes = MessagePack.Serialize("out.host");
#if NETCOREAPP
        private static ReadOnlySpan<byte> HostBytes => new byte[] { 168, 111, 117, 116, 46, 104, 111, 115, 116 };
#else
        private static readonly byte[] HostBytes = new byte[] { 168, 111, 117, 116, 46, 104, 111, 115, 116 };
#endif

        public override string? GetTag(string key)
        {
            return key switch
            {
                "span.kind" => SpanKind,
                "component" => InstrumentationName,
                "db.type" => DbType,
                "cosmosdb.container" => ContainerId,
                "db.name" => DatabaseId,
                "out.host" => Host,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "cosmosdb.container": 
                    ContainerId = value;
                    break;
                case "db.name": 
                    DatabaseId = value;
                    break;
                case "out.host": 
                    Host = value;
                    break;
                case "span.kind": 
                case "component": 
                case "db.type": 
                    Logger.Value.Warning("Attempted to set readonly tag {TagName} on {TagType}. Ignoring.", key, nameof(CosmosDbTags));
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

            if (DbType is not null)
            {
                processor.Process(new TagItem<string>("db.type", DbType, DbTypeBytes));
            }

            if (ContainerId is not null)
            {
                processor.Process(new TagItem<string>("cosmosdb.container", ContainerId, ContainerIdBytes));
            }

            if (DatabaseId is not null)
            {
                processor.Process(new TagItem<string>("db.name", DatabaseId, DatabaseIdBytes));
            }

            if (Host is not null)
            {
                processor.Process(new TagItem<string>("out.host", Host, HostBytes));
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

            if (DbType is not null)
            {
                sb.Append("db.type (tag):")
                  .Append(DbType)
                  .Append(',');
            }

            if (ContainerId is not null)
            {
                sb.Append("cosmosdb.container (tag):")
                  .Append(ContainerId)
                  .Append(',');
            }

            if (DatabaseId is not null)
            {
                sb.Append("db.name (tag):")
                  .Append(DatabaseId)
                  .Append(',');
            }

            if (Host is not null)
            {
                sb.Append("out.host (tag):")
                  .Append(Host)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
    }
}

#endif
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
    partial class AwsSdkTags
    {
        // InstrumentationNameBytes = MessagePack.Serialize("component");
#if NETCOREAPP
        private static ReadOnlySpan<byte> InstrumentationNameBytes => new byte[] { 169, 99, 111, 109, 112, 111, 110, 101, 110, 116 };
#else
        private static readonly byte[] InstrumentationNameBytes = new byte[] { 169, 99, 111, 109, 112, 111, 110, 101, 110, 116 };
#endif
        // AgentNameBytes = MessagePack.Serialize("aws.agent");
#if NETCOREAPP
        private static ReadOnlySpan<byte> AgentNameBytes => new byte[] { 169, 97, 119, 115, 46, 97, 103, 101, 110, 116 };
#else
        private static readonly byte[] AgentNameBytes = new byte[] { 169, 97, 119, 115, 46, 97, 103, 101, 110, 116 };
#endif
        // OperationBytes = MessagePack.Serialize("aws.operation");
#if NETCOREAPP
        private static ReadOnlySpan<byte> OperationBytes => new byte[] { 173, 97, 119, 115, 46, 111, 112, 101, 114, 97, 116, 105, 111, 110 };
#else
        private static readonly byte[] OperationBytes = new byte[] { 173, 97, 119, 115, 46, 111, 112, 101, 114, 97, 116, 105, 111, 110 };
#endif
        // AwsRegionBytes = MessagePack.Serialize("aws.region");
#if NETCOREAPP
        private static ReadOnlySpan<byte> AwsRegionBytes => new byte[] { 170, 97, 119, 115, 46, 114, 101, 103, 105, 111, 110 };
#else
        private static readonly byte[] AwsRegionBytes = new byte[] { 170, 97, 119, 115, 46, 114, 101, 103, 105, 111, 110 };
#endif
        // RegionBytes = MessagePack.Serialize("region");
#if NETCOREAPP
        private static ReadOnlySpan<byte> RegionBytes => new byte[] { 166, 114, 101, 103, 105, 111, 110 };
#else
        private static readonly byte[] RegionBytes = new byte[] { 166, 114, 101, 103, 105, 111, 110 };
#endif
        // RequestIdBytes = MessagePack.Serialize("aws.requestId");
#if NETCOREAPP
        private static ReadOnlySpan<byte> RequestIdBytes => new byte[] { 173, 97, 119, 115, 46, 114, 101, 113, 117, 101, 115, 116, 73, 100 };
#else
        private static readonly byte[] RequestIdBytes = new byte[] { 173, 97, 119, 115, 46, 114, 101, 113, 117, 101, 115, 116, 73, 100 };
#endif
        // AwsServiceBytes = MessagePack.Serialize("aws.service");
#if NETCOREAPP
        private static ReadOnlySpan<byte> AwsServiceBytes => new byte[] { 171, 97, 119, 115, 46, 115, 101, 114, 118, 105, 99, 101 };
#else
        private static readonly byte[] AwsServiceBytes = new byte[] { 171, 97, 119, 115, 46, 115, 101, 114, 118, 105, 99, 101 };
#endif
        // ServiceBytes = MessagePack.Serialize("aws_service");
#if NETCOREAPP
        private static ReadOnlySpan<byte> ServiceBytes => new byte[] { 171, 97, 119, 115, 95, 115, 101, 114, 118, 105, 99, 101 };
#else
        private static readonly byte[] ServiceBytes = new byte[] { 171, 97, 119, 115, 95, 115, 101, 114, 118, 105, 99, 101 };
#endif
        // HttpMethodBytes = MessagePack.Serialize("http.method");
#if NETCOREAPP
        private static ReadOnlySpan<byte> HttpMethodBytes => new byte[] { 171, 104, 116, 116, 112, 46, 109, 101, 116, 104, 111, 100 };
#else
        private static readonly byte[] HttpMethodBytes = new byte[] { 171, 104, 116, 116, 112, 46, 109, 101, 116, 104, 111, 100 };
#endif
        // HttpUrlBytes = MessagePack.Serialize("http.url");
#if NETCOREAPP
        private static ReadOnlySpan<byte> HttpUrlBytes => new byte[] { 168, 104, 116, 116, 112, 46, 117, 114, 108 };
#else
        private static readonly byte[] HttpUrlBytes = new byte[] { 168, 104, 116, 116, 112, 46, 117, 114, 108 };
#endif
        // HttpStatusCodeBytes = MessagePack.Serialize("http.status_code");
#if NETCOREAPP
        private static ReadOnlySpan<byte> HttpStatusCodeBytes => new byte[] { 176, 104, 116, 116, 112, 46, 115, 116, 97, 116, 117, 115, 95, 99, 111, 100, 101 };
#else
        private static readonly byte[] HttpStatusCodeBytes = new byte[] { 176, 104, 116, 116, 112, 46, 115, 116, 97, 116, 117, 115, 95, 99, 111, 100, 101 };
#endif

        public override string? GetTag(string key)
        {
            return key switch
            {
                "component" => InstrumentationName,
                "aws.agent" => AgentName,
                "aws.operation" => Operation,
                "aws.region" => AwsRegion,
                "region" => Region,
                "aws.requestId" => RequestId,
                "aws.service" => AwsService,
                "aws_service" => Service,
                "http.method" => HttpMethod,
                "http.url" => HttpUrl,
                "http.status_code" => HttpStatusCode,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "aws.operation": 
                    Operation = value;
                    break;
                case "region": 
                    Region = value;
                    break;
                case "aws.requestId": 
                    RequestId = value;
                    break;
                case "aws_service": 
                    Service = value;
                    break;
                case "http.method": 
                    HttpMethod = value;
                    break;
                case "http.url": 
                    HttpUrl = value;
                    break;
                case "http.status_code": 
                    HttpStatusCode = value;
                    break;
                case "component": 
                case "aws.agent": 
                case "aws.region": 
                case "aws.service": 
                    Logger.Value.Warning("Attempted to set readonly tag {TagName} on {TagType}. Ignoring.", key, nameof(AwsSdkTags));
                    break;
                default: 
                    base.SetTag(key, value);
                    break;
            }
        }

        public override void EnumerateTags<TProcessor>(ref TProcessor processor)
        {
            if (InstrumentationName is not null)
            {
                processor.Process(new TagItem<string>("component", InstrumentationName, InstrumentationNameBytes));
            }

            if (AgentName is not null)
            {
                processor.Process(new TagItem<string>("aws.agent", AgentName, AgentNameBytes));
            }

            if (Operation is not null)
            {
                processor.Process(new TagItem<string>("aws.operation", Operation, OperationBytes));
            }

            if (AwsRegion is not null)
            {
                processor.Process(new TagItem<string>("aws.region", AwsRegion, AwsRegionBytes));
            }

            if (Region is not null)
            {
                processor.Process(new TagItem<string>("region", Region, RegionBytes));
            }

            if (RequestId is not null)
            {
                processor.Process(new TagItem<string>("aws.requestId", RequestId, RequestIdBytes));
            }

            if (AwsService is not null)
            {
                processor.Process(new TagItem<string>("aws.service", AwsService, AwsServiceBytes));
            }

            if (Service is not null)
            {
                processor.Process(new TagItem<string>("aws_service", Service, ServiceBytes));
            }

            if (HttpMethod is not null)
            {
                processor.Process(new TagItem<string>("http.method", HttpMethod, HttpMethodBytes));
            }

            if (HttpUrl is not null)
            {
                processor.Process(new TagItem<string>("http.url", HttpUrl, HttpUrlBytes));
            }

            if (HttpStatusCode is not null)
            {
                processor.Process(new TagItem<string>("http.status_code", HttpStatusCode, HttpStatusCodeBytes));
            }

            base.EnumerateTags(ref processor);
        }

        protected override void WriteAdditionalTags(System.Text.StringBuilder sb)
        {
            if (InstrumentationName is not null)
            {
                sb.Append("component (tag):")
                  .Append(InstrumentationName)
                  .Append(',');
            }

            if (AgentName is not null)
            {
                sb.Append("aws.agent (tag):")
                  .Append(AgentName)
                  .Append(',');
            }

            if (Operation is not null)
            {
                sb.Append("aws.operation (tag):")
                  .Append(Operation)
                  .Append(',');
            }

            if (AwsRegion is not null)
            {
                sb.Append("aws.region (tag):")
                  .Append(AwsRegion)
                  .Append(',');
            }

            if (Region is not null)
            {
                sb.Append("region (tag):")
                  .Append(Region)
                  .Append(',');
            }

            if (RequestId is not null)
            {
                sb.Append("aws.requestId (tag):")
                  .Append(RequestId)
                  .Append(',');
            }

            if (AwsService is not null)
            {
                sb.Append("aws.service (tag):")
                  .Append(AwsService)
                  .Append(',');
            }

            if (Service is not null)
            {
                sb.Append("aws_service (tag):")
                  .Append(Service)
                  .Append(',');
            }

            if (HttpMethod is not null)
            {
                sb.Append("http.method (tag):")
                  .Append(HttpMethod)
                  .Append(',');
            }

            if (HttpUrl is not null)
            {
                sb.Append("http.url (tag):")
                  .Append(HttpUrl)
                  .Append(',');
            }

            if (HttpStatusCode is not null)
            {
                sb.Append("http.status_code (tag):")
                  .Append(HttpStatusCode)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
    }
}

#endif
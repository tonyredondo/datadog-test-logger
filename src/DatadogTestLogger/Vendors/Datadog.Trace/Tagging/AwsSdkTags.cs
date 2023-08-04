//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="AwsSdkTags.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using DatadogTestLogger.Vendors.Datadog.Trace.SourceGenerators;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Tagging
{
    internal abstract partial class AwsSdkTags : InstrumentationTags, IHasStatusCode
    {
        [Tag(Trace.Tags.InstrumentationName)]
        public string InstrumentationName => "aws-sdk";

        [Tag(Trace.Tags.AwsAgentName)]
        public string AgentName => "dotnet-aws-sdk";

        [Tag(Trace.Tags.AwsOperationName)]
        public string Operation { get; set; }

#pragma warning disable CS0618
        [Tag(Trace.Tags.AwsRegion)]
#pragma warning restore CS0618
        public string AwsRegion => Region;

        [Tag(Trace.Tags.Region)]
        public string Region { get; set; }

        [Tag(Trace.Tags.AwsRequestId)]
        public string RequestId { get; set; }

#pragma warning disable CS0618
        [Tag(Trace.Tags.AwsServiceName)]
#pragma warning restore CS0618
        public string AwsService => Service;

        [Tag(Trace.Tags.AwsService)]
        public string Service { get; set; }

        [Tag(Trace.Tags.HttpMethod)]
        public string HttpMethod { get; set; }

        [Tag(Trace.Tags.HttpUrl)]
        public string HttpUrl { get; set; }

        [Tag(Trace.Tags.HttpStatusCode)]
        public string HttpStatusCode { get; set; }
    }
}

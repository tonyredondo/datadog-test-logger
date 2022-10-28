//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="GrpcTags.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.SourceGenerators;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Tagging
{
#pragma warning disable SA1402 // File must contain single type
    internal abstract partial class GrpcTags : InstrumentationTags
    {
        public GrpcTags(string spanKind)
        {
            SpanKind = spanKind;
        }

        [Tag(Trace.Tags.SpanKind)]
        public override string SpanKind { get; }

        [Tag(Trace.Tags.InstrumentationName)]
        public string InstrumentationName => nameof(IntegrationId.Grpc);

        [Tag(Trace.Tags.GrpcMethodKind)]
        public string MethodKind { get; set; }

        [Tag(Trace.Tags.GrpcMethodName)]
        public string MethodName { get; set; }

        [Tag(Trace.Tags.GrpcMethodPath)]
        public string MethodPath { get; set; }

        [Tag(Trace.Tags.GrpcMethodPackage)]
        public string MethodPackage { get; set; }

        [Tag(Trace.Tags.GrpcMethodService)]
        public string MethodService { get; set; }

        [Tag(Trace.Tags.GrpcStatusCode)]
        public string StatusCode { get; set; }
    }

    internal partial class GrpcClientTags : GrpcTags
    {
        public GrpcClientTags()
            : base(SpanKinds.Client)
        {
        }
    }

    internal partial class GrpcServerTags : GrpcTags
    {
        public GrpcServerTags()
            : base(SpanKinds.Server)
        {
        }
    }
}

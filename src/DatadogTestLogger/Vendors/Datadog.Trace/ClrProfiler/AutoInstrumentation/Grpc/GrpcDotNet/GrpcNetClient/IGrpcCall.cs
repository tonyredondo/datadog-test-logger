//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="IGrpcCall.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable
#if !NETFRAMEWORK

using System.Net.Http;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Grpc.GrpcDotNet.GrpcNetClient
{
    /// <summary>
    /// Duck type for Grpc.Net.Client.Internal.GrpcCall{TRequest, TResponse}
    /// </summary>
    internal interface IGrpcCall
    {
        public MethodStruct Method { get; }

        public CallOptionsStruct Options { get; }

        public HttpResponseMessage? HttpResponse { get; }

        public bool TryGetTrailers(out IMetadata metadata);
    }
}
#endif

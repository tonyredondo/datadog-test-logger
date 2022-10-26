// <copyright file="NullableClientSideStatusStruct.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable

using Vendor.Datadog.Trace.DuckTyping;

namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.Grpc.GrpcLegacy.Client.DuckTypes
{
    /// <summary>
    /// Duck type for Nullable{Grpc.Core.Internal.ClientSideStatus}
    /// </summary>
    [DuckCopy]
    internal struct NullableClientSideStatusStruct
    {
        public ClientSideStatusStruct Value;
    }
}

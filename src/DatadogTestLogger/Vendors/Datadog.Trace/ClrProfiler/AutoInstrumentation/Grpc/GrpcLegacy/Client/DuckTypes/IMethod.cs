//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="IMethod.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping;

#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Grpc.GrpcLegacy.Client.DuckTypes
{
    /// <summary>
    /// Duck type for Method{TRequest, TResponse}
    /// Interface for use in constraints
    /// https://github.com/grpc/grpc/blob/master/src/csharp/Grpc.Core.Api/Method.cs
    /// </summary>
    internal interface IMethod
    {
        public string? ServiceName { get; }

        public string? Name { get; }

        public string? FullName { get; }

        [Duck(Name = "Type")]
        public int GrpcType { get; }
    }
}

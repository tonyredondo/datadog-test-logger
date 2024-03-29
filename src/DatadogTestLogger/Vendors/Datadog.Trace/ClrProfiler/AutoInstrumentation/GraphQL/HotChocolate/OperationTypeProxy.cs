//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="OperationTypeProxy.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.GraphQL.HotChocolate
{
    /// <summary>
    /// A proxy enum for GraphQL.Language.AST.OperationType.
    /// The enum values must match those of GraphQL.Language.AST.OperationType for spans
    /// to be decorated with the correct operation. Since the original type is public,
    /// we not expect changes between minor versions of the HotChocolate GraphQL library.
    /// </summary>
    internal enum OperationTypeProxy
    {
        /// <summary>
        /// A query operation.
        /// </summary>
        Query,

        /// <summary>
        /// A mutation operation.
        /// </summary>
        Mutation,

        /// <summary>
        /// A subscription operation.
        /// </summary>
        Subscription
    }
}

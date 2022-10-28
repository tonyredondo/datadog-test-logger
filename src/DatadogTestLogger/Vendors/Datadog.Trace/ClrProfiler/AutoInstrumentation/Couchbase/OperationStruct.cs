//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="OperationStruct.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System.Net;
using Datadog.Trace.Vendors.Datadog.Trace.DuckTyping;

namespace Datadog.Trace.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Couchbase
{
    /// <summary>
    /// Ducktyping of Couchbase.IO.Operations.IOperation and generic implementations
    /// </summary>
    [DuckCopy]
    internal struct OperationStruct
    {
        /// <summary>
        /// Gets the Operation Code
        /// </summary>
        public OperationCode OperationCode;

        /// <summary>
        /// Gets the Operation Key
        /// </summary>
        public string Key;

        /// <summary>
        /// Gets the Operation Code
        /// </summary>
        public IPEndPoint CurrentHost;
    }
}
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="IClientContext.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System.Collections.Generic;
using System.Net;

namespace Datadog.Trace.Vendors.Datadog.Trace.ClrProfiler.ServerlessInstrumentation.AWS
{
    internal interface IClientContext
    {
        /// <summary>
        /// Gets the datadog injected trace context
        /// Used with the datadog ducktyping library
        /// </summary>
        /// <returns>The trace context</returns>
        IDictionary<string, string> Custom { get; }
    }
}
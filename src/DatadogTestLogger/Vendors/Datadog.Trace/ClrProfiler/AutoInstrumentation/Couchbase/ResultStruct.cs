//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="ResultStruct.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Couchbase
{
    /// <summary>
    /// Ducktyping of Couchbase.IResult and generic implementations
    /// </summary>
    [DuckCopy]
    internal struct ResultStruct
    {
        /// <summary>
        /// Gets a value indicating whether the operation was succesful.
        /// </summary>
        public bool Success;

        /// <summary>
        /// Gets a message indicating why it was not succesful if the operation wasn't succesful.
        /// </summary>
        public string Message;

        /// <summary>
        /// Gets the exception, If Success is false and an exception has been caught internally
        /// </summary>
        public Exception Exception;
    }
}

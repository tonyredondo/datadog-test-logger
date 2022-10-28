//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="IFunctionInstance.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#if !NETFRAMEWORK
using System;
using System.Collections.Generic;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Azure.Functions
{
    /// <summary>
    /// For duck typing
    /// </summary>
    internal interface IFunctionInstance
    {
        /// <summary>
        /// Gets Function unique id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets trigger details
        /// </summary>
        IDictionary<string, string> TriggerDetails { get; }

        /// <summary>
        /// Gets reason for invocation
        /// </summary>
        AzureFunctionsExecutionReason Reason { get; }

        /// <summary>
        /// Gets access to the binding source object for trigger inspection
        /// </summary>
        object BindingSource { get; }

        /// <summary>
        /// Gets Function description object
        /// </summary>
        IFunctionDescriptor FunctionDescriptor { get; }
    }
}
#endif

//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="MappedDiagnosticsLogicalContextSetterProxy.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Collections.Generic;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Logging.NLog.LogsInjection
{
    /// <summary>
    /// Duck type for MappedDiagnosticsLogicalContext in NLog 4.6+
    /// </summary>
    internal class MappedDiagnosticsLogicalContextSetterProxy
    {
        /// <summary>
        /// Updates the current logical context with multiple items in single operation
        /// </summary>
        /// <param name="items">.</param>
        /// <returns>>An <see cref="IDisposable"/> that can be used to remove the item from the current logical context (null if no items).</returns>
        public virtual IDisposable SetScoped(IReadOnlyList<KeyValuePair<string, object>> items)
        {
            return null;
        }
    }
}

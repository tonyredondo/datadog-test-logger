//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="DuckFieldAttribute.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping
{
    /// <summary>
    /// Duck attribute where the underlying member is a field
    /// </summary>
    internal class DuckFieldAttribute : DuckAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuckFieldAttribute"/> class.
        /// </summary>
        public DuckFieldAttribute()
        {
            Kind = DuckKind.Field;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="ITimestamp.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Kafka
{
    /// <summary>
    /// Timestamp struct for duck-typing
    /// Requires boxing, but necessary as we need to duck-type <see cref="Type"/> too
    /// </summary>
    internal interface ITimestamp
    {
        /// <summary>
        /// Gets the timestamp type
        /// </summary>
        public int Type { get; }

        /// <summary>
        /// Gets the UTC DateTime for the timestamp
        /// </summary>
        public DateTime UtcDateTime { get; }
    }
}

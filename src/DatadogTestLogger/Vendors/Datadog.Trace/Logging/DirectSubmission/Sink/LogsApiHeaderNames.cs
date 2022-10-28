//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="LogsApiHeaderNames.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System.Collections.Generic;

#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.Logging.DirectSubmission.Sink
{
    internal static class LogsApiHeaderNames
    {
        /// <summary>
        /// Gets the default constant header that should be added to any request to the agent
        /// </summary>
        internal static KeyValuePair<string, string>[] DefaultHeaders { get; } =
        {
            new(HttpHeaderNames.TracingEnabled, "false"), // don't add automatic instrumentation to requests directed to the agent
        };
    }
}

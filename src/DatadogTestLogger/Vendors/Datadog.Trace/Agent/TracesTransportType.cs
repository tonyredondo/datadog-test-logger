//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="TracesTransportType.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

namespace DatadogTestLogger.Vendors.Datadog.Trace.Agent
{
    /// <summary>
    /// Available types of transports.
    /// </summary>
    internal enum TracesTransportType
    {
        /// <summary>
        /// Default transport.
        /// Defers transport logic to agent API.
        /// </summary>
        Default,

        /// <summary>
        /// Windows Named Pipe strategy.
        /// Transport used primarily for Azure App Service.
        /// </summary>
        WindowsNamedPipe,

        /// <summary>
        /// Unix Domain Socket strategy.
        /// Transport used primarily for kubernetes
        /// </summary>
        UnixDomainSocket
    }
}

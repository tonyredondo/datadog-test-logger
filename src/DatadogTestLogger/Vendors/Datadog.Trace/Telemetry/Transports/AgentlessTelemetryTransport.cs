//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="AgentlessTelemetryTransport.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable
using Datadog.Trace.Vendors.Datadog.Trace.Agent;

namespace Datadog.Trace.Vendors.Datadog.Trace.Telemetry.Transports;

internal class AgentlessTelemetryTransport : JsonTelemetryTransport
{
    public AgentlessTelemetryTransport(IApiRequestFactory requestFactory)
        : base(requestFactory)
    {
    }

    public override string GetTransportInfo() => nameof(AgentTelemetryTransport) + " to " + GetEndpointInfo();
}
// <copyright file="AgentlessTelemetryTransport.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable
using Vendor.Datadog.Trace.Agent;

namespace Vendor.Datadog.Trace.Telemetry.Transports;

internal class AgentlessTelemetryTransport : JsonTelemetryTransport
{
    public AgentlessTelemetryTransport(IApiRequestFactory requestFactory)
        : base(requestFactory)
    {
    }

    public override string GetTransportInfo() => nameof(AgentTelemetryTransport) + " to " + GetEndpointInfo();
}

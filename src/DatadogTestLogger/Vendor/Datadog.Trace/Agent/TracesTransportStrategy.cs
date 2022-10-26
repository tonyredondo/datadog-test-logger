// <copyright file="TracesTransportStrategy.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using Vendor.Datadog.Trace.Configuration;
using Vendor.Datadog.Trace.HttpOverStreams;

namespace Vendor.Datadog.Trace.Agent
{
    internal static class TracesTransportStrategy
    {
        public static IApiRequestFactory Get(ImmutableExporterSettings settings)
            => AgentTransportStrategy.Get(
                settings,
                productName: "trace",
                tcpTimeout: null,
                AgentHttpHeaderNames.DefaultHeaders,
                () => new TraceAgentHttpHeaderHelper(),
                uri => uri);
    }
}

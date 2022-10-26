﻿// <copyright file="TelemetryTransportStrategy.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using Vendor.Datadog.Trace.Agent;
using Vendor.Datadog.Trace.Agent.Transports;
using Vendor.Datadog.Trace.Configuration;
using Vendor.Datadog.Trace.HttpOverStreams;
using Vendor.Datadog.Trace.Logging;
using Vendor.Datadog.Trace.Util;

namespace Vendor.Datadog.Trace.Telemetry.Transports;

internal static class TelemetryTransportStrategy
{
    private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor<Tracer>();
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(15);

    public static IApiRequestFactory GetDirectIntakeFactory(Uri baseEndpoint, string apiKey)
    {
#if NETCOREAPP
        Log.Information("Using {FactoryType} for telemetry transport direct to intake.", nameof(HttpClientRequestFactory));
        return new HttpClientRequestFactory(baseEndpoint, TelemetryHttpHeaderNames.GetDefaultIntakeHeaders(apiKey), timeout: Timeout);
#else
        Log.Information("Using {FactoryType} for telemetry transport direct to intake.", nameof(ApiWebRequestFactory));
        return new ApiWebRequestFactory(baseEndpoint, TelemetryHttpHeaderNames.GetDefaultIntakeHeaders(apiKey), timeout: Timeout);
#endif
    }

    public static IApiRequestFactory GetAgentIntakeFactory(ImmutableExporterSettings settings)
        => AgentTransportStrategy.Get(
            settings,
            productName: "telemetry",
            tcpTimeout: Timeout,
            TelemetryHttpHeaderNames.GetDefaultAgentHeaders(),
            () => new TelemetryAgentHttpHeaderHelper(),
            uri => UriHelpers.Combine(uri, TelemetryConstants.AgentTelemetryEndpoint));
}

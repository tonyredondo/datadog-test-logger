// <copyright file="RemoteConfigurationApiFactory.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
#nullable enable

using System;
using Vendor.Datadog.Trace.Agent;
using Vendor.Datadog.Trace.Agent.DiscoveryService;
using Vendor.Datadog.Trace.Configuration;
using Vendor.Datadog.Trace.HttpOverStreams;

namespace Vendor.Datadog.Trace.RemoteConfigurationManagement.Transport
{
    internal class RemoteConfigurationApiFactory
    {
        public static IRemoteConfigurationApi Create(ImmutableExporterSettings exporterSettings, RemoteConfigurationSettings remoteConfigurationSettings, IDiscoveryService discoveryService)
        {
            var apiRequestFactory = AgentTransportStrategy.Get(
                exporterSettings,
                productName: "rcm",
                tcpTimeout: TimeSpan.FromSeconds(15),
                AgentHttpHeaderNames.MinimalHeaders,
                () => new MinimalAgentHeaderHelper(),
                uri => uri);

            return RemoteConfigurationApi.Create(apiRequestFactory, discoveryService);
        }
    }
}

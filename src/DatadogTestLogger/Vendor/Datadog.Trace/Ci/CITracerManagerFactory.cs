// <copyright file="CITracerManagerFactory.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using Vendor.Datadog.Trace.Agent;
using Vendor.Datadog.Trace.Agent.DiscoveryService;
using Vendor.Datadog.Trace.Ci.Agent;
using Vendor.Datadog.Trace.Ci.Configuration;
using Vendor.Datadog.Trace.Ci.Sampling;
using Vendor.Datadog.Trace.Configuration;
using Vendor.Datadog.Trace.DataStreamsMonitoring;
using Vendor.Datadog.Trace.Logging;
using Vendor.Datadog.Trace.Logging.DirectSubmission;
using Vendor.Datadog.Trace.RuntimeMetrics;
using Vendor.Datadog.Trace.Sampling;
using Vendor.Datadog.Trace.Telemetry;
using Vendor.Datadog.Trace.Vendors.StatsdClient;

namespace Vendor.Datadog.Trace.Ci
{
    internal class CITracerManagerFactory : TracerManagerFactory
    {
        private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor<CITracerManagerFactory>();
        private readonly CIVisibilitySettings _settings;
        private readonly IDiscoveryService _discoveryService;
        private readonly bool _enabledEventPlatformProxy;

        public CITracerManagerFactory(CIVisibilitySettings settings, IDiscoveryService discoveryService, bool enabledEventPlatformProxy = false)
        {
            _settings = settings;
            _discoveryService = discoveryService;
            _enabledEventPlatformProxy = enabledEventPlatformProxy;
        }

        protected override TracerManager CreateTracerManagerFrom(
            ImmutableTracerSettings settings,
            IAgentWriter agentWriter,
            ITraceSampler sampler,
            ISpanSampler spanSampler,
            IScopeManager scopeManager,
            IDogStatsd statsd,
            RuntimeMetricsWriter runtimeMetrics,
            DirectLogSubmissionManager logSubmissionManager,
            ITelemetryController telemetry,
            IDiscoveryService discoveryService,
            DataStreamsManager dataStreamsManager,
            string defaultServiceName)
        {
            return new CITracerManager(settings, agentWriter, sampler, spanSampler, scopeManager, statsd, runtimeMetrics, logSubmissionManager, telemetry, discoveryService, dataStreamsManager, defaultServiceName);
        }

        protected override ITraceSampler GetSampler(ImmutableTracerSettings settings)
        {
            return new CISampler();
        }

        protected override IAgentWriter GetAgentWriter(ImmutableTracerSettings settings, IDogStatsd statsd, ITraceSampler sampler, IDiscoveryService discoveryService)
        {
            // Check for agentless scenario
            if (_settings.Agentless)
            {
                if (!string.IsNullOrEmpty(_settings.ApiKey))
                {
                    return new CIVisibilityProtocolWriter(_settings, new CIWriterHttpSender(CIVisibility.GetRequestFactory(settings)));
                }

                Environment.FailFast("An API key is required in Agentless mode.");
                return null;
            }

            // With agent scenario:
            if (_enabledEventPlatformProxy)
            {
                return new CIVisibilityProtocolWriter(_settings, new CIWriterHttpSender(CIVisibility.GetRequestFactory(settings)));
            }

            // Event platform proxy not found
            // Set the tracer buffer size to the max
            var traceBufferSize = 1024 * 1024 * 45; // slightly lower than the 50mb payload agent limit.
            return new ApmAgentWriter(settings, sampler, discoveryService, traceBufferSize);
        }

        protected override IDiscoveryService GetDiscoveryService(ImmutableTracerSettings settings)
            => _discoveryService;
    }
}

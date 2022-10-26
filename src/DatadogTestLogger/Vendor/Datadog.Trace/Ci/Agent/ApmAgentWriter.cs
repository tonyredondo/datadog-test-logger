// <copyright file="ApmAgentWriter.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Threading.Tasks;
using Vendor.Datadog.Trace.Agent;
using Vendor.Datadog.Trace.Agent.DiscoveryService;
using Vendor.Datadog.Trace.Ci.EventModel;
using Vendor.Datadog.Trace.Configuration;
using Vendor.Datadog.Trace.Sampling;

namespace Vendor.Datadog.Trace.Ci.Agent
{
    /// <summary>
    /// APM Agent Writer for CI Visibility
    /// </summary>
    internal class ApmAgentWriter : IEventWriter
    {
        private const int DefaultMaxBufferSize = 1024 * 1024 * 10;

        [ThreadStatic]
        private static Span[] _spanArray;
        private readonly AgentWriter _agentWriter;

        public ApmAgentWriter(ImmutableTracerSettings settings, ITraceSampler sampler, IDiscoveryService discoveryService, int maxBufferSize = DefaultMaxBufferSize)
        {
            var partialFlushEnabled = settings.Exporter.PartialFlushEnabled;
            var apiRequestFactory = TracesTransportStrategy.Get(settings.Exporter);
            var api = new Api(apiRequestFactory, null, rates => sampler.SetDefaultSampleRates(rates), partialFlushEnabled);
            var statsAggregator = StatsAggregator.Create(api, settings, discoveryService);

            _agentWriter = new AgentWriter(api, statsAggregator, null, maxBufferSize: maxBufferSize);
        }

        public ApmAgentWriter(IApi api, int maxBufferSize = DefaultMaxBufferSize)
        {
            _agentWriter = new AgentWriter(api, null, null, maxBufferSize: maxBufferSize);
        }

        public void WriteEvent(IEvent @event)
        {
            // To keep compatibility with the agent version of the payload, any IEvent conversion to span
            // goes here.

            if (_spanArray is not { } spanArray)
            {
                spanArray = new Span[1];
                _spanArray = spanArray;
            }

            if (CIVisibilityEventsFactory.GetSpan(@event) is { } span)
            {
                spanArray[0] = span;
                WriteTrace(new ArraySegment<Span>(spanArray));
            }
        }

        public Task FlushAndCloseAsync()
        {
            return _agentWriter.FlushAndCloseAsync();
        }

        public Task FlushTracesAsync()
        {
            return _agentWriter.FlushTracesAsync();
        }

        public Task<bool> Ping()
        {
            return _agentWriter.Ping();
        }

        public void WriteTrace(ArraySegment<Span> trace)
        {
            _agentWriter.WriteTrace(trace);
        }
    }
}

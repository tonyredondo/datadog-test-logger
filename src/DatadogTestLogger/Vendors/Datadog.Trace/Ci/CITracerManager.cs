//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="CITracerManager.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Runtime.CompilerServices;
using DatadogTestLogger.Vendors.Datadog.Trace.Agent;
using DatadogTestLogger.Vendors.Datadog.Trace.Agent.DiscoveryService;
using DatadogTestLogger.Vendors.Datadog.Trace.Ci.Agent;
using DatadogTestLogger.Vendors.Datadog.Trace.Ci.EventModel;
using DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.DataStreamsMonitoring;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging.DirectSubmission;
using DatadogTestLogger.Vendors.Datadog.Trace.RuntimeMetrics;
using DatadogTestLogger.Vendors.Datadog.Trace.Sampling;
using DatadogTestLogger.Vendors.Datadog.Trace.Telemetry;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Ci
{
    internal class CITracerManager : TracerManager, ILockedTracer
    {
        private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor<CITracerManager>();

        public CITracerManager(ImmutableTracerSettings settings, IAgentWriter agentWriter, ITraceSampler sampler, IScopeManager scopeManager, IDogStatsd statsd, RuntimeMetricsWriter runtimeMetricsWriter, DirectLogSubmissionManager logSubmissionManager, ITelemetryController telemetry, IDiscoveryService discoveryService, DataStreamsManager dataStreamsManager, string defaultServiceName, IGitMetadataTagsProvider gitMetadataTagsProvider)
            : base(settings, agentWriter, sampler, scopeManager, statsd, runtimeMetricsWriter, logSubmissionManager, telemetry, discoveryService, dataStreamsManager, defaultServiceName, gitMetadataTagsProvider, GetProcessors(settings.Exporter.PartialFlushEnabled, agentWriter is CIVisibilityProtocolWriter))
        {
        }

        private static Trace.Processors.ITraceProcessor[] GetProcessors(bool partialFlushEnabled, bool isCiVisibilityProtocol)
        {
            if (isCiVisibilityProtocol)
            {
                return new Trace.Processors.ITraceProcessor[]
                {
                    new Trace.Processors.NormalizerTraceProcessor(),
                    new Trace.Processors.TruncatorTraceProcessor(),
                    new Processors.OriginTagTraceProcessor(partialFlushEnabled, true),
                };
            }

            return new Trace.Processors.ITraceProcessor[]
            {
                new Trace.Processors.NormalizerTraceProcessor(),
                new Trace.Processors.TruncatorTraceProcessor(),
                new Processors.TestSuiteVisibilityProcessor(),
                new Processors.OriginTagTraceProcessor(partialFlushEnabled, false),
            };
        }

        private Span ProcessSpan(Span span)
        {
            if (span is null)
            {
                return span;
            }

            foreach (var processor in TraceProcessors)
            {
                if (processor is null)
                {
                    continue;
                }

                try
                {
                    span = processor.Process(span);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error executing trace processor {TraceProcessorType}", processor?.GetType());
                }
            }

            return span;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteEvent(IEvent @event)
        {
            if (@event is TestEvent testEvent)
            {
                testEvent.Content = ProcessSpan(testEvent.Content);
            }
            else if (@event is SpanEvent spanEvent)
            {
                spanEvent.Content = ProcessSpan(spanEvent.Content);
            }

            ((IEventWriter)AgentWriter).WriteEvent(@event);
        }
    }
}

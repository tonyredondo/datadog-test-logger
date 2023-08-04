//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="OriginTagTraceProcessor.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using DatadogTestLogger.Vendors.Datadog.Trace.Ci.Tags;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging;
using DatadogTestLogger.Vendors.Datadog.Trace.Processors;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Ci.Processors
{
    internal class OriginTagTraceProcessor : ITraceProcessor
    {
        private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor<OriginTagTraceProcessor>();

        private readonly bool _isPartialFlushEnabled = false;
        private readonly bool _isCiVisibilityProtocol = false;

        public OriginTagTraceProcessor(bool isPartialFlushEnabled, bool isCiVisibilityProtocol)
        {
            _isPartialFlushEnabled = isPartialFlushEnabled;
            _isCiVisibilityProtocol = isCiVisibilityProtocol;

            Log.Information("OriginTraceProcessor initialized.");
        }

        public ArraySegment<Span> Process(ArraySegment<Span> trace)
        {
            // We ensure there's no trace (local root span) without a test tag.
            // And ensure all other spans have the origin tag.

            // Check if the trace has any span
            if (trace.Count == 0)
            {
                // No trace to write
                return trace;
            }

            if (!_isPartialFlushEnabled)
            {
                // Check if the root span is a test, benchmark or build span
                for (var i = trace.Offset + trace.Count - 1; i >= trace.Offset; i--)
                {
                    var span = trace.Array![i];
                    if (span.Context.ParentInternal is null &&
                        span.Type != SpanTypes.Test &&
                        span.Type != SpanTypes.TestSuite &&
                        span.Type != SpanTypes.TestModule &&
                        span.Type != SpanTypes.TestSession &&
                        span.Type != SpanTypes.Benchmark &&
                        span.Type != SpanTypes.Build)
                    {
                        Log.Warning<int>("Spans dropped because not having a test or benchmark root span: {Count}", trace.Count);
                        return default;
                    }
                }
            }

            if (!_isCiVisibilityProtocol)
            {
                // Sets the origin tag on the TraceContext to ensure the CI track.
                var traceContext = trace.Array![trace.Offset].Context.TraceContext;

                if (traceContext is not null)
                {
                    traceContext.Origin = TestTags.CIAppTestOriginName;
                }
            }

            return trace;
        }

        public Span Process(Span span)
        {
            // Sets the origin tag on the TraceContext to ensure the CI track.
            var traceContext = span.Context.TraceContext;

            if (traceContext is not null)
            {
                traceContext.Origin = TestTags.CIAppTestOriginName;
            }

            return span;
        }

        public ITagProcessor GetTagProcessor()
        {
            return null;
        }
    }
}

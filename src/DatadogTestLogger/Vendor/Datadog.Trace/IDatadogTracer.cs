// <copyright file="IDatadogTracer.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using Vendor.Datadog.Trace.Configuration;
using Vendor.Datadog.Trace.Sampling;

namespace Vendor.Datadog.Trace
{
    /// <summary>
    /// Internal interface used for mocking the Tracer in <see cref="TraceContext"/>, its associated tests,
    /// and the AgentWriterTests
    /// </summary>
    internal interface IDatadogTracer
    {
        string DefaultServiceName { get; }

        ITraceSampler Sampler { get; }

        ISpanSampler SpanSampler { get; }

        ImmutableTracerSettings Settings { get; }

        void Write(ArraySegment<Span> span);
    }
}

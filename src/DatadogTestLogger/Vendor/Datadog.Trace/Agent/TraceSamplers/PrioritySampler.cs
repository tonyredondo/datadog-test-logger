// <copyright file="PrioritySampler.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using Vendor.Datadog.Trace.Util;

namespace Vendor.Datadog.Trace.Agent.TraceSamplers
{
    internal class PrioritySampler : ITraceChunkSampler
    {
        public bool Sample(ArraySegment<Span> trace) =>
            SamplingHelpers.IsKeptBySamplingPriority(trace);
    }
}

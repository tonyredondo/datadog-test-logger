// <copyright file="IProfilerStatus.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

namespace Vendor.Datadog.Trace.ContinuousProfiler
{
    internal interface IProfilerStatus
    {
        bool IsProfilerReady { get; }
    }
}

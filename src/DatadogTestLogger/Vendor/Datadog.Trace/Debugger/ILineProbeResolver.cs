// <copyright file="ILineProbeResolver.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using Vendor.Datadog.Trace.Debugger.Configurations.Models;
using Vendor.Datadog.Trace.Debugger.Models;

namespace Vendor.Datadog.Trace.Debugger
{
    /// <summary>
    /// Matches a source file path with the assembly and pdb files that correlate to it,
    /// and resolves the line probe's line number to a byte code offset.
    /// </summary>
    internal interface ILineProbeResolver
    {
        LineProbeResolveResult TryResolveLineProbe(ProbeDefinition probe, out BoundLineProbeLocation location);
    }
}

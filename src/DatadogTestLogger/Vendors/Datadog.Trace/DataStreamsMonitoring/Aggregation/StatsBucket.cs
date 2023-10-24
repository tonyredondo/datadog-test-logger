//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="StatsBucket.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable
using DatadogTestLogger.Vendors.Datadog.Trace.DataStreamsMonitoring.Hashes;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Datadog.Sketches;

namespace DatadogTestLogger.Vendors.Datadog.Trace.DataStreamsMonitoring.Aggregation;

internal readonly struct StatsBucket
{
    public readonly string[] EdgeTags;
    public readonly PathwayHash Hash;
    public readonly PathwayHash ParentHash;
    public readonly DDSketch PathwayLatency;
    public readonly DDSketch EdgeLatency;
    public readonly DDSketch PayloadSize;

    public StatsBucket(string[] edgeTags, PathwayHash hash, PathwayHash parentHash, DDSketch pathwayLatency, DDSketch edgeLatency, DDSketch payloadSize)
    {
        EdgeTags = edgeTags;
        Hash = hash;
        ParentHash = parentHash;
        PathwayLatency = pathwayLatency;
        EdgeLatency = edgeLatency;
        PayloadSize = payloadSize;
    }
}

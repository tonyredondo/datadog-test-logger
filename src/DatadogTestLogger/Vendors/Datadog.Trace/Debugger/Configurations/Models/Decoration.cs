//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="Decoration.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#pragma warning disable SA1402 // FileMayOnlyContainASingleType - StyleCop did not enforce this for records initially

namespace DatadogTestLogger.Vendors.Datadog.Trace.Debugger.Configurations.Models;

internal record Decoration
{
    public SnapshotSegment When { get; set; }

    public Tags[] Tags { get; set; }
}

internal record Tags
{
    public string Name { get; set; }

    public TagValue Value { get; set; }
}

internal record TagValue
{
    public string Template { get; set; }

    public SnapshotSegment[] Segments { get; set; }
}

#pragma warning restore SA1402 // FileMayOnlyContainASingleType - StyleCop did not enforce this for records initially
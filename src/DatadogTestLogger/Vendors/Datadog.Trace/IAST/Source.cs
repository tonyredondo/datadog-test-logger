//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="Source.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.Iast;

internal readonly struct Source
{
    public Source(byte origin, string name, string value)
    {
        this.Origin = origin;
        this.Name = name;
        this.Value = value;
    }

    public byte Origin { get; }

    public string Name { get; }

    public string Value { get; }

    public override int GetHashCode()
    {
        return IastUtils.GetHashCode(new object[] { Origin, Name, Value });
    }
}

//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="DependencyTelemetryData.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable
namespace DatadogTestLogger.Vendors.Datadog.Trace.Telemetry
{
    /// <summary>
    /// Using a record as used as dictionary key so getting equality comparison for free
    /// </summary>
    internal record DependencyTelemetryData
    {
        public DependencyTelemetryData(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string? Version { get; set; }

        public string? Hash { get; set; }
    }
}

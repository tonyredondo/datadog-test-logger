//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="DoubleExtensions.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

namespace DatadogTestLogger.Vendors.Datadog.Trace.ExtensionMethods;

internal static class DoubleExtensions
{
    /// <summary>
    /// Ensure any double value is a valid percentage number in the range [0,100]
    /// </summary>
    /// <param name="value">Original percentage value</param>
    /// <returns>Sanitized value</returns>
    public static double ToValidPercentage(this double value)
    {
        if (double.IsNaN(value) || value < 0)
        {
            return 0;
        }

        return value > 100 ? 100 : value;
    }
}
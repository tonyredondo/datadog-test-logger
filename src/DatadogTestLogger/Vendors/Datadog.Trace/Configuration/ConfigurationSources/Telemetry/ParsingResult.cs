//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="ParsingResult.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

namespace DatadogTestLogger.Vendors.Datadog.Trace.Configuration.ConfigurationSources.Telemetry;

internal readonly record struct ParsingResult<T>
{
    private ParsingResult(T result, bool isValid)
    {
        Result = result;
        IsValid = isValid;
    }

    /// <summary>
    /// Gets the extracted configuration value, if parsing was successful
    /// </summary>
    public T Result { get; }

    /// <summary>
    /// Gets a value indicating whether parsing was successful, and so whether <see cref="Result"/> contains a valid value
    /// </summary>
    public bool IsValid { get; }

    public static implicit operator ParsingResult<T>(T result) => Success(result);

    public static ParsingResult<T> Success(T result) => new(result, isValid: true);

    public static ParsingResult<T> Failure() => new(default, isValid: false);
}
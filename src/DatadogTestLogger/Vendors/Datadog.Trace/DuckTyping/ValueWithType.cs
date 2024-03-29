//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="ValueWithType.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable
using System;
using System.ComponentModel;

namespace DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping;

/// <summary>
/// DuckType return value with original type
/// </summary>
/// <typeparam name="TProxy">Type of ducktype proxy</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal readonly struct ValueWithType<TProxy>
{
    /// <summary>
    /// Gets the value
    /// </summary>
    public readonly TProxy? Value;

    /// <summary>
    /// Gets the Type of the value
    /// </summary>
    public readonly Type Type;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueWithType{TProxy}"/> struct.
    /// </summary>
    /// <param name="value">Value of the proxy instance</param>
    /// <param name="type">Type of the original value</param>
    private ValueWithType(TProxy? value, Type type)
    {
        Value = value;
        Type = type;
    }

    /// <summary>
    /// Create Value with original Type
    /// </summary>
    /// <param name="value">Value of the proxy instance</param>
    /// <param name="type">Type of the original value</param>
    /// <returns>Instance of the value with the original type</returns>
    public static ValueWithType<TProxy> Create(TProxy? value, Type type)
    {
        return new ValueWithType<TProxy>(value, type);
    }
}

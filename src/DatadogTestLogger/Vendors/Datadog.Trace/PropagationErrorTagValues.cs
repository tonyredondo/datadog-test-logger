//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="PropagationErrorTagValues.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

namespace Datadog.Trace.Vendors.Datadog.Trace;

/// <summary>
/// Error names used in `_dd.propagation_error` as trace-level tag.
/// </summary>
internal static class PropagationErrorTagValues
{
    internal const string ExtractMaxSize = "extract_max_size";

    internal const string InjectMaxSize = "inject_max_size";

    internal const string EncodingError = "encoding_error";

    internal const string DecodingError = "decoding_error";

    internal const string PropagationDisabled = "disabled";
}
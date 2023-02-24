//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="TaggingUtils.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace;

internal class TaggingUtils
{
    internal static Action<string, string> GetSpanSetter(ISpan span)
    {
        return GetSpanSetter(span, out var _);
    }

    internal static Action<string, string> GetSpanSetter(ISpan span, out Span spanClass)
    {
        TraceContext traceContext = null;
        if (span is Span spanClassTemp)
        {
            spanClass = spanClassTemp;
            traceContext = spanClass.Context.TraceContext;
        }
        else
        {
            spanClass = null;
        }

        Action<string, string> setTag =
            traceContext != null
                ? (name, value) => traceContext.Tags.SetTag(name, value)
                : (name, value) => span.SetTag(name, value);
        return setTag;
    }
}
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="LinkedListExtensions.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable
using System.Collections.Generic;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Iast.Helpers;

internal static class LinkedListExtensions
{
    public static T? Poll<T>(this LinkedList<T>? list)
        where T : struct
   {
        if (list != null && list.First != null)
        {
            var res = list.First.Value;
            list.RemoveFirst();
            return res;
        }

        return null;
    }

    public static T? Peek<T>(this LinkedList<T>? list)
        where T : struct
    {
        if (list != null && list.First != null)
        {
            var res = list.First.Value;
            return res;
        }

        return null;
    }

    public static bool IsEmpty<T>(this LinkedList<T>? list)
    {
        return list == null || list.Count == 0;
    }
}

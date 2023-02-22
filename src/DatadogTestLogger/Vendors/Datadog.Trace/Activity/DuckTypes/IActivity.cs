//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="IActivity.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable

using System;
using System.Collections.Generic;
using DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Activity.DuckTypes
{
    internal interface IActivity : IDuckType
    {
        // The corresponding property on the Activity object is nullable,
        // but we are guaranteed that the value will not be null once the Activity is started.
        // Since we are only accessing the Activity after it has started, we can treat the value
        // as never having a null value.
        string Id { get; }

        string? ParentId { get; }

        // The corresponding property on the Activity object is nullable,
        // but we are guaranteed that the value will not be null once the Activity is started.
        // Since we are only accessing the Activity after it has started, we can treat the value
        // as never having a null value.
        string RootId { get; }

        TimeSpan Duration { get; }

        string? OperationName { get; }

        IActivity? Parent { get; }

        DateTime StartTimeUtc { get; }

        IEnumerable<KeyValuePair<string, string>> Baggage { get; }

        IEnumerable<KeyValuePair<string, string>> Tags { get; }

        object AddBaggage(string key, string value);

        object AddTag(string key, string value);

        string GetBaggageItem(string key);

        object SetEndTime(DateTime endTimeUtc);

        object SetParentId(string parentId);

        object SetStartTime(DateTime startTimeUtc);
    }
}

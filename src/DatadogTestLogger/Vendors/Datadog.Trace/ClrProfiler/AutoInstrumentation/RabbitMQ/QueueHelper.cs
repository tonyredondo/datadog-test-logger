//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="QueueHelper.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable

using System;
using System.Runtime.CompilerServices;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.RabbitMQ;

internal class QueueHelper
{
    // A map between RabbitMQ Consumer<TKey,TValue> and the corresponding queue
    private static readonly ConditionalWeakTable<object, string> ConsumerToQueueMap = new();

    public static void SetQueue(object consumer, string queue)
    {
#if NETCOREAPP3_1_OR_GREATER
        ConsumerToQueueMap.AddOrUpdate(consumer, queue);
#else
        ConsumerToQueueMap.GetValue(consumer, x => queue);
#endif
    }

    public static bool TryGetQueue(object consumer, out string? queue)
    {
        queue = null;

        return ConsumerToQueueMap.TryGetValue(consumer, out queue);
    }
}
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="KafkaConsumerCommitIntegration.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.CallTarget;
using DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping;
#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Kafka;

/// <summary>
/// Confluent.Kafka Consumer.Commit calltarget instrumentation
/// </summary>
[InstrumentMethod(
    AssemblyName = "Confluent.Kafka",
    TypeName = "Confluent.Kafka.Consumer`2",
    MethodName = "Commit",
    ReturnTypeName = ClrNames.Void,
    ParameterTypeNames = new[] { KafkaConstants.TopicPartitionOffsetEnumerableTypeName },
    MinimumVersion = "1.4.0",
    MaximumVersion = "2.*.*",
    IntegrationName = KafkaConstants.IntegrationName)]
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal class KafkaConsumerCommitIntegration
{
    internal static CallTargetState OnMethodBegin<TTarget>(TTarget instance, object offsets)
    {
        return new CallTargetState(null, offsets);
    }

    internal static CallTargetReturn OnMethodEnd<TTarget>(TTarget instance, Exception? exception, in CallTargetState state)
    {
        var dataStreams = Tracer.Instance.TracerManager.DataStreamsManager;
        if (exception is null && state.State is IEnumerable<object> offsets && dataStreams.IsEnabled && instance != null)
        {
            ConsumerCache.TryGetConsumerGroup(instance, out var groupId, out var _);

            foreach (var offset in offsets)
            {
                if (offset.TryDuckCast<ITopicPartitionOffset>(out var item))
                {
                    dataStreams.TrackBacklog(
                        $"consumer_group:{groupId},partition:{item.Partition.Value},topic:{item.Topic},type:kafka_commit",
                        item.Offset.Value);
                }
            }
        }

        return CallTargetReturn.GetDefault();
    }
}

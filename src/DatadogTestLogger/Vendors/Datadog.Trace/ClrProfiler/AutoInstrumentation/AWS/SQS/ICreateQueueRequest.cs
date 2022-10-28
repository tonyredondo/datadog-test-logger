//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="ICreateQueueRequest.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System.Collections.Generic;

namespace Datadog.Trace.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.AWS.SQS
{
    /// <summary>
    /// CreateQueueRequest interface for ducktyping
    /// </summary>
    internal interface ICreateQueueRequest
    {
        /// <summary>
        /// Gets the name of the queue
        /// </summary>
        string QueueName { get; }

        /// <summary>
        /// Gets the message attributes
        /// </summary>
        Dictionary<string, string> Attributes { get; }
    }
}
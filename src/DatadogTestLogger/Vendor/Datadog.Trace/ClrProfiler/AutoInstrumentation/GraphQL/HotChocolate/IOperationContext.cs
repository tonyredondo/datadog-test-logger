// <copyright file="IOperationContext.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using Vendor.Datadog.Trace.DuckTyping;

namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.GraphQL.HotChocolate
{
    /// <summary>
    /// HotChocolate.Execution.Processing.IOperationContext interface for ducktyping
    /// </summary>
    internal interface IOperationContext
    {
        ///// <summary>
        ///// Gets the context operation
        ///// </summary>
        PreparedOperationStruct Operation { get; }
    }
}

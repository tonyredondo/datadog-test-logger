// <copyright file="ILockedTracer.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

namespace Vendor.Datadog.Trace
{
    /// <summary>
    /// Defines a tracer that cannot be replaced once is setted in the singleton
    /// </summary>
    internal interface ILockedTracer
    {
    }
}

// <copyright file="DuckIncludeAttribute.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable

using System;

namespace Vendor.Datadog.Trace.DuckTyping
{
    /// <summary>
    /// Use to include a member that would normally be ignored
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class DuckIncludeAttribute : Attribute
    {
    }
}

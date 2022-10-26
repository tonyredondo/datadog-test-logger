// <copyright file="IHttpContext.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System.Collections;
using Vendor.Datadog.Trace.DuckTyping;

namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.AspNet
{
    /// <summary>
    /// System.Web.HttpContext interface for ducktyping
    /// </summary>
    [DuckCopy]
    internal struct IHttpContext
    {
        /// <summary>
        /// Gets the items dictionary
        /// </summary>
        public IDictionary Items;
    }
}

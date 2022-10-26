﻿// <copyright file="ITargetListProxy.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using Vendor.Datadog.Trace.DuckTyping;

namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.Logging.NLog.DirectSubmission.Proxies.Pre43
{
    /// <summary>
    /// Duck type for IList&lt;LoggingRule&gt;
    /// </summary>
    internal interface ITargetListProxy
    {
        /// <summary>
        /// Adds the logging rule to the collection
        /// </summary>
        /// <param name="item">The logging rule to add to the collection</param>
        [Duck(ParameterTypeNames = new[] { "NLog.Targets.Target, NLog" })]
        public void Add(object item);
    }
}

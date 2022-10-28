//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="ILogEventInfoProxyBase.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Logging.NLog.DirectSubmission.Proxies
{
    /// <summary>
    /// Duck type for LogEventInfo  for NLog &lt; 4.5
    /// Using interface members, as will need to be boxed, so no advantage from using a struct
    /// </summary>
    internal interface ILogEventInfoProxyBase
    {
        /// <summary>
        /// Gets the timestamp
        /// </summary>
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Gets the log level
        /// </summary>
        public LogLevelProxy Level { get; }

        /// <summary>
        /// Gets the exception
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Gets the formatted message.
        /// </summary>
        public string FormattedMessage { get; }

        /// <summary>
        /// Gets the log message including any parameter placeholders.
        /// </summary>
        public string Message { get; }
    }
}

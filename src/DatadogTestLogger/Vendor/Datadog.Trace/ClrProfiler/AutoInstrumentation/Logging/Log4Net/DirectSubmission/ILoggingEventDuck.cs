﻿// <copyright file="ILoggingEventDuck.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;

namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.Logging.Log4Net.DirectSubmission
{
    /// <summary>
    /// Duck type for LoggingEvent
    /// </summary>
    internal interface ILoggingEventDuck : ILoggingEventDuckBase
    {
        /// <summary>
        /// Gets the UTC time the event was logged
        /// </summary>
        public DateTime TimeStampUtc { get; }
    }
}

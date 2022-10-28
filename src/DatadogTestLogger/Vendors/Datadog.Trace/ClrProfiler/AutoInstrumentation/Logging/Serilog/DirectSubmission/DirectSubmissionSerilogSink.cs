//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="DirectSubmissionSerilogSink.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
#nullable enable

using DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging.DirectSubmission;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging.DirectSubmission.Sink;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Logging.Serilog.DirectSubmission
{
    /// <summary>
    /// Serilog Sink
    /// </summary>
    internal class DirectSubmissionSerilogSink
    {
        private readonly IDatadogSink _sink;
        private readonly int _minimumLevel;

        internal DirectSubmissionSerilogSink(IDatadogSink sink, DirectSubmissionLogLevel minimumLevel)
        {
            _sink = sink;
            _minimumLevel = (int)minimumLevel;
        }

        /// <summary>
        /// Emit the provided log event to the sink
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        [DuckReverseMethod(ParameterTypeNames = new[] { "Serilog.Events.LogEvent, Serilog" })]
        public void Emit(ILogEvent? logEvent)
        {
            if (logEvent is null)
            {
                return;
            }

            if ((int)logEvent.Level < _minimumLevel)
            {
                return;
            }

            _sink.EnqueueLog(new SerilogDatadogLogEvent(logEvent));
        }
    }
}

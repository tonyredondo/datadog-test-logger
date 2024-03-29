//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="DirectSubmissionNLogLegacyTarget.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
#nullable enable

using System;
using System.Collections.Generic;
using DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Logging.NLog.DirectSubmission.Formatting;
using DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Logging.NLog.DirectSubmission.Proxies;
using DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging.DirectSubmission;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging.DirectSubmission.Formatting;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging.DirectSubmission.Sink;
using DatadogTestLogger.Vendors.Datadog.Trace.Telemetry;
using DatadogTestLogger.Vendors.Datadog.Trace.Telemetry.Metrics;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Logging.NLog.DirectSubmission
{
    /// <summary>
    /// NLog Target that sends logs directly to Datadog for NLog &lt;4.5
    /// </summary>
    internal class DirectSubmissionNLogLegacyTarget
    {
        private readonly IDirectSubmissionLogSink _sink;
        private readonly int _minimumLevel;
        private readonly LogFormatter? _formatter;
        private Func<IDictionary<string, object?>?>? _getProperties = null;

        internal DirectSubmissionNLogLegacyTarget(IDirectSubmissionLogSink sink, DirectSubmissionLogLevel minimumLevel)
            : this(sink, minimumLevel, formatter: null)
        {
        }

        // internal for testing
        internal DirectSubmissionNLogLegacyTarget(
            IDirectSubmissionLogSink sink,
            DirectSubmissionLogLevel minimumLevel,
            LogFormatter? formatter)
        {
            _sink = sink;
            _formatter = formatter;
            _minimumLevel = (int)minimumLevel;
        }

        /// <summary>
        /// Writes logging event to the log target
        /// </summary>
        /// <param name="logEventInfo">Logging event to be written out</param>
        [DuckReverseMethod(ParameterTypeNames = new[] { "NLog.LogEventInfo, NLog" })]
        public void Write(ILogEventInfoLegacyProxy? logEventInfo)
        {
            if (logEventInfo is null)
            {
                return;
            }

            if (logEventInfo.Level.Ordinal < _minimumLevel)
            {
                return;
            }

            var contextProperties = _getProperties?.Invoke();
            var eventProperties = logEventInfo.Properties is { Count: > 0 } props ? props : null;

            // We render the event to a string immediately as we need to capture the properties
            // This is more expensive from a CPU perspective, but is necessary as the properties
            // won't necessarily be serialized correctly otherwise (e.g. dd_span_id/dd_trace_id)

            var logEvent = new LogEntry(logEventInfo, contextProperties, eventProperties);
            var logFormatter = _formatter ?? TracerManager.Instance.DirectLogSubmission.Formatter;
            var serializedLog = NLogLogFormatter.FormatLogEvent(logFormatter, logEvent);

            TelemetryFactory.Metrics.RecordCountDirectLogLogs(MetricTags.IntegrationName.NLog);
            _sink.EnqueueLog(new NLogDirectSubmissionLogEvent(serializedLog));
        }

        internal void SetGetContextPropertiesFunc(Func<IDictionary<string, object?>?> func)
        {
            _getProperties = func;
        }
    }
}

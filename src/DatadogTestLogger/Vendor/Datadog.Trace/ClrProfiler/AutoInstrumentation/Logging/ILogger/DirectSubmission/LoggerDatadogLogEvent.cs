// <copyright file="LoggerDatadogLogEvent.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
#nullable enable

using System.Text;
using Vendor.Datadog.Trace.Logging.DirectSubmission.Formatting;
using Vendor.Datadog.Trace.Logging.DirectSubmission.Sink;

namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.Logging.ILogger.DirectSubmission
{
    internal class LoggerDatadogLogEvent : DatadogLogEvent
    {
        private readonly string? _serializedEvent;

        public LoggerDatadogLogEvent(string? serializedEvent)
        {
            _serializedEvent = serializedEvent;
        }

        public override void Format(StringBuilder sb, LogFormatter formatter)
        {
            sb.Append(_serializedEvent);
        }
    }
}

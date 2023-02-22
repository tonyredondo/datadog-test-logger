//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8604, CS8618, CS8620, CS8714, CS8762, CS8765, CS8766, CS8767, CS8768, CS8769, CS8612, CS8629, CS8774
#nullable enable
#if HAVE_TRACE_WRITER
using System;
using System.Diagnostics;
using DiagnosticsTrace = System.Diagnostics.Trace;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Newtonsoft.Json.Serialization
{
    /// <summary>
    /// Represents a trace writer that writes to the application's <see cref="TraceListener"/> instances.
    /// </summary>
    internal class DiagnosticsTraceWriter : ITraceWriter
    {
        /// <summary>
        /// Gets the <see cref="TraceLevel"/> that will be used to filter the trace messages passed to the writer.
        /// For example a filter level of <see cref="TraceLevel.Info"/> will exclude <see cref="TraceLevel.Verbose"/> messages and include <see cref="TraceLevel.Info"/>,
        /// <see cref="TraceLevel.Warning"/> and <see cref="TraceLevel.Error"/> messages.
        /// </summary>
        /// <value>
        /// The <see cref="TraceLevel"/> that will be used to filter the trace messages passed to the writer.
        /// </value>
        public TraceLevel LevelFilter { get; set; }

        private TraceEventType GetTraceEventType(TraceLevel level)
        {
            switch (level)
            {
                case TraceLevel.Error:
                    return TraceEventType.Error;
                case TraceLevel.Warning:
                    return TraceEventType.Warning;
                case TraceLevel.Info:
                    return TraceEventType.Information;
                case TraceLevel.Verbose:
                    return TraceEventType.Verbose;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level));
            }
        }

        /// <summary>
        /// Writes the specified trace level, message and optional exception.
        /// </summary>
        /// <param name="level">The <see cref="TraceLevel"/> at which to write this trace.</param>
        /// <param name="message">The trace message.</param>
        /// <param name="ex">The trace exception. This parameter is optional.</param>
        public void Trace(TraceLevel level, string message, Exception? ex)
        {
            if (level == TraceLevel.Off)
            {
                return;
            }

            TraceEventCache eventCache = new TraceEventCache();
            TraceEventType traceEventType = GetTraceEventType(level);

            foreach (TraceListener listener in DiagnosticsTrace.Listeners)
            {
                if (!listener.IsThreadSafe)
                {
                    lock (listener)
                    {
                        listener.TraceEvent(eventCache, "Newtonsoft.Json", traceEventType, 0, message);
                    }
                }
                else
                {
                    listener.TraceEvent(eventCache, "Newtonsoft.Json", traceEventType, 0, message);
                }

                if (DiagnosticsTrace.AutoFlush)
                {
                    listener.Flush();
                }
            }
        }
    }
}

#endif
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Newtonsoft.Json.Serialization
{
    /// <summary>
    /// Represents a trace writer that writes to memory. When the trace message limit is
    /// reached then old trace messages will be removed as new messages are added.
    /// </summary>
    internal class MemoryTraceWriter : ITraceWriter
    {
        private readonly Queue<string> _traceMessages;
        private readonly object _lock;

        /// <summary>
        /// Gets the <see cref="TraceLevel"/> that will be used to filter the trace messages passed to the writer.
        /// For example a filter level of <see cref="TraceLevel.Info"/> will exclude <see cref="TraceLevel.Verbose"/> messages and include <see cref="TraceLevel.Info"/>,
        /// <see cref="TraceLevel.Warning"/> and <see cref="TraceLevel.Error"/> messages.
        /// </summary>
        /// <value>
        /// The <see cref="TraceLevel"/> that will be used to filter the trace messages passed to the writer.
        /// </value>
        public TraceLevel LevelFilter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTraceWriter"/> class.
        /// </summary>
        public MemoryTraceWriter()
        {
            LevelFilter = TraceLevel.Verbose;
            _traceMessages = new Queue<string>();
            _lock = new object();
        }

        /// <summary>
        /// Writes the specified trace level, message and optional exception.
        /// </summary>
        /// <param name="level">The <see cref="TraceLevel"/> at which to write this trace.</param>
        /// <param name="message">The trace message.</param>
        /// <param name="ex">The trace exception. This parameter is optional.</param>
        public void Trace(TraceLevel level, string message, Exception? ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", CultureInfo.InvariantCulture));
            sb.Append(" ");
            sb.Append(level.ToString("g"));
            sb.Append(" ");
            sb.Append(message);

            string s = sb.ToString();

            lock (_lock)
            {
                if (_traceMessages.Count >= 1000)
                {
                    _traceMessages.Dequeue();
                }

                _traceMessages.Enqueue(s);
            }
        }

        /// <summary>
        /// Returns an enumeration of the most recent trace messages.
        /// </summary>
        /// <returns>An enumeration of the most recent trace messages.</returns>
        public IEnumerable<string> GetTraceMessages()
        {
            return _traceMessages;
        }

        /// <summary>
        /// Returns a <see cref="String"/> of the most recent trace messages.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> of the most recent trace messages.
        /// </returns>
        public override string ToString()
        {
            lock (_lock)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string traceMessage in _traceMessages)
                {
                    if (sb.Length > 0)
                    {
                        sb.AppendLine();
                    }

                    sb.Append(traceMessage);
                }

                return sb.ToString();
            }
        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
using System;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Serilog.Events;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Serilog
{
    /// <summary>
    /// Extends <see cref="ILogger" /> with additional methods.
    /// </summary>
    internal static class LoggerExtensions
    {
        /// <summary>
        /// Create a logger that enriches log events when the specified level is enabled.
        /// </summary>
        /// <typeparam name="TValue"> The type of the property value. </typeparam>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The log event level used to determine if log is enriched with property.</param>
        /// <param name="propertyName">The name of the property. Must be non-empty.</param>
        /// <param name="value">The property value.</param>
        /// <param name="destructureObjects">If true, the value will be serialized as a structured
        /// object if possible; if false, the object will be recorded as a scalar or simple array.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="logger"/> is <code>null</code></exception>
        public static ILogger ForContext<TValue>(
            this ILogger logger,
            LogEventLevel level,
            string propertyName,
            TValue value,
            bool destructureObjects = false)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            return logger.IsEnabled(level)
                ? logger.ForContext(propertyName, value, destructureObjects)
                : logger;
        }
    }
}

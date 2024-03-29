//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
// Copyright 2013-2015 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Serilog.Core;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Serilog.Events;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Serilog.Configuration
{
    /// <summary>
    /// Controls sink configuration.
    /// </summary>
    internal class LoggerMinimumLevelConfiguration
    {
        readonly LoggerConfiguration _loggerConfiguration;
        readonly Action<LogEventLevel> _setMinimum;
        readonly Action<LoggingLevelSwitch> _setLevelSwitch;
        readonly Action<string, LoggingLevelSwitch> _addOverride;

        internal LoggerMinimumLevelConfiguration(LoggerConfiguration loggerConfiguration, Action<LogEventLevel> setMinimum,
                                                 Action<LoggingLevelSwitch> setLevelSwitch, Action<string, LoggingLevelSwitch> addOverride)
        {
            _loggerConfiguration = loggerConfiguration ?? throw new ArgumentNullException(nameof(loggerConfiguration));
            _setMinimum = setMinimum ?? throw new ArgumentNullException(nameof(setMinimum));
            _setLevelSwitch = setLevelSwitch;
            _addOverride = addOverride ?? throw new ArgumentNullException(nameof(addOverride));
        }

        /// <summary>
        /// Sets the minimum level at which events will be passed to sinks.
        /// </summary>
        /// <param name="minimumLevel">The minimum level to set.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Is(LogEventLevel minimumLevel)
        {
            _setMinimum(minimumLevel);
            return _loggerConfiguration;
        }

        /// <summary>
        /// Sets the minimum level to be dynamically controlled by the provided switch.
        /// </summary>
        /// <param name="levelSwitch">The switch.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="levelSwitch"/> is <code>null</code></exception>
        // ReSharper disable once UnusedMethodReturnValue.Global
        public LoggerConfiguration ControlledBy(LoggingLevelSwitch levelSwitch)
        {
            if (levelSwitch == null) throw new ArgumentNullException(nameof(levelSwitch));

            _setLevelSwitch(levelSwitch);
            return _loggerConfiguration;
        }

        /// <summary>
        /// Anything and everything you might want to know about
        /// a running block of code.
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Verbose() => Is(LogEventLevel.Verbose);

        /// <summary>
        /// Internal system events that aren't necessarily
        /// observable from the outside.
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Debug() => Is(LogEventLevel.Debug);

        /// <summary>
        /// The lifeblood of operational intelligence - things
        /// happen.
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Information() => Is(LogEventLevel.Information);

        /// <summary>
        /// Service is degraded or endangered.
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Warning() => Is(LogEventLevel.Warning);

        /// <summary>
        /// Functionality is unavailable, invariants are broken
        /// or data is lost.
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Error() => Is(LogEventLevel.Error);

        /// <summary>
        /// If you have a pager, it goes off when one of these
        /// occurs.
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Fatal() => Is(LogEventLevel.Fatal);

        /// <summary>
        /// Override the minimum level for events from a specific namespace or type name.
        /// This API is not supported for configuring sub-loggers (created through <see cref="LoggerSinkConfiguration.Logger(ILogger, LogEventLevel)"/>). Use <see cref="LoggerConfiguration.Filter"/> or <see cref="LoggerSinkConfiguration.Conditional(Func{LogEvent, bool}, Action{LoggerSinkConfiguration})"/> instead.
        /// You also might consider using https://github.com/serilog/serilog-filters-expressions.
        /// </summary>
        /// <param name="source">The (partial) namespace or type name to set the override for.</param>
        /// <param name="levelSwitch">The switch controlling loggers for matching sources.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="source"/> is <code>null</code></exception>
        /// <exception cref="ArgumentException">When a trimmed <paramref name="source"/> is empty</exception>
        /// <exception cref="ArgumentNullException">When <paramref name="levelSwitch"/> is <code>null</code></exception> 
        public LoggerConfiguration Override(string source, LoggingLevelSwitch levelSwitch)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (levelSwitch == null) throw new ArgumentNullException(nameof(levelSwitch));

            var trimmed = source.Trim();
            if (trimmed.Length == 0) throw new ArgumentException($"A source {nameof(source)} must be provided.", nameof(source));

            _addOverride(trimmed, levelSwitch);
            return _loggerConfiguration;
        }

        /// <summary>
        /// Override the minimum level for events from a specific namespace or type name.
        /// This API is not supported for configuring sub-loggers (created through <see cref="LoggerSinkConfiguration.Logger(ILogger, LogEventLevel)"/>). Use <see cref="LoggerConfiguration.Filter"/> or <see cref="LoggerSinkConfiguration.Conditional(Func{LogEvent, bool}, Action{LoggerSinkConfiguration})"/> instead.
        /// You also might consider using https://github.com/serilog/serilog-filters-expressions.
        /// </summary>
        /// <param name="source">The (partial) namespace or type name to set the override for.</param>
        /// <param name="minimumLevel">The minimum level applied to loggers for matching sources.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="source"/> is <code>null</code></exception>
        public LoggerConfiguration Override(string source, LogEventLevel minimumLevel)
        {
            return Override(source, new LoggingLevelSwitch(minimumLevel));
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
// Copyright 2016-2020 Serilog Contributors
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
    /// Controls audit sink configuration.
    /// </summary>
    internal class LoggerAuditSinkConfiguration
    {
        readonly LoggerSinkConfiguration _sinkConfiguration;

        internal LoggerAuditSinkConfiguration(LoggerConfiguration loggerConfiguration, Action<ILogEventSink> addSink)
        {
            _sinkConfiguration = new LoggerSinkConfiguration(loggerConfiguration, addSink);
        }

        /// <summary>
        /// Audit log events to the specified <see cref="ILogEventSink"/>.
        /// </summary>
        /// <param name="logEventSink">The sink.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Sink(
            ILogEventSink logEventSink,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            // ReSharper disable once MethodOverloadWithOptionalParameter
            LoggingLevelSwitch levelSwitch = null)
        {
            return _sinkConfiguration.Sink(logEventSink, restrictedToMinimumLevel, levelSwitch);
        }

        /// <summary>
        /// Audit log events to the specified <see cref="ILogEventSink"/>.
        /// </summary>
        /// <typeparam name="TSink">The sink.</typeparam>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Sink<TSink>(
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
            where TSink : ILogEventSink, new()
        {
            return _sinkConfiguration.Sink<TSink>(restrictedToMinimumLevel, levelSwitch);
        }

        /// <summary>
        /// Audit log events to a sub-logger, where further processing may occur. Events through
        /// the sub-logger will be constrained by filters and enriched by enrichers that are
        /// active in the parent. A sub-logger cannot be used to log at a more verbose level, but
        /// a less verbose level is possible.
        /// </summary>
        /// <param name="configureLogger">An action that configures the sub-logger.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch">A switch allowing the pass-through minimum level
        /// to be changed at runtime.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Logger(
            Action<LoggerConfiguration> configureLogger,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
        {
            return _sinkConfiguration.Logger(configureLogger, restrictedToMinimumLevel, levelSwitch);
        }

        /// <summary>
        /// Audit log events to a sub-logger, where further processing may occur. Events through
        /// the sub-logger will be constrained by filters and enriched by enrichers that are
        /// active in the parent. A sub-logger cannot be used to log at a more verbose level, but
        /// a less verbose level is possible.
        /// </summary>
        /// <param name="logger">The sub-logger. This will <em>not</em> be shut down automatically when the
        /// parent logger is disposed.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration Logger(
            ILogger logger,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            return _sinkConfiguration.Logger(logger, restrictedToMinimumLevel);
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="SerilogLogFormatter.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
#nullable enable

using System.Collections;
using System.Text;
using DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging.DirectSubmission;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging.DirectSubmission.Formatting;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Newtonsoft.Json;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Serilog.Events;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Logging.Serilog.DirectSubmission.Formatting
{
    internal static class SerilogLogFormatter
    {
        private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor(typeof(SerilogLogFormatter));

        public static void FormatLogEvent(LogFormatter logFormatter, StringBuilder sb, ILogEvent logEvent)
        {
            var message = logEvent.RenderMessage();

            logFormatter.FormatLog(
                sb,
                logEvent,
                logEvent.Timestamp.UtcDateTime,
                message,
                eventId: null,
                GetLogLevelString(logEvent.Level),
                logEvent.Exception,
                (JsonTextWriter w, in ILogEvent e) => RenderProperties(w, e));
        }

        private static string GetLogLevelString(LogEventLevelDuck logLevel) =>
            logLevel switch
            {
                LogEventLevelDuck.Verbose => DirectSubmissionLogLevelExtensions.Verbose,
                LogEventLevelDuck.Debug => DirectSubmissionLogLevelExtensions.Debug,
                LogEventLevelDuck.Information => DirectSubmissionLogLevelExtensions.Information,
                LogEventLevelDuck.Warning => DirectSubmissionLogLevelExtensions.Warning,
                LogEventLevelDuck.Error => DirectSubmissionLogLevelExtensions.Error,
                LogEventLevelDuck.Fatal => DirectSubmissionLogLevelExtensions.Fatal,
                _ => DirectSubmissionLogLevelExtensions.Unknown,
            };

        private static LogPropertyRenderingDetails RenderProperties(JsonTextWriter writer, in ILogEvent logEvent)
        {
            var haveSource = false;
            var haveService = false;
            var haveHost = false;
            var haveTags = false;
            var haveEnv = false;
            var haveVersion = false;
            foreach (var property in logEvent.Properties)
            {
                var duckProperty = property.DuckCast<KeyValuePairStringStruct>();
                var name = duckProperty.Key;

                haveSource |= LogFormatter.IsSourceProperty(name);
                haveService |= LogFormatter.IsServiceProperty(name);
                haveHost |= LogFormatter.IsHostProperty(name);
                haveTags |= LogFormatter.IsTagsProperty(name);
                haveEnv |= LogFormatter.IsEnvProperty(name);
                haveVersion |= LogFormatter.IsVersionProperty(name);

                LogFormatter.WritePropertyName(writer, name);
                FormatLogEventPropertyValue(writer, duckProperty.Value);
            }

            return new LogPropertyRenderingDetails(
                hasRenderedSource: haveSource,
                hasRenderedService: haveService,
                hasRenderedHost: haveHost,
                hasRenderedTags: haveTags,
                hasRenderedEnv: haveEnv,
                hasRenderedVersion: haveVersion,
                messageTemplate: logEvent.MessageTemplate.Text);
        }

        private static void FormatLogEventPropertyValue(JsonTextWriter writer, object value)
        {
            // format the value correctly depending on type
            if (value.TryDuckCast<ScalarValueDuck>(out var scalar))
            {
                LogFormatter.WriteValue(writer, scalar.Value);
                return;
            }

            if (value.TryDuckCast<SequenceValueDuck>(out var sequence))
            {
                FormatSequence(writer, sequence.Elements);
                return;
            }

            if (value.TryDuckCast<StructureValueDuck>(out var structure))
            {
                FormatStructure(writer, structure.Properties, structure.TypeTag);
                return;
            }

            if (value.TryDuckCast<DictionaryValueDuck>(out var dictionary))
            {
                FormatDictionary(writer, dictionary.Elements);
                return;
            }

            if (Log.IsEnabled(LogEventLevel.Debug))
            {
                Log.Debug("Unknown Serilog LogEventPropertyValue '{Type}': skipping in log message", value.GetType());
                LogFormatter.WriteValue(writer, value: null);
            }
        }

        private static void FormatSequence(JsonTextWriter writer, IEnumerable properties)
        {
            writer.WriteStartArray();
            foreach (var property in properties)
            {
                FormatLogEventPropertyValue(writer, property!);
            }

            writer.WriteEndArray();
        }

        private static void FormatStructure(JsonTextWriter writer, IEnumerable properties, string typeTag)
        {
            writer.WriteStartObject();

            foreach (var property in properties)
            {
                var duck = property.DuckCast<LogEventPropertyDuck>();
                writer.WritePropertyName(duck.Name);
                FormatLogEventPropertyValue(writer, duck.Value);
            }

            if (!string.IsNullOrEmpty(typeTag))
            {
                writer.WritePropertyName("_typeTag");
                writer.WriteValue(typeTag);
            }

            writer.WriteEndObject();
        }

        private static void FormatDictionary(JsonTextWriter writer, IEnumerable properties)
        {
            writer.WriteStartObject();

            foreach (var property in properties)
            {
                var duck = property.DuckCast<KeyValuePairObjectStruct>();
                writer.WritePropertyName(duck.Key?.ToString() ?? "null");
                FormatLogEventPropertyValue(writer, duck.Value);
            }

            writer.WriteEndObject();
        }
    }
}

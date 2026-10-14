using System.Reflection;
using System.Text;
using DatadogTestLogger.Vendors.Datadog.Trace;
using DatadogTestLogger.Vendors.Datadog.Trace.Ci;
using DatadogTestLogger.Vendors.Datadog.Trace.Ci.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.Configuration.Telemetry;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace DatadogTestLogger.Test;

[CollectionDefinition("Logger environment", DisableParallelization = true)]
public class LoggerEnvironmentCollection
{
}

[Collection("Logger environment")]
public class LogSubmissionTests : IDisposable
{
    private const string LogsEnabledKey = "DD_CIVISIBILITY_LOGS_ENABLED";
    private const string LoggerLogsEnabledKey = "DD_LOGGER_" + LogsEnabledKey;

    private readonly Dictionary<string, string?> _originalEnvironment = new[]
    {
        LogsEnabledKey, LoggerLogsEnabledKey, "DD_LOGGER_ENABLED", "DD_LOGGER_API_KEY_CHECKER_ENABLED",
        "DD_LOGGER_DD_CIVISIBILITY_ENABLED", "DD_LOGGER_DD_CIVISIBILITY_AGENTLESS_ENABLED",
        "DD_LOGGER_DD_PROFILING_ENABLED", "DD_LOGGER_DD_APPSEC_ENABLED",
        "DD_LOGGER_DD_INSTRUMENTATION_TELEMETRY_ENABLED",
        "DD_LOGGER_DD_INSTRUMENTATION_TELEMETRY_AGENT_PROXY_ENABLED",
        "DD_LOGGER_DD_INSTRUMENTATION_TELEMETRY_AGENTLESS_ENABLED",
    }.ToDictionary(key => key, Environment.GetEnvironmentVariable);

    [Theory]
    [InlineData(null, null, false)]
    [InlineData("false", null, false)]
    [InlineData("true", null, true)]
    [InlineData(null, "false", false)]
    [InlineData(null, "true", true)]
    [InlineData("true", "false", false)]
    [InlineData("false", "true", true)]
    [InlineData(null, "TRUE", true)]
    [InlineData(null, "invalid", false)]
    public void SerializeRespectsLogSubmissionConfiguration(string? globalValue, string? loggerValue, bool expected)
    {
        Environment.SetEnvironmentVariable(LogsEnabledKey, globalValue);
        Environment.SetEnvironmentVariable(LoggerLogsEnabledKey, loggerValue);
        Environment.SetEnvironmentVariable("DD_LOGGER_ENABLED", "true");
        Environment.SetEnvironmentVariable("DD_LOGGER_API_KEY_CHECKER_ENABLED", "false");

        // Fody merges and renames Spekt's types after the compiler emits the reference assembly.
        // Resolve those types from the runtime signature to exercise the packaged serializer.
        var serialize = typeof(DatadogTestResultSerializer).GetMethod("Serialize")!;
        var parameters = serialize.GetParameters();
        var runConfiguration = Activator.CreateInstance(parameters[1].ParameterType)!;
        parameters[1].ParameterType.GetProperty("TargetFramework")!.SetValue(runConfiguration, ".NETCoreApp,Version=v7.0");
        var output = (string)serialize.Invoke(new DatadogTestResultSerializer(), new[]
        {
            null, runConfiguration,
            Activator.CreateInstance(parameters[2].ParameterType),
            Activator.CreateInstance(parameters[3].ParameterType),
        })!;

        Assert.Contains("Serializing test session results.", output);
        var modifiedEnvironment = output.Split("MODIFIED ENVIRONMENT VARIABLES")[1]
                                        .Split("REVERTED ENVIRONMENT VARIABLES")[0];
        var logSetting = modifiedEnvironment.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(line => line.Trim())
                                            .SingleOrDefault(line => line.StartsWith(LogsEnabledKey + "=", StringComparison.Ordinal));
        var configuration = new Dictionary<string, string>();
        if (logSetting is not null)
        {
            configuration[LogsEnabledKey] = logSetting.Substring(LogsEnabledKey.Length + 1);
        }

        var settings = new CIVisibilitySettings(new DictionaryConfigurationSource(configuration), NullConfigurationTelemetry.Instance);
        Assert.Equal(expected, settings.Logs);
        Assert.Equal(globalValue, Environment.GetEnvironmentVariable(LogsEnabledKey));
        Assert.Equal(loggerValue, Environment.GetEnvironmentVariable(LoggerLogsEnabledKey));
    }

    [Fact]
    public void SessionMessagesPreserveSpanTagsWhenLogsAreDisabled()
    {
        var settingsField = typeof(CIVisibility).GetField("_settings", BindingFlags.Static | BindingFlags.NonPublic)!;
        var originalSettings = settingsField.GetValue(null);
        try
        {
            settingsField.SetValue(null, new CIVisibilitySettings(
                new DictionaryConfigurationSource(new Dictionary<string, string>()), NullConfigurationTelemetry.Instance));
            var span = new Span(new SpanContext(1, 2), DateTimeOffset.UtcNow);
            var output = new StringBuilder();
            var emitMessages = typeof(TestSuiteSerializer).GetMethod("EmitSessionMessages", BindingFlags.Static | BindingFlags.NonPublic)!;
            var messagesType = emitMessages.GetParameters()[0].ParameterType;
            var messageType = messagesType.GenericTypeArguments[0];
            var message = Activator.CreateInstance(messageType)!;
            messageType.GetProperty("Level")!.SetValue(message, TestMessageLevel.Warning);
            messageType.GetProperty("Message")!.SetValue(message, "Session message <SpanTag Key=\"session.custom\" Value=\"value\" />");
            var messages = (System.Collections.IList)Activator.CreateInstance(messagesType)!;
            messages.Add(message);

            emitMessages.Invoke(null, new object[] { messages, span, output });

            Assert.Equal("value", span.GetTag("session.custom"));
            Assert.Contains("Added span tag: session.custom = value", output.ToString());
            Assert.DoesNotContain("Exception", output.ToString());
        }
        finally
        {
            settingsField.SetValue(null, originalSettings);
        }
    }

    public void Dispose()
    {
        foreach (var variable in _originalEnvironment)
        {
            Environment.SetEnvironmentVariable(variable.Key, variable.Value);
        }
    }
}

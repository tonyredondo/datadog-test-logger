// <copyright file="DatadogTestResultSerializer.cs" company="PlaceholderCompany">
// Copyright (c) Tony Redondo. All rights reserved.
// </copyright>

using System.Collections;
using System.Diagnostics;
using System.Text;

namespace Datadog.TestLogger;

using Spekt.TestLogger.Core;

internal class DatadogTestResultSerializer : ITestResultSerializer
{
    private const string LoggerPrefix = "DD_LOGGER_";

    public string Serialize(LoggerConfiguration loggerConfiguration, TestRunConfiguration runConfiguration, List<TestResultInfo> results, List<TestMessageInfo> messages)
    {
        try
        {
            if (string.Equals(Environment.GetEnvironmentVariable($"{LoggerPrefix}ENABLED"), "false", StringComparison.OrdinalIgnoreCase))
            {
                return "Logger disabled.";
            }

            if (!string.Equals(Environment.GetEnvironmentVariable($"{LoggerPrefix}API_KEY_CHECKER_ENABLED"), "false", StringComparison.OrdinalIgnoreCase) &&
                !IsApiKeyAvailable())
            {
                return "Api Key is not available, DD_API_KEY environment variable is missing or empty.";
            }

            var builder = new StringBuilder();
            builder.AppendLine($"ProcessId: {Process.GetCurrentProcess().Id}");
            builder.AppendLine($"CommandLine: {Environment.CommandLine}");
            builder.AppendLine();
            ShowEnvironmentVariables(builder, "ORIGINAL ENVIRONMENT VARIABLES");
            Environment.SetEnvironmentVariable($"{LoggerPrefix}DD_CIVISIBILITY_ENABLED", "true");
            Environment.SetEnvironmentVariable($"{LoggerPrefix}DD_CIVISIBILITY_AGENTLESS_ENABLED", "true");
            Environment.SetEnvironmentVariable($"{LoggerPrefix}DD_CIVISIBILITY_LOGS_ENABLED", "false");
            Environment.SetEnvironmentVariable($"{LoggerPrefix}DD_PROFILING_ENABLED", "false");
            Environment.SetEnvironmentVariable($"{LoggerPrefix}DD_INSTRUMENTATION_TELEMETRY_ENABLED", "false");
            Environment.SetEnvironmentVariable($"{LoggerPrefix}DD_INSTRUMENTATION_TELEMETRY_AGENT_PROXY_ENABLED", "false");
            Environment.SetEnvironmentVariable($"{LoggerPrefix}DD_INSTRUMENTATION_TELEMETRY_AGENTLESS_ENABLED", "false");
            Environment.SetEnvironmentVariable($"{LoggerPrefix}DD_APPSEC_ENABLED", "false");
            using (var _ = new DatadogEnvironmentVariablesReplacer(LoggerPrefix))
            {
                ShowEnvironmentVariables(builder, "MODIFIED ENVIRONMENT VARIABLES");
                builder.AppendLine(new TestSuiteSerializer(runConfiguration).Serialize(results, messages));
            }

            ShowEnvironmentVariables(builder, "REVERTED ENVIRONMENT VARIABLES");
            return builder.ToString();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    private void ShowEnvironmentVariables(StringBuilder builder, string title)
    {
        builder.AppendLine(title);
        var curatedEnvironmentVariables = new List<KeyValuePair<string, string>>();
        foreach (DictionaryEntry? envVars in Environment.GetEnvironmentVariables())
        {
            curatedEnvironmentVariables.Add(new KeyValuePair<string, string>(envVars?.Key?.ToString() ?? string.Empty, envVars?.Value?.ToString() ?? string.Empty));
        }

        curatedEnvironmentVariables.Sort((a, b) => a.Key.CompareTo(b.Key));
        foreach (var envVars in curatedEnvironmentVariables)
        {
            builder.AppendLine($"  {envVars.Key}={envVars.Value}");
        }

        builder.AppendLine();
    }

    private bool IsApiKeyAvailable()
    {
        return Environment.GetEnvironmentVariable("DD_API_KEY") is { } ddApiKey && !string.IsNullOrEmpty(ddApiKey) ||
               Environment.GetEnvironmentVariable($"{LoggerPrefix}DD_API_KEY") is { } ddLoggerApiKey && !string.IsNullOrEmpty(ddLoggerApiKey);
    }
}
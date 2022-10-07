// <copyright file="DatadogTestResultSerializer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DatadogTestLogger;

using System.Text.RegularExpressions;
using Spekt.TestLogger.Core;

internal class DatadogTestResultSerializer : ITestResultSerializer
{
    private static int firstInitialization = 1;

    public string Serialize(LoggerConfiguration loggerConfiguration, TestRunConfiguration runConfiguration, List<TestResultInfo> results, List<TestMessageInfo> messages)
    {
        if (Interlocked.Exchange(ref firstInitialization, 0) == 1)
        {
            Environment.SetEnvironmentVariable("DD_CIVISIBILITY_ENABLED", "true");
            Environment.SetEnvironmentVariable("DD_CIVISIBILITY_AGENTLESS_ENABLED", "true");
            Environment.SetEnvironmentVariable("DD_API_KEY", this.GetLoggerApiKey());
            Environment.SetEnvironmentVariable("DD_CIVISIBILITY_LOGS_ENABLED", "true");
            Datadog.Trace.ClrProfiler.Instrumentation.Initialize();
        }

        var runtimeName = string.Empty;
        var runtimeVersion = string.Empty;
        var runtimeNameAndVersionRegex = new Regex("([a-zA-Z.]*),Version=v([0-9.]*)");
        var match = runtimeNameAndVersionRegex.Match(runConfiguration.TargetFramework);
        if (match.Success && match.Groups.Count == 3)
        {
            runtimeName = match.Groups[1].Value;
            if (runtimeName == ".NETFramework")
            {
                runtimeName = ".NET Framework";
            }
            else if (runtimeName == ".NETCoreApp")
            {
                runtimeName = ".NET";
            }

            runtimeVersion = match.Groups[2].Value;

            if (new Version(runtimeVersion).Major < 4)
            {
                runtimeName = ".NET Core";
            }
        }
        else
        {
            runtimeName = runConfiguration.TargetFramework.IndexOf("NETFramework") != -1 ? ".NET Framework" : ".NET";
            runtimeVersion = runConfiguration.TargetFramework switch
            {
                "NETCoreApp21" => "2.1.x",
                "NETCoreApp30" => "3.0.x",
                "NETCoreApp31" => "3.1.x",
                "NETCoreApp50" => "5.0.x",
                "NETCoreApp60" => "6.0.x",
                "NETFramework461" => "4.6.1",
                _ => runConfiguration.TargetFramework
            };
        }

        return string.Empty;
    }

    private string? GetLoggerApiKey()
    {
        var apiKey = Environment.GetEnvironmentVariable("DD_LOGGER_API_KEY");
        if (string.IsNullOrEmpty(apiKey))
        {
            apiKey = Environment.GetEnvironmentVariable("DD_API_KEY");
        }

        return apiKey;
    }
}
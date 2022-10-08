// <copyright file="DatadogTestResultSerializer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DatadogTestLogger;

using System.Reflection;
using System.Text.RegularExpressions;
using Datadog.Trace.Ci;
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

            var ciVisibilityType = typeof(TestModule).Assembly.GetType("Datadog.Trace.Ci.CIVisibility", false);
            var initializeMethod = ciVisibilityType.GetMethod("Initialize");
            initializeMethod.Invoke(null, null);

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

        var groupedResults = results
            .OrderBy(i => i.StartTime)
            .ThenBy(i => i.EndTime)
            .GroupBy(g => g.AssemblyPath).ToArray();

        foreach (var resultByAssembly in groupedResults)
        {
            string testModule = AssemblyName.GetAssemblyName(resultByAssembly.Key).Name!;
            TestModule? module = null;
            DateTime moduleStartTime = DateTime.MinValue;
            DateTime moduleTime = DateTime.MinValue;

            var resultBySuiteGroup = resultByAssembly.GroupBy(g => g.FullTypeName).ToArray();
            foreach (var resultBySuite in resultBySuiteGroup)
            {
                string testSuite = resultBySuite.Key;
                TestSuite? suite = null;
                DateTime suiteEndTime = DateTime.MinValue;
                foreach (var result in resultBySuite)
                {
                    if (module is null)
                    {
                        var testFramework = string.Empty;
                        if (result.TestCase.ExecutorUri.Host.IndexOf("xunit", StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            testFramework = "xUnit";
                        }
                        else if (result.TestCase.ExecutorUri.Host.IndexOf("nunit", StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            testFramework = "NUnit";
                        }
                        else if (result.TestCase.ExecutorUri.Host.IndexOf("mstest", StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            testFramework = "MSTestV2";
                        }

                        moduleStartTime = result.StartTime;
                        module = TestModule.Create(testModule, testFramework, "N/A", moduleStartTime);
                        module.SetTag("runtime.name", runtimeName);
                        module.SetTag("runtime.version", runtimeVersion);
                    }

                    if (suite is null)
                    {
                        suite = module.GetOrCreateSuite(testSuite, result.StartTime);
                    }

                    var testName = result.Method;
                    var test = suite.CreateTest(testName, result.StartTime);

                    // Traits
                    if (result.Traits.Any())
                    {
                        var traits = result.Traits.GroupBy(k => k.Name).ToDictionary(k => k.Key, v => v.Select(i => i.Value).ToList());
                        test.SetTraits(traits);
                    }

                    // Set status
                    var endTime = result.StartTime.Add(result.Duration);
                    if (endTime > suiteEndTime)
                    {
                        suiteEndTime = endTime;
                    }

                    if (result.Outcome == Microsoft.VisualStudio.TestPlatform.ObjectModel.TestOutcome.Passed)
                    {
                        test.Close(TestStatus.Pass, result.Duration);
                    }
                    else if (result.Outcome == Microsoft.VisualStudio.TestPlatform.ObjectModel.TestOutcome.Skipped)
                    {
                        // xUnit skipped message is stored here
                        if (result.Messages.FirstOrDefault(m => m.Category == "StdOutMsgs") is { } xUnitSkipMessage)
                        {
                            test.Close(TestStatus.Skip, result.Duration, xUnitSkipMessage.Text);
                        }
                        else
                        {
                            test.Close(TestStatus.Skip, result.Duration, result.ErrorMessage);
                        }
                    }
                    else if (result.Outcome is Microsoft.VisualStudio.TestPlatform.ObjectModel.TestOutcome.Failed 
                             or Microsoft.VisualStudio.TestPlatform.ObjectModel.TestOutcome.NotFound)
                    {
                        test.SetErrorInfo("Exception", result.ErrorMessage, result.ErrorStackTrace);
                        test.Close(TestStatus.Fail, result.Duration);
                    }
                }

                suite?.Close(suiteEndTime.Subtract(suite.StartTime.DateTime));
                if (moduleTime < suiteEndTime)
                {
                    moduleTime = suiteEndTime;
                }
            }

            module?.Close(moduleTime.Subtract(moduleStartTime));
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
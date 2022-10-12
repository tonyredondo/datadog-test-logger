// <copyright file="DatadogTestResultSerializer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections;
using System.Text;

namespace DatadogTestLogger;

using System.Reflection;
using System.Text.RegularExpressions;
using Datadog.Trace.Ci;
using Spekt.TestLogger.Core;

internal class DatadogTestResultSerializer : ITestResultSerializer
{
    public string Serialize(LoggerConfiguration loggerConfiguration, TestRunConfiguration runConfiguration, List<TestResultInfo> results, List<TestMessageInfo> messages)
    {
        var output = new StringBuilder();

        Environment.SetEnvironmentVariable("DD_CIVISIBILITY_ENABLED", "true");
        Environment.SetEnvironmentVariable("DD_CIVISIBILITY_AGENTLESS_ENABLED", "true");
        Environment.SetEnvironmentVariable("DD_API_KEY", this.GetLoggerApiKey());
        Environment.SetEnvironmentVariable("DD_CIVISIBILITY_LOGS_ENABLED", "true");

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
            var testModule = Assembly.LoadFile(resultByAssembly.Key);
            var testModuleName = AssemblyName.GetAssemblyName(resultByAssembly.Key).Name!;
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
                        var testFrameworkVersion = "N/A";
                        var folder = Path.GetDirectoryName(result.TestCase.Source);

                        if (result.TestCase.ExecutorUri.Host.IndexOf("xunit", StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            output.AppendLine("xUnit framework detected.");
                            testFramework = "xUnit";
                            var xunitCoreFile = Path.Combine(folder!, "xunit.core.dll");
                            output.AppendLine("Looking for: " + xunitCoreFile);
                            if (File.Exists(xunitCoreFile))
                            {
                                output.AppendLine("File exist, opening name.");
                                var xunitCoreAssemblyName = AssemblyName.GetAssemblyName(xunitCoreFile);
                                if (xunitCoreAssemblyName?.Version is { } version)
                                {
                                    output.AppendLine("Version extracted: " + version);
                                    testFrameworkVersion = version.ToString();
                                }
                            }
                            else
                            {
                                output.AppendLine("File doesn't exist.");
                            }
                        }
                        else if (result.TestCase.ExecutorUri.Host.IndexOf("nunit", StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            output.AppendLine("NUnit framework detected.");
                            testFramework = "NUnit";
                            var nUnitFramework = Path.Combine(folder!, "nunit.framework.dll");
                            output.AppendLine("Looking for: " + nUnitFramework);
                            if (File.Exists(nUnitFramework))
                            {
                                output.AppendLine("File exist, opening name.");
                                var nUnitFrameworkAssemblyName = AssemblyName.GetAssemblyName(nUnitFramework);
                                if (nUnitFrameworkAssemblyName?.Version is { } version)
                                {
                                    output.AppendLine("Version extracted: " + version);
                                    testFrameworkVersion = version.ToString();
                                }
                            }
                            else
                            {
                                output.AppendLine("File doesn't exist.");
                            }
                        }
                        else if (result.TestCase.ExecutorUri.Host.IndexOf("mstest", StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            output.AppendLine("MSTest framework detected.");
                            testFramework = "MSTestV2";
                            var msTestFramework = Path.Combine(folder!, "Microsoft.VisualStudio.TestPlatform.TestFramework.dll");
                            output.AppendLine("Looking for: " + msTestFramework);
                            if (File.Exists(msTestFramework))
                            {
                                output.AppendLine("File exist, opening name.");
                                var msTestFrameworkAssemblyName = AssemblyName.GetAssemblyName(msTestFramework);
                                if (msTestFrameworkAssemblyName?.Version is { } version)
                                {
                                    output.AppendLine("Version extracted: " + version);
                                    testFrameworkVersion = version.ToString();
                                }
                            }
                            else
                            {
                                output.AppendLine("File doesn't exist.");
                            }
                        }

                        output.AppendLine("Creating test module: " + testModuleName);
                        moduleStartTime = result.StartTime;
                        module = TestModule.Create(testModuleName, testFramework, testFrameworkVersion, moduleStartTime);
                        module.SetTag("runtime.name", runtimeName);
                        module.SetTag("runtime.version", runtimeVersion);
                    }

                    if (suite is null)
                    {
                        output.AppendLine("  Creating test suite: " + testSuite);
                        suite = module.GetOrCreateSuite(testSuite, result.StartTime);
                    }

                    var testName = result.Method;
                    output.AppendLine("    Creating test: " + testName);
                    var test = suite.CreateTest(testName, result.StartTime);

                    // Set test method
                    var methodName = testName;
                    if (methodName.IndexOf("(") != -1)
                    {
                        output.AppendLine("      Test name has been modified (parameters).");
                        methodName = methodName.Substring(0, methodName.IndexOf("("));
                    }

                    var methodInfo = testModule.GetType(testSuite)?.GetMethod(methodName);
                    if (methodInfo is not null)
                    {
                        output.AppendLine("      Setting Test Method Info.");
                        test.SetTestMethodInfo(methodInfo);
                    }
                    
                    // Traits
                    if (result.Traits.Any())
                    {
                        output.AppendLine("      Setting test traits.");
                        var traits = result.Traits.GroupBy(k => k.Name).ToDictionary(k => k.Key, v => v.Select(i => i.Value).ToList());
                        test.SetTraits(traits);
                    }

                    // Set status
                    output.AppendLine("    Closing test: " + testName);
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

                output.AppendLine("  Closing suite: " + testSuite);
                suite?.Close(suiteEndTime.Subtract(suite.StartTime.DateTime));
                if (moduleTime < suiteEndTime)
                {
                    moduleTime = suiteEndTime;
                }
            }

            output.AppendLine("Closing module: " + testModuleName);
            module?.Close(moduleTime.Subtract(moduleStartTime));
        }

        return output.ToString();
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
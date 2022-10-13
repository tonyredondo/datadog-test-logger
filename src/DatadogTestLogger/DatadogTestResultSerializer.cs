// <copyright file="DatadogTestResultSerializer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

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

        try
        {
            Environment.SetEnvironmentVariable("DD_CIVISIBILITY_ENABLED", "true");
            Environment.SetEnvironmentVariable("DD_CIVISIBILITY_AGENTLESS_ENABLED", "true");
            Environment.SetEnvironmentVariable("DD_CIVISIBILITY_LOGS_ENABLED", "false");
            Environment.SetEnvironmentVariable("DD_PROFILING_ENABLED", "false");
            Environment.SetEnvironmentVariable("DD_INSTRUMENTATION_TELEMETRY_ENABLED", "false");
            Environment.SetEnvironmentVariable("DD_APPSEC_ENABLED", "false");
            SetEnvironmentVariablesFromPrefix();

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
                runtimeName = runConfiguration.TargetFramework.IndexOf("NETFramework") != -1
                    ? ".NET Framework"
                    : ".NET";
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
                var moduleFile = resultByAssembly.Key;
                var modulePdb = Path.ChangeExtension(moduleFile, "pdb");
                var hasPdbFile = File.Exists(modulePdb);
                var testModule = Assembly.LoadFile(moduleFile);
                var testModuleName = testModule.GetName().Name!;
                output.AppendLine("Test Module File: " + moduleFile);
                output.AppendLine("Test Module Pdb: " + modulePdb);
                output.AppendLine("Test Module Pdb Exist: " + hasPdbFile);
                output.AppendLine("Test Module: " + testModule);
                output.AppendLine("Test Module Name: " + testModule.GetName());
                output.AppendLine("AppDomain.CurrentDomain.IsFullyTrusted: " + AppDomain.CurrentDomain.IsFullyTrusted);

                var testFramework = string.Empty;
                var testFrameworkVersion = "N/A";

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
                            var folder = Path.GetDirectoryName(result.TestCase.Source);

                            if (result.TestCase.ExecutorUri.Host.IndexOf("xunit", StringComparison.OrdinalIgnoreCase) !=
                                -1)
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
                            else if (result.TestCase.ExecutorUri.Host.IndexOf("nunit",
                                         StringComparison.OrdinalIgnoreCase) != -1)
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
                            else if (result.TestCase.ExecutorUri.Host.IndexOf("mstest",
                                         StringComparison.OrdinalIgnoreCase) != -1)
                            {
                                output.AppendLine("MSTest framework detected.");
                                testFramework = "MSTestV2";
                                var msTestFramework = Path.Combine(folder!,
                                    "Microsoft.VisualStudio.TestPlatform.TestFramework.dll");
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
                            module = TestModule.Create(testModuleName, testFramework, testFrameworkVersion,
                                moduleStartTime);
                            module.SetTag("runtime.name", runtimeName);
                            module.SetTag("runtime.version", runtimeVersion);
                        }

                        if (suite is null)
                        {
                            output.AppendLine("  Creating test suite: " + testSuite);
                            suite = module.GetOrCreateSuite(testSuite, result.StartTime);
                        }

                        var displayName = result.Method ?? string.Empty;
                        var testName = displayName;
                        var testParameters = string.Empty;
                        var hasParameters = testName.IndexOf("(", StringComparison.Ordinal) != -1;
                        output.AppendLine("    Creating test: " + testName);

                        // Set test method
                        if (hasParameters)
                        {
                            var index = testName.IndexOf("(", StringComparison.Ordinal);
                            testParameters = testName.Substring(index).Trim();
                            testName = testName.Substring(0, index).Trim();
                            output.AppendLine("      Test name has been modified (parameters): " + testName);
                            output.AppendLine("      Parameters: " + testParameters);
                        }

                        var test = suite.CreateTest(testName, result.StartTime);

                        // Process parameters
                        if (!string.IsNullOrEmpty(testParameters))
                        {
                            if (testFramework == "xUnit")
                            {
                                if (testParameters[0] == '(' && testParameters[testParameters.Length - 1] == ')')
                                {
                                    var parameters = new TestParameters
                                    {
                                        Metadata = new Dictionary<string, object>
                                        {
                                            ["test_name"] = displayName
                                        },
                                        Arguments = new Dictionary<string, object>()
                                    };

                                    testParameters = testParameters.Substring(1, testParameters.Length - 2);
                                    var paramsArray = testParameters.Split(new[] { ',' },
                                        StringSplitOptions.RemoveEmptyEntries);
                                    foreach (var pItem in paramsArray)
                                    {
                                        var keyValue = pItem.Split(new[] { ':' },
                                            StringSplitOptions.RemoveEmptyEntries);
                                        keyValue[0] = keyValue[0].Trim();
                                        keyValue[1] = keyValue[1].Trim();
                                        output.AppendLine($"          Param: {keyValue[0]} = {keyValue[1]}");
                                        parameters.Arguments[keyValue[0]] = keyValue[1];
                                    }

                                    test.SetParameters(parameters);
                                }
                            }
                            else if (testFramework is "NUnit" or "MSTestV2")
                            {
                                if (testParameters[0] == '(' && testParameters[testParameters.Length - 1] == ')')
                                {
                                    var parameters = new TestParameters
                                    {
                                        Metadata = new Dictionary<string, object>
                                        {
                                            ["test_name"] = displayName
                                        },
                                        Arguments = new Dictionary<string, object>()
                                    };

                                    testParameters = testParameters.Substring(1, testParameters.Length - 2);
                                    var paramsArray = testParameters.Split(new[] { ',' },
                                        StringSplitOptions.RemoveEmptyEntries);
                                    var argIndex = 0;
                                    foreach (var pItem in paramsArray)
                                    {
                                        var key = "arg" + (argIndex++);
                                        var value = pItem.Trim();
                                        output.AppendLine($"          Param: {key} = {value}");
                                        parameters.Arguments[key] = value;
                                    }

                                    test.SetParameters(parameters);
                                }
                            }
                        }

                        // Set Test method info
                        var methodInfo = testModule.GetType(testSuite)?.GetMethod(testName);
                        if (methodInfo is not null)
                        {
                            output.AppendLine("      Setting Test Method Info: " + methodInfo);
                            test.SetTestMethodInfo(methodInfo);
                        }

                        // Traits
                        if (result.Traits.Any())
                        {
                            output.AppendLine("      Setting test traits.");
                            var traits = result.Traits.GroupBy(k => k.Name)
                                .ToDictionary(k => k.Key, v => v.Select(i => i.Value).ToList());
                            test.SetTraits(traits);
                        }

                        // Set status
                        var endTime = result.StartTime.Add(result.Duration);
                        if (endTime > suiteEndTime)
                        {
                            suiteEndTime = endTime;
                        }

                        output.AppendLine("    result.Outcome: " + result.Outcome);
                        if (result.Outcome == TestOutcome.Passed)
                        {
                            output.AppendLine("    Closing test: " + testName + $" [PASS] [{result.Duration}]");
                            test.Close(TestStatus.Pass, result.Duration);
                        }
                        else if (result.Outcome == TestOutcome.Skipped)
                        {
                            // xUnit skipped message is stored here
                            if (result.Messages.FirstOrDefault(m => m.Category == "StdOutMsgs") is { } xUnitSkipMessage)
                            {
                                output.AppendLine("    Closing test: " + testName +
                                                  $" [SKIP] [{result.Duration}, {xUnitSkipMessage.Text}]");
                                test.Close(TestStatus.Skip, result.Duration, xUnitSkipMessage.Text);
                            }
                            else
                            {
                                output.AppendLine("    Closing test: " + testName +
                                                  $" [SKIP] [{result.Duration}, {result.ErrorMessage}]");
                                test.Close(TestStatus.Skip, result.Duration, result.ErrorMessage);
                            }
                        }
                        else if (result.Outcome is TestOutcome.Failed or TestOutcome.NotFound)
                        {
                            output.AppendLine("    Closing test: " + testName +
                                              $" [FAIL] [{result.Duration}, {result.ErrorMessage}]");
                            test.SetErrorInfo("Exception", result.ErrorMessage, result.ErrorStackTrace);
                            test.Close(TestStatus.Fail, result.Duration);
                        }
                        else if (result.Outcome is TestOutcome.None)
                        {
                            output.AppendLine("    Closing test: " + testName +
                                              $" [SKIP] [{result.Duration}, {result.ErrorMessage}]");
                            test.Close(TestStatus.Skip, result.Duration, result.ErrorMessage);
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
        }
        catch (Exception ex)
        {
            output.AppendLine(ex.ToString());
        }

        return output.ToString();
    }

    private void SetEnvironmentVariablesFromPrefix()
    {
        const string LoggerPrefix = "DD_LOGGER_";
        foreach (DictionaryEntry? keyValue in Environment.GetEnvironmentVariables())
        {
            var key = keyValue?.Key?.ToString() ?? string.Empty;
            if (key.StartsWith(LoggerPrefix, StringComparison.Ordinal))
            {
                var value = keyValue?.Value?.ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    Environment.SetEnvironmentVariable(key.Replace(LoggerPrefix, "DD_"), value);
                }
            }
        }
    }
}
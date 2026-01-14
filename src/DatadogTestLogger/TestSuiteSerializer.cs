// <copyright file="TestSuiteSerializer.cs" company="PlaceholderCompany">
// Copyright (c) Tony Redondo. All rights reserved.
// </copyright>

using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using DatadogTestLogger.Vendors.Datadog.Trace;
using DatadogTestLogger.Vendors.Datadog.Trace.Ci;
using DatadogTestLogger.Vendors.Datadog.Trace.Ci.Logging.DirectSubmission;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Newtonsoft.Json;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Spekt.TestLogger.Core;

namespace DatadogTestLogger;

internal class TestSuiteSerializer
{
    private readonly TestRunConfiguration _runConfiguration;

    public TestSuiteSerializer(TestRunConfiguration runConfiguration)
    {
        _runConfiguration = runConfiguration;
    }

    internal static bool TryGetTypeAndMethodInfo(Assembly assembly, string testSuiteName, string testName, out Type? suiteType, out MethodInfo? testMethod)
    {
        suiteType = null;
        testMethod = null;

        if (assembly is null ||
            string.IsNullOrWhiteSpace(testSuiteName) ||
            string.IsNullOrWhiteSpace(testName))
        {
            return false;
        }

        testSuiteName = testSuiteName.Trim();
        testName = testName.Trim();

        Type? firstTypeFound = null;

        bool TryResolve(string suiteTypeName, string methodName, out Type? resolvedSuiteType, out MethodInfo? resolvedMethod)
        {
            resolvedSuiteType = null;
            resolvedMethod = null;

            if (string.IsNullOrWhiteSpace(suiteTypeName) || string.IsNullOrWhiteSpace(methodName))
            {
                return false;
            }

            suiteTypeName = suiteTypeName.Trim();
            methodName = methodName.Trim();

            resolvedSuiteType = assembly.GetType(suiteTypeName);
            if (resolvedSuiteType is null)
            {
                return false;
            }

            firstTypeFound ??= resolvedSuiteType;

            resolvedMethod = FindMethodInfo(resolvedSuiteType, methodName);
            return resolvedMethod is not null;
        }

        // 1) The simple case: full type name + method name
        if (TryResolve(testSuiteName, testName, out suiteType, out testMethod))
        {
            return true;
        }

        // 2) When the test name contains a '.', it's very likely "TypeName.MethodName" (methods can't contain '.').
        var lastDotIndex = testName.LastIndexOf('.');
        if (lastDotIndex > 0 && lastDotIndex < testName.Length - 1)
        {
            var typePart = testName.Substring(0, lastDotIndex).Trim();
            var methodPart = testName.Substring(lastDotIndex + 1).Trim();

            // Handle redundant prefixes like: suiteType="Namespace.Type", testName="Type.Method"
            if (TryResolve(testSuiteName, methodPart, out suiteType, out testMethod))
            {
                return true;
            }

            foreach (var candidateSuiteTypeName in GetCandidateSuiteTypeNames(testSuiteName, typePart))
            {
                if (TryResolve(candidateSuiteTypeName, methodPart, out suiteType, out testMethod))
                {
                    return true;
                }
            }
        }

        // 3) Fallback: suite name looks like a namespace, test name looks like a method name
        if (testName.IndexOf('.') == -1 &&
            TryResolveMethodInNamespace(assembly, testSuiteName, testName, out var fallbackSuiteType, out var fallbackMethod))
        {
            suiteType = fallbackSuiteType;
            testMethod = fallbackMethod;
            return true;
        }

        suiteType = firstTypeFound;
        testMethod = null;
        return false;
    }

    public string Serialize(List<TestResultInfo> results, List<TestMessageInfo> messages)
    { 
        var output = new StringBuilder();
        
        try
        {
            output.AppendLine("**************************************************");
            output.AppendLine("Serializing test session results.");
            output.AppendLine($"\tDate = {DateTimeOffset.Now}");
            output.AppendLine($"\tResults count = {results?.Count ?? 0}");
            output.AppendLine($"\tMessages count = {messages?.Count ?? 0}");

            var targetFrameworkAttribute = Assembly.GetExecutingAssembly().GetCustomAttribute<TargetFrameworkAttribute>();
            output.AppendLine($"\tLogger TargetFramework = {targetFrameworkAttribute?.FrameworkName}");
            output.AppendLine($"\tLogger TargetFramework(Display) = {targetFrameworkAttribute?.FrameworkDisplayName}");
            output.AppendLine($"\tLogger BCL = {typeof(int).Assembly.FullName}");
            output.AppendLine($"\tLogger AssemblyBuilder assembly = {typeof(AssemblyBuilder).Assembly.FullName}");
            
            var runtimeName = string.Empty;
            var runtimeVersion = string.Empty;
            var runtimeNameAndVersionRegex = new Regex("([a-zA-Z.]*),Version=v([0-9.]*)");
            var match = runtimeNameAndVersionRegex.Match(_runConfiguration.TargetFramework);
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
                runtimeName = _runConfiguration.TargetFramework.IndexOf("NETFramework", StringComparison.InvariantCultureIgnoreCase) != -1
                    ? ".NET Framework"
                    : ".NET";
                runtimeVersion = _runConfiguration.TargetFramework switch
                {
                    "NETCoreApp21" => "2.1.x",
                    "NETCoreApp22" => "2.2.x",
                    "NETCoreApp30" => "3.0.x",
                    "NETCoreApp31" => "3.1.x",
                    "NETCoreApp50" => "5.0.x",
                    "NETCoreApp60" => "6.0.x",
                    "NETCoreApp70" => "7.0.x",
                    "NETCoreApp80" => "8.0.x",
                    "NETFramework461" => "4.6.1",
                    "NETFramework472" => "4.7.2",
                    "NETFramework48" => "4.8",
                    _ => _runConfiguration.TargetFramework
                };
            }

            output.AppendLine($"\tRuntimeName = {runtimeName}");
            output.AppendLine($"\tRuntimeVersion = {runtimeVersion}");
            output.AppendLine("**************************************************");

            if (results is not null)
            {
                var groupedResults = results
                    .OrderBy(i => i.StartTime)
                    .ThenBy(i => i.EndTime)
                    .GroupBy(g => g.AssemblyPath).ToArray();

                Dictionary<Guid, List<CpuUsagePair>>? cpuValues = null;
                Dictionary<Guid, TestCaseMetadata>? testCaseMetadatas = null;

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
                    output.AppendLine("AppDomain.CurrentDomain.IsFullyTrusted: " +
                                      AppDomain.CurrentDomain.IsFullyTrusted);

                    foreach (var folderPath in new[] { Path.GetDirectoryName(moduleFile) ?? string.Empty, Environment.CurrentDirectory })
                    {
                        if (cpuValues is null)
                        {
                            var cpuValuesPath = Path.Combine(folderPath, "cpu_values.json");
                            try
                            {
                                if (File.Exists(cpuValuesPath))
                                {
                                    output.AppendLine("CpuValues file: " + cpuValuesPath);
                                    var jsonCpuValues = File.ReadAllText(cpuValuesPath);
                                    cpuValues = JsonConvert
                                        .DeserializeObject<Dictionary<Guid, List<CpuUsagePair>>>(jsonCpuValues);
                                    output.AppendLine("CpuValues file loaded with " + (cpuValues?.Count ?? -1) +
                                                      " test cases.");
                                }
                                else
                                {
                                    output.AppendLine("CpuValues file doesn't exist!: " + cpuValuesPath);
                                }
                            }
                            catch (Exception ex)
                            {
                                output.AppendLine("Error reading CpuValues json file: " + ex);
                            }
                        }

                        if (testCaseMetadatas is null)
                        {
                            var testCaseMetadataPath = Path.Combine(folderPath, "testcase_metadata.json");
                            try
                            {
                                if (File.Exists(testCaseMetadataPath))
                                {
                                    output.AppendLine("TestCase Metadata file: " + testCaseMetadataPath);
                                    var testCaseMetadataJson = File.ReadAllText(testCaseMetadataPath);
                                    testCaseMetadatas =
                                        JsonConvert.DeserializeObject<Dictionary<Guid, TestCaseMetadata>>(
                                            testCaseMetadataJson);
                                    output.AppendLine("TestCase Metadata file loaded with " +
                                                      (testCaseMetadatas?.Count ?? -1) + " test cases.");
                                }
                                else
                                {
                                    output.AppendLine("TestCase Metadata file doesn't exist!: " + testCaseMetadataPath);
                                }
                            }
                            catch (Exception ex)
                            {
                                output.AppendLine("Error reading TestCase Metadata json file: " + ex);
                            }
                        }
                    }
                    
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
                            TestCaseMetadata? testCaseMetadata = null;
                            var startTime = result.StartTime;

                            // StartTime in metadata
                            if (testCaseMetadatas is not null &&
                                testCaseMetadatas.TryGetValue(result.TestCase.Id, out testCaseMetadata))
                            {
                                startTime = testCaseMetadata.Start;
                            }

                            if (module is null)
                            {
                                var folder = Path.GetDirectoryName(result.TestCase.Source);

                                if (result.TestCase.ExecutorUri.Host.IndexOf("xunit",
                                        StringComparison.OrdinalIgnoreCase) !=
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

                                output.AppendLine("**************************************************");
                                output.AppendLine("Creating test module: " + testModuleName);
                                moduleStartTime = startTime;
                                module = TestModule.Create(testModuleName, testFramework, testFrameworkVersion,
                                    moduleStartTime);
                                module.SetTag("runtime.name", runtimeName);
                                module.SetTag("runtime.version", runtimeVersion);
                            }

                            if (suite is null)
                            {
                                output.AppendLine("  Creating test suite: " + testSuite);
                                suite = module.GetOrCreateSuite(testSuite, startTime);
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
                            
                            
                            var test = suite.CreateTest(testName, startTime);
                            
                            // Cpu values
                            if (cpuValues is not null && cpuValues.TryGetValue(result.TestCase.Id, out var values))
                            {
                                var processCpuData = values.Select(i => i.Process).ToArray();
                                var processCpuStat = BenchmarkDiscreteStats.GetFrom(processCpuData);
                                test.SetTag("test.process_cpu_usage.samples", processCpuStat.N);
                                test.SetTag("test.process_cpu_usage.mean", processCpuStat.Mean);
                                test.SetTag("test.process_cpu_usage.median", processCpuStat.Median);
                                test.SetTag("test.process_cpu_usage.min", processCpuStat.Min);
                                test.SetTag("test.process_cpu_usage.max", processCpuStat.Max);
                                test.SetTag("test.process_cpu_usage.p90", processCpuStat.P90);
                                test.SetTag("test.process_cpu_usage.p95", processCpuStat.P95);
                                test.SetTag("test.process_cpu_usage.p99", processCpuStat.P99);
                                
                                var totalCpuData = values.Select(i => i.System).ToArray();
                                var totalCpuStat = BenchmarkDiscreteStats.GetFrom(totalCpuData);
                                test.SetTag("test.total_cpu_usage.samples", totalCpuStat.N);
                                test.SetTag("test.total_cpu_usage.mean", totalCpuStat.Mean);
                                test.SetTag("test.total_cpu_usage.median", totalCpuStat.Median);
                                test.SetTag("test.total_cpu_usage.min", totalCpuStat.Min);
                                test.SetTag("test.total_cpu_usage.max", totalCpuStat.Max);
                                test.SetTag("test.total_cpu_usage.p90", totalCpuStat.P90);
                                test.SetTag("test.total_cpu_usage.p95", totalCpuStat.P95);
                                test.SetTag("test.total_cpu_usage.p99", totalCpuStat.P99);
                            }

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

                                        try
                                        {
                                            testParameters = testParameters.Substring(1, testParameters.Length - 2);
                                            var paramsArray = testParameters.Split(new[] { ',' },
                                                StringSplitOptions.RemoveEmptyEntries);
                                            foreach (var pItem in paramsArray)
                                            {
                                                var keyValue = pItem.Split(new[] { ':' },
                                                    StringSplitOptions.RemoveEmptyEntries);
                                                if (keyValue.Length != 2)
                                                {
                                                    continue;
                                                }

                                                keyValue[0] = keyValue[0].Trim();
                                                keyValue[1] = keyValue[1].Trim();
                                                output.AppendLine($"          Param: {keyValue[0]} = {keyValue[1]}");
                                                parameters.Arguments[keyValue[0]] = keyValue[1];
                                            }
                                        }
                                        catch (Exception paramEx)
                                        {
                                            output.AppendLine($"Error parsing parameters: {paramEx}");
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

                                        try
                                        {
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
                                        }
                                        catch (Exception paramEx)
                                        {
                                            output.AppendLine($"Error parsing parameters: {paramEx}");
                                        }

                                        test.SetParameters(parameters);
                                    }
                                }
                            }

                            // Set Test method info
                            if (TryGetTypeAndMethodInfo(testModule, testSuite, testName, out var testSuiteType, out var methodInfo))
                            {
                                output.AppendLine("      Setting Test Method Info: " + methodInfo);
                                test.SetTestMethodInfo(methodInfo!);
                            }
                            else
                            {
                                if (testSuiteType is null)
                                {
                                    output.AppendFormat("      Test suite type not found (for TestMethodInfo): {1}, {0} (testMethod: {2})", testModuleName, testSuite, testName);
                                }
                                else
                                {
                                    output.AppendFormat("      Test method not found (for TestMethodInfo): {1}.{2}, {0}", testModuleName, testSuite, testName);
                                }
                            }

                            // Traits
                            if (result.Traits.Any())
                            {
                                output.AppendLine("      Setting test traits.");
                                var traits = result.Traits.GroupBy(k => k.Name)
                                    .ToDictionary(k => k.Key, v => v.Select(i => i.Value).ToList());
                                test.SetTraits(traits);
                            }

                            // Messages
                            try
                            {
                                if (result.Messages?.Count > 0)
                                {
                                    var scopeField = typeof(Test).GetField("_scope",
                                        BindingFlags.Instance | BindingFlags.NonPublic);
                                    if (scopeField?.GetValue(test) is Scope scope)
                                    {
                                        output.AppendLine("      Including test messages.");
                                        foreach (var message in result.Messages)
                                        {
                                            if (message?.Text is { } messageText)
                                            {
                                                var messageArray = messageText.Split(new[] { "\r\n", "\n" },
                                                    StringSplitOptions.RemoveEmptyEntries);
                                                if (messageArray.Length > 2)
                                                {
                                                    foreach (var messageItem in messageArray)
                                                    {
                                                        var logEvent = new CIVisibilityLogEvent("xunit", "info",
                                                            messageItem, scope.Span);
                                                        Tracer.Instance.TracerManager.DirectLogSubmission.Sink
                                                            .EnqueueLog(logEvent);
                                                    }
                                                }
                                                else
                                                {
                                                    var logEvent = new CIVisibilityLogEvent("xunit", "info",
                                                        messageText, scope.Span);
                                                    Tracer.Instance.TracerManager.DirectLogSubmission.Sink.EnqueueLog(
                                                        logEvent);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        output.AppendLine();
                                        output.AppendLine(":( _scope cannot be found inside the Test.");
                                        output.AppendLine();
                                    }
                                }
                            }
                            catch (Exception innerEx)
                            {
                                output.AppendLine(innerEx.ToString());
                            }

                            // Calculate duration (logger doesn't have a good duration precision, sometimes the minimum duration is 1ms)
                            // Here we try to reduce the duration so traces doesn't get stack together in the flamegraph
                            var duration = result.Duration;
                            if (duration.TotalMilliseconds >= 1d)
                            {
                                duration = duration.Subtract(TimeSpan.FromMilliseconds(0.5d));
                            }

                            var endTime = result.StartTime.Add(duration);

                            // If we have data with precision we use that.
                            if (testCaseMetadata is not null)
                            {
                                endTime = testCaseMetadata.End;
                                duration = endTime - startTime;
                            }
                            
                            if (suiteEndTime < endTime)
                            {
                                suiteEndTime = endTime;
                            }

                            // Set status
                            output.AppendLine("      result.Outcome: " + result.Outcome);
                            if (result.Outcome == TestOutcome.Passed)
                            {
                                output.AppendLine("    Closing test: " + testName + $" [PASS] [{duration}]");
                                test.Close(TestStatus.Pass, duration);
                            }
                            else if (result.Outcome == TestOutcome.Skipped)
                            {
                                // xUnit skipped message is stored here
                                if (result.Messages?.FirstOrDefault(m => m.Category == "StdOutMsgs") is
                                    { } xUnitSkipMessage)
                                {
                                    output.AppendLine("    Closing test: " + testName +
                                                      $" [SKIP] [{duration}, {xUnitSkipMessage.Text}]");
                                    test.Close(TestStatus.Skip, duration, xUnitSkipMessage.Text);
                                }
                                else
                                {
                                    output.AppendLine("    Closing test: " + testName +
                                                      $" [SKIP] [{duration}, {result.ErrorMessage}]");
                                    test.Close(TestStatus.Skip, duration, result.ErrorMessage);
                                }
                            }
                            else if (result.Outcome is TestOutcome.Failed or TestOutcome.NotFound)
                            {
                                var errorType = "Exception";
                                var errorMessage = result.ErrorMessage?.Trim() ?? string.Empty;

                                var sepIndex = errorMessage.IndexOf(":", StringComparison.Ordinal);
                                if (sepIndex != -1)
                                {
                                    var initialSentence = errorMessage.Substring(0, sepIndex).Trim();
                                    if (initialSentence.IndexOf(" ", StringComparison.Ordinal) == -1 &&
                                        initialSentence.Contains("Exception"))
                                    {
                                        // sentence before ":" doesn't have space and contains `Exception`
                                        errorType = initialSentence;
                                        errorMessage = errorMessage.Substring(sepIndex + 1).Trim();
                                    }
                                }

                                // If we couldn't extract the type we try further (more allocations)
                                if (errorType == "Exception")
                                {
                                    foreach (var errorMessagePart in errorMessage.Split(new[] { ':' },
                                                 StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        var phrase = errorMessagePart.Trim();

                                        if (phrase.EndsWith("Exception"))
                                        {
                                            var spaceIndex = phrase.IndexOf(" ", StringComparison.Ordinal);
                                            errorType = phrase.Substring(spaceIndex + 1);
                                            break;
                                        }
                                    }
                                }

                                output.AppendLine("    Closing test: " + testName +
                                                  $" [FAIL] [{duration}, {errorType}, {errorMessage}]");
                                test.SetErrorInfo(errorType, errorMessage, result.ErrorStackTrace);
                                test.Close(TestStatus.Fail, duration);
                            }
                            else
                            {
                                output.AppendLine("    Closing test: " + testName +
                                                  $" [SKIP/NONE] [{duration}, {result.ErrorMessage}]");
                                test.Close(TestStatus.Skip, duration, result.ErrorMessage);
                            }
                        }

                        output.AppendLine("  Closing suite: " + testSuite);
                        suite?.Close(suiteEndTime.Subtract(suite.StartTime.DateTime));
                        if (moduleTime < suiteEndTime)
                        {
                            moduleTime = suiteEndTime;
                        }
                    }

                    // Messages
                    try
                    {
                        if (messages is not null)
                        {
                            var spanField = typeof(TestModule).GetField("_span",
                                BindingFlags.Instance | BindingFlags.NonPublic);
                            if (spanField is not null && spanField.GetValue(module) is ISpan span)
                            {
                                output.AppendLine();
                                output.AppendLine("Module contains messages.");
                                foreach (var message in messages)
                                {
                                    var level = message.Level switch
                                    {
                                        TestMessageLevel.Error => "error",
                                        TestMessageLevel.Warning => "warn",
                                        _ => "info"
                                    };

                                    var logEvent = new CIVisibilityLogEvent("xunit", level, message.Message,
                                        new InternalISpan(span));
                                    Tracer.Instance.TracerManager.DirectLogSubmission.Sink.EnqueueLog(logEvent);
                                }

                                output.AppendLine();
                            }
                            else
                            {
                                output.AppendLine();
                                output.AppendLine(":( _span cannot be found inside the TestModule.");
                                output.AppendLine();
                            }
                        }
                    }
                    catch (Exception innerEx)
                    {
                        output.AppendLine(innerEx.ToString());
                    }

                    output.AppendLine("Closing module: " + testModuleName);
                    module?.Close(moduleTime.Subtract(moduleStartTime));
                }
            }
        }
        catch (Exception ex)
        {
            output.AppendLine(ex.ToString());
        }

        return output.ToString();
    }

    // *****************************************************
    // TraceID inside a TestModule instance is not being sent to the backend
    // The backend automatically uses the SpanId as a TraceId on Modules and Suites.
    // That's why we have to decorate the ISpan to return TraceId = SpanId
    // *****************************************************
    private class InternalISpan : ISpan
    {
        private readonly ISpan _span;
        
        public InternalISpan(ISpan span)
        {
            _span = span;
        }

        public void Dispose()
        {
            _span.Dispose();
        }

        public string OperationName
        {
            get => _span.OperationName;
            set => _span.OperationName = value;
        }

        public string ResourceName
        {
            get => _span.ResourceName; 
            set => _span.ResourceName = value;
        }

        public string Type
        {
            get => _span.Type; 
            set => _span.Type = value;
        }

        public bool Error
        {
            get => _span.Error; 
            set => _span.Error = value;
        }

        public string ServiceName
        {
            get => _span.ServiceName; 
            set => _span.ServiceName = value;
        }

        public ulong TraceId => _span.SpanId;

        public ulong SpanId => _span.SpanId;

        public ISpanContext Context => new InternalISpanContext(_span.Context);

        public ISpan SetTag(string key, string value)
        {
            return _span.SetTag(key, value);
        }

        public void Finish()
        {
            _span.Finish();
        }

        public void Finish(DateTimeOffset finishTimestamp)
        {
            _span.Finish(finishTimestamp);
        }

        public void SetException(Exception exception)
        {
            _span.SetException(exception);
        }

        public string GetTag(string key)
        {
            return _span.GetTag(key);
        }
    }

    private class InternalISpanContext : ISpanContext
    {
        private readonly ISpanContext _context;

        public InternalISpanContext(ISpanContext context)
        {
            _context = context;
        }

        public ulong TraceId => _context.SpanId;

        public ulong SpanId => _context.SpanId;

        public string ServiceName => _context.ServiceName;
    }

    internal class TestCaseMetadata
    {
        [JsonProperty("start")]
        public DateTime Start { get; set; }
        [JsonProperty("end")]
        public DateTime End { get; set; }
    }

    internal readonly struct CpuUsagePair
    {
        [JsonProperty("process")]
        public readonly double Process;
        [JsonProperty("system")]
        public readonly double System;

        public CpuUsagePair(double process, double system)
        {
            Process = process;
            System = system;
        }
    }

    private static IEnumerable<string> GetCandidateSuiteTypeNames(string testSuiteName, string typePart)
    {
        var candidates = new List<string>
        {
            typePart,
            $"{testSuiteName}.{typePart}",
            $"{testSuiteName}+{typePart}",
        };

        var testSuiteLastSegment = GetLastTypeSegment(testSuiteName);
        if (!string.IsNullOrEmpty(testSuiteLastSegment) &&
            typePart.StartsWith(testSuiteLastSegment, StringComparison.Ordinal))
        {
            var remainder = typePart.Substring(testSuiteLastSegment.Length);
            if (remainder.Length > 0 && (remainder[0] == '.' || remainder[0] == '+'))
            {
                candidates.Add(testSuiteName + remainder);
            }
        }

        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var candidate in candidates)
        {
            foreach (var normalized in NormalizeTypeNameVariants(candidate))
            {
                if (seen.Add(normalized))
                {
                    yield return normalized;
                }
            }
        }
    }

    private static IEnumerable<string> NormalizeTypeNameVariants(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            yield break;
        }

        typeName = typeName.Trim();
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var candidate in GetCoreTypeNameVariants(typeName))
        {
            var currentCandidate = candidate;
            while (true)
            {
                if (seen.Add(currentCandidate))
                {
                    yield return currentCandidate;
                }

                // Some test frameworks use '.' to represent nested types (instead of '+')
                var lastDotIndex = currentCandidate.LastIndexOf('.');
                if (lastDotIndex < 0)
                {
                    break;
                }

                currentCandidate = currentCandidate.Substring(0, lastDotIndex) + "+" + currentCandidate.Substring(lastDotIndex + 1);
            }
        }
    }

    private static IEnumerable<string> GetCoreTypeNameVariants(string typeName)
    {
        yield return typeName;

        var normalizedGenericsName = ReplaceAngleBracketGenericNotation(typeName);
        if (!string.Equals(normalizedGenericsName, typeName, StringComparison.Ordinal))
        {
            yield return normalizedGenericsName;
        }
    }

    private static string GetLastTypeSegment(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
        {
            return string.Empty;
        }

        var lastPlusIndex = typeName.LastIndexOf('+');
        var lastDotIndex = typeName.LastIndexOf('.');
        var lastSeparatorIndex = Math.Max(lastPlusIndex, lastDotIndex);
        return lastSeparatorIndex < 0 ? typeName : typeName.Substring(lastSeparatorIndex + 1);
    }

    private static string ReplaceAngleBracketGenericNotation(string typeName)
    {
        if (typeName.IndexOf('<') == -1)
        {
            return typeName;
        }

        var sb = new StringBuilder(typeName.Length);
        var index = 0;
        while (index < typeName.Length)
        {
            var ch = typeName[index];
            if (ch != '<')
            {
                sb.Append(ch);
                index++;
                continue;
            }

            index++;
            var depth = 1;
            var argCount = 1;

            while (index < typeName.Length && depth > 0)
            {
                ch = typeName[index];
                if (ch == '<')
                {
                    depth++;
                }
                else if (ch == '>')
                {
                    depth--;
                }
                else if (ch == ',' && depth == 1)
                {
                    argCount++;
                }

                index++;
            }

            if (depth != 0)
            {
                return typeName;
            }

            sb.Append('`').Append(argCount);
        }

        return sb.ToString();
    }

    private static bool TryResolveMethodInNamespace(Assembly assembly, string namespaceName, string methodName, out Type? suiteType, out MethodInfo? testMethod)
    {
        suiteType = null;
        testMethod = null;

        Type[] types;
        try
        {
            types = assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            types = ex.Types.Where(t => t is not null).Cast<Type>().ToArray();
        }

        foreach (var type in types)
        {
            var fullName = type.FullName;
            if (fullName is null)
            {
                continue;
            }

            if (type.Namespace != namespaceName &&
                !fullName.StartsWith(namespaceName + ".", StringComparison.Ordinal) &&
                !fullName.StartsWith(namespaceName + "+", StringComparison.Ordinal))
            {
                continue;
            }

            var resolvedMethod = FindMethodInfo(type, methodName);
            if (resolvedMethod is null)
            {
                continue;
            }

            if (suiteType is not null)
            {
                suiteType = null;
                testMethod = null;
                return false;
            }

            suiteType = type;
            testMethod = resolvedMethod;
        }

        return suiteType is not null && testMethod is not null;
    }

    private static MethodInfo? FindMethodInfo(Type suiteType, string methodName)
    {
        const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

        for (var currentType = suiteType; currentType is not null; currentType = currentType.BaseType)
        {
            var methods = currentType.GetMethods(Flags);
            for (var i = 0; i < methods.Length; i++)
            {
                if (string.Equals(methods[i].Name, methodName, StringComparison.Ordinal))
                {
                    return methods[i];
                }
            }
        }

        return null;
    }
}

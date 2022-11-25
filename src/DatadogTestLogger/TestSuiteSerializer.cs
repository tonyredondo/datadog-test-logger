// <copyright file="TestSuiteSerializer.cs" company="PlaceholderCompany">
// Copyright (c) Tony Redondo. All rights reserved.
// </copyright>

using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DatadogTestLogger.Vendors.Datadog.Trace;
using DatadogTestLogger.Vendors.Datadog.Trace.Ci;
using DatadogTestLogger.Vendors.Datadog.Trace.Ci.Logging.DirectSubmission;
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
                runtimeName = _runConfiguration.TargetFramework.IndexOf("NETFramework") != -1
                    ? ".NET Framework"
                    : ".NET";
                runtimeVersion = _runConfiguration.TargetFramework switch
                {
                    "NETCoreApp21" => "2.1.x",
                    "NETCoreApp30" => "3.0.x",
                    "NETCoreApp31" => "3.1.x",
                    "NETCoreApp50" => "5.0.x",
                    "NETCoreApp60" => "6.0.x",
                    "NETFramework461" => "4.6.1",
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
}
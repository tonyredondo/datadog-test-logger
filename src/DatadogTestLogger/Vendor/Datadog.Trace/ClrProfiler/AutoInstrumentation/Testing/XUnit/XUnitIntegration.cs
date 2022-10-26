// <copyright file="XUnitIntegration.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
#nullable enable

using System;
using System.Collections.Generic;
using Vendor.Datadog.Trace.Ci;
using Vendor.Datadog.Trace.Ci.Tags;
using Vendor.Datadog.Trace.Configuration;

namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.Testing.XUnit;

internal static class XUnitIntegration
{
    internal const string IntegrationName = nameof(IntegrationId.XUnit);
    internal const IntegrationId IntegrationId = Configuration.IntegrationId.XUnit;

    internal static bool IsEnabled => CIVisibility.IsRunning && Tracer.Instance.Settings.IsIntegrationEnabled(IntegrationId);

    internal static Test? CreateTest(ref TestRunnerStruct runnerInstance, Type targetType)
    {
        // Get the test suite instance
        var testSuite = TestSuite.Current;
        if (testSuite is null)
        {
            Common.Log.Warning("Test suite cannot be found.");
            return null;
        }

        var testMethod = runnerInstance.TestMethod;
        var test = testSuite.CreateTest(testMethod?.Name ?? string.Empty);

        // Get test parameters
        var testMethodArguments = runnerInstance.TestMethodArguments;
        var methodParameters = testMethod?.GetParameters();
        if (methodParameters?.Length > 0 && testMethodArguments?.Length > 0)
        {
            var testParameters = new TestParameters();
            testParameters.Metadata = new Dictionary<string, object>();
            testParameters.Arguments = new Dictionary<string, object>();
            testParameters.Metadata[TestTags.MetadataTestName] = runnerInstance.TestCase.DisplayName ?? string.Empty;

            for (var i = 0; i < methodParameters.Length; i++)
            {
                var key = methodParameters[i].Name ?? string.Empty;
                if (i < testMethodArguments.Length)
                {
                    testParameters.Arguments[key] = Common.GetParametersValueData(testMethodArguments[i]);
                }
                else
                {
                    testParameters.Arguments[key] = "(default)";
                }
            }

            test.SetParameters(testParameters);
        }

        // Get traits
        if (runnerInstance.TestCase.Traits is { } traits)
        {
            test.SetTraits(traits);
        }

        // Test code and code owners
        if (testMethod is not null)
        {
            test.SetTestMethodInfo(testMethod);
        }

        // Telemetry
        Tracer.Instance.TracerManager.Telemetry.IntegrationGeneratedSpan(IntegrationId);

        // Skip tests
        if (runnerInstance.SkipReason is { } skipReason)
        {
            test.Close(TestStatus.Skip, skipReason: skipReason, duration: TimeSpan.Zero);
            return null;
        }

        test.ResetStartTime();
        return test;
    }

    internal static void FinishTest(Test test, IExceptionAggregator? exceptionAggregator)
    {
        try
        {
            if (exceptionAggregator?.ToException() is { } exception)
            {
                if (exception.GetType().Name == "SkipException")
                {
                    test.Close(TestStatus.Skip, TimeSpan.Zero, exception.Message);
                }
                else
                {
                    test.SetErrorInfo(exception);
                    test.Close(TestStatus.Fail);
                }
            }
            else
            {
                test.Close(TestStatus.Pass);
            }
        }
        catch (Exception ex)
        {
            CIVisibility.Log.Warning(ex, "Error finishing test scope");
            test.Close(TestStatus.Pass);
        }
    }

    internal static bool ShouldSkip(ref TestRunnerStruct runnerInstance)
    {
        if (CIVisibility.Settings.IntelligentTestRunnerEnabled != true)
        {
            return false;
        }

        var testClass = runnerInstance.TestClass;
        var testMethod = runnerInstance.TestMethod;
        return Common.ShouldSkip(testClass?.ToString() ?? string.Empty, testMethod?.Name ?? string.Empty, runnerInstance.TestMethodArguments, testMethod?.GetParameters());
    }
}

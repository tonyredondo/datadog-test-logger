// <copyright file="NUnitCompositeWorkItemSkipChildrenIntegration.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Vendor.Datadog.Trace.Ci;
using Vendor.Datadog.Trace.ClrProfiler.CallTarget;
using Vendor.Datadog.Trace.DuckTyping;

namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.Testing.NUnit;

/// <summary>
/// NUnit.Framework.Internal.Execution.CompositeWorkItem.SkipChildren() calltarget instrumentation
/// </summary>
[InstrumentMethod(
    AssemblyName = "nunit.framework",
    TypeName = "NUnit.Framework.Internal.Execution.CompositeWorkItem",
    MethodName = "SkipChildren",
    ReturnTypeName = ClrNames.Void,
    ParameterTypeNames = new[] { "_", "NUnit.Framework.Interfaces.ResultState", ClrNames.String },
    MinimumVersion = "3.0.0",
    MaximumVersion = "3.*.*",
    IntegrationName = NUnitIntegration.IntegrationName)]
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public class NUnitCompositeWorkItemSkipChildrenIntegration
{
    private static readonly ConditionalWeakTable<object, object> _errorSpansFromCompositeWorkItems = new();

    /// <summary>
    /// OnMethodBegin callback
    /// </summary>
    /// <typeparam name="TTarget">Type of the target</typeparam>
    /// <typeparam name="TSuite">Test suite type</typeparam>
    /// <typeparam name="TResultState">Result state type</typeparam>
    /// <param name="instance">Instance value, aka `this` of the instrumented method.</param>
    /// <param name="testSuite">Test suite instance</param>
    /// <param name="resultState">Result state instance</param>
    /// <param name="message">Message instance</param>
    /// <returns>Calltarget state value</returns>
    internal static CallTargetState OnMethodBegin<TTarget, TSuite, TResultState>(TTarget instance, TSuite testSuite, TResultState resultState, string message)
    {
        if (testSuite is not null)
        {
            string typeName = testSuite.GetType().Name;

            if (typeName == "CompositeWorkItem")
            {
                // In case we have a CompositeWorkItem we check if there is a OneTimeSetUp failure
                var compositeWorkItem = testSuite.DuckCast<ICompositeWorkItem>();

                if (compositeWorkItem.Result?.ResultState?.Status == TestStatus.Failed && compositeWorkItem.Result.ResultState.Site == FailureSite.SetUp)
                {
                    foreach (var item in compositeWorkItem.Children)
                    {
                        if (item.GetType().Name == "CompositeWorkItem")
                        {
                            var compositeWorkItem2 = item.DuckCast<ICompositeWorkItem>();
                            foreach (var item2 in compositeWorkItem2.Children)
                            {
                                var testResult = item2.DuckCast<IWorkItem>().Result;
                                if (NUnitIntegration.CreateTest(testResult.Test) is { } test)
                                {
                                    test.SetErrorInfo("SetUpException", compositeWorkItem.Result.Message, compositeWorkItem.Result.StackTrace);
                                    test.Close(Ci.TestStatus.Fail, TimeSpan.Zero);
                                }

                                // we need to track all items that we tagged as error due this method uses recursion on child spans.
                                _errorSpansFromCompositeWorkItems.GetOrCreateValue(item2);
                            }
                        }
                        else
                        {
                            var testResult = item.DuckCast<IWorkItem>().Result;
                            if (NUnitIntegration.CreateTest(testResult.Test) is { } test)
                            {
                                test.SetErrorInfo("SetUpException", compositeWorkItem.Result.Message, compositeWorkItem.Result.StackTrace);
                                test.Close(Ci.TestStatus.Fail, TimeSpan.Zero);
                            }

                            // we need to track all items that we tagged as error due this method uses recursion on child spans.
                            _errorSpansFromCompositeWorkItems.GetOrCreateValue(item);
                        }
                    }

                    return new CallTargetState((Scope)null, (object)null);
                }
            }
        }

        return new CallTargetState(null, new object[] { testSuite, message });
    }

    /// <summary>
    /// OnMethodEnd callback
    /// </summary>
    /// <typeparam name="TTarget">Type of the target</typeparam>
    /// <param name="instance">Instance value, aka `this` of the instrumented method.</param>
    /// <param name="exception">Exception instance in case the original code threw an exception.</param>
    /// <param name="state">Calltarget state value</param>
    /// <returns>Return value of the method</returns>
    internal static CallTargetReturn OnMethodEnd<TTarget>(TTarget instance, Exception exception, in CallTargetState state)
    {
        if (state.State != null)
        {
            var stateArray = (object[])state.State;
            var testSuiteOrWorkItem = stateArray[0];
            var skipMessage = (string)stateArray[1];

            const string startString = "OneTimeSetUp:";
            if (skipMessage?.StartsWith(startString, StringComparison.OrdinalIgnoreCase) == true)
            {
                skipMessage = skipMessage.Substring(startString.Length).Trim();
            }

            if (testSuiteOrWorkItem is not null)
            {
                var typeName = testSuiteOrWorkItem.GetType().Name;

                if (typeName == "ParameterizedMethodSuite")
                {
                    // In case the TestSuite is a ParameterizedMethodSuite instance
                    var testSuite = testSuiteOrWorkItem.DuckCast<ITestSuite>();
                    foreach (var item in testSuite.Tests)
                    {
                        if (NUnitIntegration.CreateTest(item.DuckCast<ITest>()) is { } test)
                        {
                            test.Close(Ci.TestStatus.Skip, TimeSpan.Zero, skipMessage);
                        }
                    }
                }
                else if (typeName == "CompositeWorkItem")
                {
                    // In case we have a CompositeWorkItem
                    var compositeWorkItem = testSuiteOrWorkItem.DuckCast<ICompositeWorkItem>();

                    foreach (var item in compositeWorkItem.Children)
                    {
                        // If we already created an error span for this item, we skip this other span creation.
                        if (_errorSpansFromCompositeWorkItems.TryGetValue(item, out _))
                        {
                            continue;
                        }

                        var testResult = item.DuckCast<IWorkItem>().Result;
                        if (NUnitIntegration.CreateTest(testResult.Test) is { } test)
                        {
                            test.Close(Ci.TestStatus.Skip, TimeSpan.Zero, skipMessage);
                        }
                    }
                }
            }
        }

        return default;
    }
}

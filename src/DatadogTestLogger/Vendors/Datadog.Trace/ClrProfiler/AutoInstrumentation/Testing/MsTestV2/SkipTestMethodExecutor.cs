//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="SkipTestMethodExecutor.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Reflection;
using DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Testing.MsTestV2;

internal class SkipTestMethodExecutor
{
    private readonly object _arrayInstance;

    public SkipTestMethodExecutor(Assembly assembly)
    {
        var testResultType = assembly.GetType("Microsoft.VisualStudio.TestTools.UnitTesting.TestResult", throwOnError: true);
        var array = Array.CreateInstance(testResultType, 1);
        var result = Activator.CreateInstance(testResultType);
        var iResult = DuckType.Create<ITestResult>(result);
        iResult.Outcome = UnitTestOutcome.Inconclusive; // Inconclusive is reported as Skipped in the CLI
        array.SetValue(result, 0);
        _arrayInstance = array;
    }

    [DuckReverseMethod(Name = "Execute", ParameterTypeNames = new[] { "Microsoft.VisualStudio.TestTools.UnitTesting.ITestMethod" })]
    public object Execute(object testMethod)
    {
        return _arrayInstance;
    }
}

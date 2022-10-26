﻿// <copyright file="TestAssemblyInfoRunAssemblyCleanupIntegration.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.ComponentModel;
using Vendor.Datadog.Trace.Ci;
using Vendor.Datadog.Trace.ClrProfiler.CallTarget;

namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.Testing.MsTestV2;

/// <summary>
/// Microsoft.VisualStudio.TestPlatform.MSTest.TestAdapter.Execution.TestAssemblyInfo.RunAssemblyCleanup() calltarget instrumentation
/// </summary>
[InstrumentMethod(
    AssemblyName = "Microsoft.VisualStudio.TestPlatform.MSTest.TestAdapter",
    TypeName = "Microsoft.VisualStudio.TestPlatform.MSTest.TestAdapter.Execution.TestAssemblyInfo",
    MethodName = "RunAssemblyCleanup",
    ReturnTypeName = ClrNames.String,
    ParameterTypeNames = new string[0],
    MinimumVersion = "14.0.0",
    MaximumVersion = "14.*.*",
    IntegrationName = MsTestIntegration.IntegrationName)]
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class TestAssemblyInfoRunAssemblyCleanupIntegration
{
    /// <summary>
    /// OnMethodBegin callback
    /// </summary>
    /// <typeparam name="TTarget">Type of the target</typeparam>
    /// <param name="instance">Instance value, aka `this` of the instrumented method.</param>
    /// <returns>Calltarget state value</returns>
    internal static CallTargetState OnMethodBegin<TTarget>(TTarget instance)
        where TTarget : ITestAssemblyInfo
    {
        if (!MsTestIntegration.IsEnabled)
        {
            return CallTargetState.GetDefault();
        }

        if (TestAssemblyInfoRunAssemblyInitializeIntegration.TestAssemblyInfos.TryGetValue(instance.Instance, out var moduleObject) && moduleObject is TestModule module)
        {
            return new CallTargetState(null, module);
        }

        return CallTargetState.GetDefault();
    }

    /// <summary>
    /// OnAsyncMethodEnd callback
    /// </summary>
    /// <typeparam name="TTarget">Type of the target</typeparam>
    /// <typeparam name="TReturn">Type of the return value</typeparam>
    /// <param name="instance">Instance value, aka `this` of the instrumented method.</param>
    /// <param name="returnValue">Return value instance</param>
    /// <param name="exception">Exception instance in case the original code threw an exception.</param>
    /// <param name="state">Calltarget state value</param>
    /// <returns>A response value, in an async scenario will be T of Task of T</returns>
    internal static CallTargetReturn<TReturn> OnMethodEnd<TTarget, TReturn>(TTarget instance, TReturn returnValue, Exception exception, in CallTargetState state)
    {
        if (state.State is TestModule module)
        {
            if (exception is not null)
            {
                module.SetErrorInfo(exception);
            }

            if (returnValue is string { } strWarning)
            {
                module.SetErrorInfo("AssemblyCleanUp", strWarning, null);
            }

            module.Close();
        }

        return new CallTargetReturn<TReturn>(returnValue);
    }
}

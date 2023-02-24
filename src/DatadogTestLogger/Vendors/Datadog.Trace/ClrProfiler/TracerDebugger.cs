//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="TracerDebugger.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
#nullable enable

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.Util;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Serilog;
using SD = System.Diagnostics;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler;

// Based on: https://github.com/microsoft/vstest/blob/main/src/Microsoft.TestPlatform.Execution.Shared/DebuggerBreakpoint.cs#L25
internal static class TracerDebugger
{
    [Conditional("DEBUG")]
    internal static void WaitForDebugger()
    {
        // We check for the managed debugger first then for the native debugger.
        if (!WaitForManagedDebugger())
        {
            WaitForNativeDebugger();
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static bool WaitForManagedDebugger()
    {
        if (SD.Debugger.IsAttached)
        {
            return true;
        }

        var debugEnabled = EnvironmentHelpers.GetEnvironmentVariable(ConfigurationKeys.WaitForDebuggerAttach);
        if (!string.IsNullOrEmpty(debugEnabled) && debugEnabled.Equals("1", StringComparison.Ordinal))
        {
            Console.WriteLine("Waiting for debugger attach...");
            Log.Information("Waiting for debugger attach...");
            var currentProcess = Process.GetCurrentProcess();
            Console.WriteLine("Process Id: {0}, Name: {1}", currentProcess.Id, currentProcess.ProcessName);
            Log.Information<int, string>("Process Id: {id}, Name: {name}", currentProcess.Id, currentProcess.ProcessName);
            while (!SD.Debugger.IsAttached)
            {
                Task.Delay(1000).Wait();
            }

            Break();
            return true;
        }

        return false;
    }

    private static bool WaitForNativeDebugger()
    {
        // Check if native debugging is enabled and OS is windows.
        var nativeDebugEnabled = EnvironmentHelpers.GetEnvironmentVariable(ConfigurationKeys.WaitForNativeDebuggerAttach);
        if (!string.IsNullOrEmpty(nativeDebugEnabled) && nativeDebugEnabled.Equals("1", StringComparison.Ordinal)
                                                      && FrameworkDescription.Instance.IsWindows())
        {
            Console.WriteLine("Waiting for native debugger attach...");
            Log.Information("Waiting for native debugger attach...");
            var currentProcess = Process.GetCurrentProcess();
            Console.WriteLine("Process Id: {0}, Name: {1}", currentProcess.Id, currentProcess.ProcessName);
            Log.Information<int, string>("Process Id: {id}, Name: {name}", currentProcess.Id, currentProcess.ProcessName);
            while (!IsDebuggerPresent())
            {
                Task.Delay(1000).Wait();
            }

            BreakNative();
            return true;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Break()
    {
        {}
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void BreakNative()
    {
        DebugBreak();
    }

    // Native APIs for enabling native debugging.
    [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsDebuggerPresent();

    [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
    private static extern void DebugBreak();
}
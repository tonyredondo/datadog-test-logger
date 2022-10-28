//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="DdwafRuleSetInfoStruct.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Runtime.InteropServices;

namespace DatadogTestLogger.Vendors.Datadog.Trace.AppSec.Waf.NativeBindings
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DdwafRuleSetInfoStruct
    {
        /// <summary>
        /// Number of rules successfully loaded
        /// </summary>
        public ushort Loaded;

        /// <summary>
        /// Number of rules which failed to parse
        /// </summary>
        public ushort Failed;

        /// <summary>
        /// Map from an error string to an array of all the rule ids for which that error was raised. { error: [rule_ids]}
        /// </summary>
        public DdwafObjectStruct Errors;

        /// <summary>
        /// Ruleset version
        /// </summary>
        public IntPtr Version;
    }
}

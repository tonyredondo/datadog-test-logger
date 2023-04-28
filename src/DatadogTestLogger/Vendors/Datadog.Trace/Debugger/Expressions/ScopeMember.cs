//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="ScopeMember.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Debugger.Expressions
{
    internal enum ScopeMemberKind
    {
        Argument,
        Local,
        This,
        Return,
        Exception,
        Duration,
        None
    }

    internal readonly struct ScopeMember
    {
        internal readonly object Value;
        internal readonly string Name;
        internal readonly Type Type;
        internal readonly ScopeMemberKind ElementType;

        internal ScopeMember(string name, Type type, object value, ScopeMemberKind elementType)
        {
            Name = name;
            Type = type;
            Value = value;
            ElementType = elementType;
        }
    }
}

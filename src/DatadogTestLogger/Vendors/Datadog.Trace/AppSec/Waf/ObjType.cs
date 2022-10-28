//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="ObjType.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace DatadogTestLogger.Vendors.Datadog.Trace.AppSec.Waf
{
    internal enum ObjType
    {
        Invalid = 0,
        SignedNumber = 1 << 0,
        UnsignedNumber = 1 << 1,
        String = 1 << 2,
        Array = 1 << 3,
        Map = 1 << 4,
    }
}

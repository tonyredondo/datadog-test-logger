//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="TagItem.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

namespace DatadogTestLogger.Vendors.Datadog.Trace.Tagging
{
    internal readonly ref struct TagItem<T>
    {
        public readonly string Key;
        public readonly byte[] KeyUtf8;
        public readonly T Value;

        public TagItem(string key, T value, byte[] keyUtf8)
        {
            Key = key;
            Value = value;
            KeyUtf8 = keyUtf8;
        }
    }
}

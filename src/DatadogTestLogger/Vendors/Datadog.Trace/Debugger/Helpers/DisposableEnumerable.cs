//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="DisposableEnumerable.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Collections.Generic;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Debugger.Helpers
{
    internal readonly struct DisposableEnumerable<T> : IDisposable
        where T : IDisposable
    {
        private readonly List<T> _items;

        public DisposableEnumerable(List<T> items) => _items = items;

        public void Dispose()
        {
            foreach (var item in _items)
            {
                try
                {
                    item.Dispose();
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}

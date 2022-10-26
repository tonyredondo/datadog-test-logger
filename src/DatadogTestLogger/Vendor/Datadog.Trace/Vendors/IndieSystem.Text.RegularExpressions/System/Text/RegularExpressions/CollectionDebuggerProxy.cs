//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8604, CS8618, CS8620, CS8714, CS8762, CS8765, CS8766, CS8767, CS8768, CS8769, CS8612, CS8629, CS8774
#nullable enable
#if NETCOREAPP3_1_OR_GREATER
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Vendor.Datadog.Trace.Vendors.IndieSystem.Text.RegularExpressions
{
    internal sealed class CollectionDebuggerProxy<T>
    {
        private readonly ICollection<T> _collection;

        public CollectionDebuggerProxy(ICollection<T> collection)
        {
#if NET6_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(collection);
            _collection = collection;
#else
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
#endif
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                var items = new T[_collection.Count];
                _collection.CopyTo(items, 0);
                return items;
            }
        }
    }
}

#endif
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="SnapshotSerializerFieldsAndPropsSelector.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Debugger.Snapshots
{
    internal class SnapshotSerializerFieldsAndPropsSelector
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        private static readonly SnapshotSerializerFieldsAndPropsSelector Instance = new();
        private static readonly List<SnapshotSerializerFieldsAndPropsSelector> CustomSelectors =
            new()
            {
                new LazySnapshotSerializerFieldsAndPropsSelector(),
                new ExceptionSnapshotSerializerFieldsAndPropsSelector(),
                new TaskSnapshotSerializerFieldsAndPropsSelector(),
                new OldStyleTupleSnapshotSerializerFieldsAndPropsSelector()
            };

        protected SnapshotSerializerFieldsAndPropsSelector()
        {
        }

        internal static SnapshotSerializerFieldsAndPropsSelector CreateDeepClonerFieldsAndPropsSelector(Type type)
        {
            return CustomSelectors.FirstOrDefault(c => c.IsApplicable(type)) ?? Instance;
        }

        internal virtual bool IsApplicable(Type type) => true;

        internal virtual IEnumerable<MemberInfo> GetFieldsAndProps(
            Type type,
            object source,
            CancellationTokenSource cts)
        {
            return GetAllFields(type, cts).ToArray();
        }

        /// <summary>
        /// Gets all fields and auto properties e.g. property with a backing field
        /// </summary>
        /// <param name="type">Object type</param>
        /// <param name="cts">Cancellation token source</param>
        /// <returns>Collection of fields and auto properties</returns>
        private static IEnumerable<FieldInfo> GetAllFields(Type type, CancellationTokenSource cts)
        {
            int depth = 0;
            const int maxBaseClassesToExplore = 10;
            while (depth < maxBaseClassesToExplore && type != null && type != typeof(object))
            {
                cts.Token.ThrowIfCancellationRequested();

                foreach (var field in type.GetFields(Flags))
                {
                    yield return field;
                }

                depth++;
                type = type.BaseType;
            }
        }
    }
}

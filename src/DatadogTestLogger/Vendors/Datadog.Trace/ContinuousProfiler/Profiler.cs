//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="Profiler.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System.Threading;

namespace Datadog.Trace.Vendors.Datadog.Trace.ContinuousProfiler
{
    internal class Profiler
    {
        private static Profiler _instance;

        internal Profiler(IContextTracker contextTracker, IProfilerStatus status)
        {
            ContextTracker = contextTracker;
            Status = status;
        }

        public static Profiler Instance
        {
            get { return LazyInitializer.EnsureInitialized(ref _instance, () => Create()); }
        }

        public IProfilerStatus Status { get; }

        public IContextTracker ContextTracker { get; }

        internal static void SetInstanceForTests(Profiler value)
        {
            _instance = value;
        }

        private static Profiler Create()
        {
            var status = new ProfilerStatus();
            var contextTracker = new ContextTracker(status);
            return new Profiler(contextTracker, status);
        }
    }
}
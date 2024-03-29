//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="RuntimeId.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Threading;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Util
{
    internal static class RuntimeId
    {
        private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor(typeof(RuntimeId));
        private static string _runtimeId;

        public static string Get() => LazyInitializer.EnsureInitialized(ref _runtimeId, () => GetImpl());

        private static string GetImpl()
        {
            if (NativeLoader.TryGetRuntimeIdFromNative(out var runtimeId))
            {
                Log.Information("Runtime id retrieved from native loader: {RuntimeId}", runtimeId);
                return runtimeId;
            }

            var guid = Guid.NewGuid().ToString();
            Log.Debug("Unable to get the runtime id from native. Fallback to Guid.NewGuid() : {NewGuid}", guid);

            return guid;
        }
    }
}

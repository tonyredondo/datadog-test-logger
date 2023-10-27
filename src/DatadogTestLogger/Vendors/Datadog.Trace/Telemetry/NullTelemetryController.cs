//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="NullTelemetryController.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System.Threading.Tasks;
using DatadogTestLogger.Vendors.Datadog.Trace.AppSec;
using DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.ContinuousProfiler;
using DatadogTestLogger.Vendors.Datadog.Trace.Iast.Settings;
using DatadogTestLogger.Vendors.Datadog.Trace.PlatformHelpers;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Telemetry
{
    internal class NullTelemetryController : ITelemetryController
    {
        public static readonly NullTelemetryController Instance = new();

        public void IntegrationRunning(IntegrationId integrationId)
        {
        }

        public void IntegrationGeneratedSpan(IntegrationId integrationId)
        {
        }

        public void IntegrationDisabledDueToError(IntegrationId integrationId, string error)
        {
        }

        public void RecordTracerSettings(ImmutableTracerSettings settings, string defaultServiceName)
        {
        }

        public void RecordProfilerSettings(Profiler profiler)
        {
        }

        public void Start()
        {
        }

        public void ProductChanged(TelemetryProductType product, bool enabled, ErrorData? error)
        {
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}

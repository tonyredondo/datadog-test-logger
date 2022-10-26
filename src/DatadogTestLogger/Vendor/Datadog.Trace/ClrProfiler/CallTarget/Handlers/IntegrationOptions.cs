// <copyright file="IntegrationOptions.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Vendor.Datadog.Trace.Configuration;
using Vendor.Datadog.Trace.DuckTyping;
using Vendor.Datadog.Trace.Logging;

namespace Vendor.Datadog.Trace.ClrProfiler.CallTarget.Handlers
{
    internal static class IntegrationOptions<TIntegration, TTarget>
    {
        private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor(typeof(IntegrationOptions<TIntegration, TTarget>));

        private static readonly Lazy<IntegrationId?> _integrationId = new(() => InstrumentationDefinitions.GetIntegrationId(typeof(TIntegration).FullName, typeof(TTarget)));
        private static volatile bool _disableIntegration = false;

        internal static bool IsIntegrationEnabled => !_disableIntegration;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DisableIntegration() => _disableIntegration = true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LogException(Exception exception, string message = null)
        {
            // ReSharper disable twice ExplicitCallerInfoArgument
            Log.Error(exception, message ?? exception?.Message);
            if (exception is DuckTypeException or TargetInvocationException { InnerException: DuckTypeException })
            {
                Log.Warning($"DuckTypeException has been detected, the integration <{typeof(TIntegration)}, {typeof(TTarget)}> will be disabled.");
                if (_integrationId.Value is not null)
                {
                    Tracer.Instance.TracerManager.Telemetry.IntegrationDisabledDueToError(_integrationId.Value.Value, nameof(DuckTypeException));
                }

                _disableIntegration = true;
            }
            else if (exception is CallTargetInvokerException)
            {
                Log.Warning($"CallTargetInvokerException has been detected, the integration <{typeof(TIntegration)}, {typeof(TTarget)}> will be disabled.");
                if (_integrationId.Value is not null)
                {
                    Tracer.Instance.TracerManager.Telemetry.IntegrationDisabledDueToError(_integrationId.Value.Value, nameof(CallTargetInvokerException));
                }

                _disableIntegration = true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void RecordTelemetry()
        {
            if (_integrationId.Value is not null)
            {
                Tracer.Instance.TracerManager.Telemetry.IntegrationRunning(_integrationId.Value.Value);
            }
        }
    }
}

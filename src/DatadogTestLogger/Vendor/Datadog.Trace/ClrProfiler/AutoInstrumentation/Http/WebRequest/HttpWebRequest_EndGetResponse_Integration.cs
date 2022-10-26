// <copyright file="HttpWebRequest_EndGetResponse_Integration.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using Vendor.Datadog.Trace.ClrProfiler.CallTarget;
using Vendor.Datadog.Trace.DuckTyping;
using Vendor.Datadog.Trace.ExtensionMethods;
using Vendor.Datadog.Trace.Propagators;
using Vendor.Datadog.Trace.Sampling;
using Vendor.Datadog.Trace.Tagging;

namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.Http.WebRequest
{
    /// <summary>
    /// CallTarget integration for HttpWebRequest.GetResponse
    /// We only instrument .NET Framework - .NET Core uses an HttpClient
    /// internally, which is already instrumented
    /// </summary>
    [InstrumentMethod(
        AssemblyName = WebRequestCommon.NetFrameworkAssembly,
        TypeName = WebRequestCommon.HttpWebRequestTypeName,
        MethodName = MethodName,
        ReturnTypeName = WebRequestCommon.WebResponseTypeName,
        ParameterTypeNames = new[] { ClrNames.IAsyncResult },
        MinimumVersion = WebRequestCommon.Major4,
        MaximumVersion = WebRequestCommon.Major4,
        IntegrationName = WebRequestCommon.IntegrationName)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class HttpWebRequest_EndGetResponse_Integration
    {
        private const string MethodName = "EndGetResponse";

        /// <summary>
        /// OnMethodEnd callback
        /// </summary>
        /// <typeparam name="TTarget">Type of the target</typeparam>
        /// <typeparam name="TReturn">Type of the return value</typeparam>
        /// <param name="instance">Instance value, aka `this` of the instrumented method.</param>
        /// <param name="returnValue">Task of HttpResponse message instance</param>
        /// <param name="exception">Exception instance in case the original code threw an exception.</param>
        /// <param name="state">Calltarget state value</param>
        /// <returns>A response value, in an async scenario will be T of Task of T</returns>
        internal static CallTargetReturn<TReturn> OnMethodEnd<TTarget, TReturn>(TTarget instance, TReturn returnValue, Exception exception, in CallTargetState state)
            where TTarget : IHttpWebRequest, IDuckType
        {
            if (instance.Instance is HttpWebRequest request && WebRequestCommon.IsTracingEnabled(request))
            {
                // Get the time the HttpWebRequest was created
                DateTime startTime;
                if (Stopwatch.IsHighResolution)
                {
                    var elapsedTicks = Stopwatch.GetTimestamp() - instance.RequestStartTicks;
                    // ReSharper disable once PossibleLossOfFraction
                    startTime = DateTime.UtcNow.AddSeconds(-elapsedTicks / Stopwatch.Frequency);
                }
                else
                {
                    startTime = new DateTime(instance.RequestStartTicks, DateTimeKind.Utc);
                }

                // Check if any headers were injected by a previous call
                var existingSpanContext = SpanContextPropagator.Instance.Extract(request.Headers.Wrap());

                // If this operation creates the trace, then we need to re-apply the sampling priority
                bool setSamplingPriority = existingSpanContext?.SamplingPriority != null && Tracer.Instance.ActiveScope == null;

                Scope scope = null;

                try
                {
                    scope = ScopeFactory.CreateOutboundHttpScope(Tracer.Instance, request.Method, request.RequestUri, WebRequestCommon.IntegrationId, out _, traceId: existingSpanContext?.TraceId, spanId: existingSpanContext?.SpanId, startTime);

                    if (scope is not null)
                    {
                        if (setSamplingPriority)
                        {
                            scope.Span.Context.TraceContext.SetSamplingPriority(existingSpanContext.SamplingPriority.Value);
                        }

                        if (returnValue is HttpWebResponse response)
                        {
                            scope.Span.SetHttpStatusCode((int)response.StatusCode, isServer: false, Tracer.Instance.Settings);
                        }

                        scope.DisposeWithException(exception);
                    }
                }
                catch
                {
                    scope?.Dispose();
                    throw;
                }
            }

            return new CallTargetReturn<TReturn>(returnValue);
        }
    }
}

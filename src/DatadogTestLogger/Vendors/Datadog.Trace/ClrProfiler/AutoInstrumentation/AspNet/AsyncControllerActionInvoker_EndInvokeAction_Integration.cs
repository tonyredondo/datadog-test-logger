//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="AsyncControllerActionInvoker_EndInvokeAction_Integration.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#if NETFRAMEWORK
using System;
using System.ComponentModel;
using System.Web;
using DatadogTestLogger.Vendors.Datadog.Trace.AspNet;
using DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.CallTarget;
using DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.ExtensionMethods;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Serilog;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.AspNet
{
    /// <summary>
    /// System.Web.Mvc.Async.AsyncControllerActionInvoker.EndInvokeAction calltarget instrumentation
    /// </summary>
    [InstrumentMethod(
        AssemblyName = AssemblyName,
        TypeName = "System.Web.Mvc.Async.AsyncControllerActionInvoker",
        MethodName = "EndInvokeAction",
        ReturnTypeName = ClrNames.Bool,
        ParameterTypeNames = new[] { ClrNames.IAsyncResult },
        MinimumVersion = MinimumVersion,
        MaximumVersion = MaximumVersion,
        IntegrationName = IntegrationName)]
    // ReSharper disable once InconsistentNaming
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class AsyncControllerActionInvoker_EndInvokeAction_Integration
    {
        private const string AssemblyName = "System.Web.Mvc";
        private const string MinimumVersion = "4";
        private const string MaximumVersion = "5";

        private const string IntegrationName = nameof(IntegrationId.AspNetMvc);

        /// <summary>
        /// OnMethodEnd callback
        /// </summary>
        /// <typeparam name="TTarget">Type of the target</typeparam>
        /// <typeparam name="TResult">TestResult type</typeparam>
        /// <param name="instance">Instance value, aka `this` of the instrumented method.</param>
        /// <param name="returnValue">Original method return value</param>
        /// <param name="exception">Exception instance in case the original code threw an exception.</param>
        /// <param name="state">Calltarget state value</param>
        /// <returns>Return value of the method</returns>
        internal static CallTargetReturn<TResult> OnMethodEnd<TTarget, TResult>(TTarget instance, TResult returnValue, Exception exception, in CallTargetState state)
        {
            Scope scope = null;
            var httpContext = HttpContext.Current;

            try
            {
                scope = SharedItems.TryPopScope(httpContext, AspNetMvcIntegration.HttpContextKey);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error instrumenting method {MethodName}", "System.Web.Mvc.Async.AsyncControllerActionInvoker.EndInvokeAction()");
            }

            if (scope != null)
            {
                if (exception != null)
                {
                    scope.Span.SetException(exception);

                    // In case of exception, the status code is set further down the ASP.NET pipeline
                    // We use the OnRequestCompleted callback to be notified when that happens.
                    // We don't know how long it'll take for ASP.NET to invoke the callback,
                    // so we store the real finish time.
                    // Additionally, update the scope so it does not finish the span on close. This allows
                    // us to defer finishing the span later while making sure callers of this method do not
                    // get this scope when calling Tracer.ActiveScope
                    var now = scope.Span.Context.TraceContext.UtcNow;
                    httpContext.AddOnRequestCompleted(h => OnRequestCompletedAfterException(h, scope, now));

                    scope.SetFinishOnClose(false);
                    scope.Dispose();
                }
                else
                {
                    HttpContextHelper.AddHeaderTagsFromHttpResponse(httpContext, scope);
                    scope.Span.SetHttpStatusCode(httpContext.Response.StatusCode, isServer: true, Tracer.Instance.Settings);
                    scope.Dispose();
                }
            }

            return new CallTargetReturn<TResult>(returnValue);
        }

        private static void OnRequestCompletedAfterException(HttpContext httpContext, Scope scope, DateTimeOffset finishTime)
        {
            HttpContextHelper.AddHeaderTagsFromHttpResponse(httpContext, scope);

            if (!HttpRuntime.UsingIntegratedPipeline && httpContext.Response.StatusCode == 200)
            {
                // in classic mode, the exception won't cause the correct status code to be set
                // even though a 500 response will be sent, so set it manually instead
                scope.Span.SetHttpStatusCode(500, isServer: true, Tracer.Instance.Settings);
            }
            else
            {
                scope.Span.SetHttpStatusCode(httpContext.Response.StatusCode, isServer: true, Tracer.Instance.Settings);
            }

            scope.Span.Finish(finishTime);
        }
    }
}
#endif

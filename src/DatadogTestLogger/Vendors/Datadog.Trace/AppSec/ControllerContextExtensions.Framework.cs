//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="ControllerContextExtensions.Framework.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#if NETFRAMEWORK

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatadogTestLogger.Vendors.Datadog.Trace.AspNet;
using DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.AspNet;

namespace DatadogTestLogger.Vendors.Datadog.Trace.AppSec
{
    internal static class ControllerContextExtensions
    {
        internal static void MonitorBodyAndPathParams(this IControllerContext controllerContext, IDictionary<string, object> parameters, string peekScopeKey)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return;
            }

            var security = Security.Instance;
            var context = HttpContext.Current;
            if (context != null && security.Settings.Enabled)
            {
                var bodyDic = new Dictionary<string, object>(parameters.Count);
                var pathParamsDic = new Dictionary<string, object>(parameters.Count);
                foreach (var item in parameters)
                {
                    if (controllerContext.RouteData.Values.ContainsKey(item.Key))
                    {
                        pathParamsDic[item.Key] = item.Value;
                    }
                    else
                    {
                        bodyDic[item.Key] = item.Value;
                    }
                }

                var scope = SharedItems.TryPeekScope(context, peekScopeKey);
                security.InstrumentationGateway.RaiseBodyAvailable(context, scope.Span, bodyDic);
                security.InstrumentationGateway.RaisePathParamsAvailable(context, scope.Span, pathParamsDic);
                security.InstrumentationGateway.RaiseBlockingOpportunity(context, scope, Tracer.Instance.Settings, args =>
                {
                    TracingHttpModule.AddHeaderTagsFromHttpResponse(args.Context, args.Scope);
                });
            }
        }
    }
}
#endif

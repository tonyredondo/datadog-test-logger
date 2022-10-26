// <copyright file="IHttpControllerContext.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#if NETFRAMEWORK
namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.AspNet
{
    /// <summary>
    /// HttpControllerContext interface for ducktyping
    /// </summary>
    internal interface IHttpControllerContext
    {
        IHttpRequestMessage Request { get; }

        IHttpRouteData RouteData { get; }

        RequestContextStruct RequestContext { get; }
    }
}
#endif

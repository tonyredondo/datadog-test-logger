// <copyright file="EndpointFeatureProxy.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using Vendor.Datadog.Trace.DuckTyping;

namespace Vendor.Datadog.Trace.DiagnosticListeners
{
    /// <summary>
    /// Proxy for ducktyping IEndpointFeature when the interface is implemented
    /// explicitly, e.g. by https://github.com/dotnet/aspnetcore/blob/v3.0.3/src/Servers/Kestrel/Core/src/Internal/Http/HttpProtocol.FeatureCollection.cs
    /// Also see AspNetCoreDiagnosticObserver.EndpointFeatureStruct
    /// </summary>
    internal class EndpointFeatureProxy
    {
        /// <summary>
        /// Delegates to IEndpointFeature.Endpoint;
        /// </summary>
        /// <returns>The <see cref="RouteEndpoint"/> proxy</returns>
        [Duck(Name = "Microsoft.AspNetCore.Http.Features.IEndpointFeature.get_Endpoint")]
        public virtual object GetEndpoint() => default;
    }
}

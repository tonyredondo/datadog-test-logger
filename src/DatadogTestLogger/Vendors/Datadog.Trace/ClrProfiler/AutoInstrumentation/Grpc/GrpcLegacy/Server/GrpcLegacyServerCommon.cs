//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="GrpcLegacyServerCommon.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping;
using DatadogTestLogger.Vendors.Datadog.Trace.ExtensionMethods;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging;
using DatadogTestLogger.Vendors.Datadog.Trace.Propagators;
using DatadogTestLogger.Vendors.Datadog.Trace.Tagging;

#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Grpc.GrpcLegacy.Server
{
    internal class GrpcLegacyServerCommon
    {
        private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor(typeof(GrpcLegacyServerCommon));

        public static Scope? CreateServerSpan<TTarget>(Tracer tracer, TTarget target, IMetadata? metadata)
        {
            var serverHandler = target.DuckCast<ServerCallHandlerStruct>();
            var method = serverHandler.Method;
            Scope? scope = null;
            try
            {
                var tags = new GrpcServerTags();
                // Grpc.Core server tags are typically the root span, so use enabledWithGlobalSetting=true
                GrpcCommon.AddGrpcTags(tags, tracer, method.GrpcType, name: method.Name, path: method.FullName, serviceName: method.ServiceName, analyticsEnabledWithGlobalSetting: true);

                // If we have a local span (e.g. from aspnetcore) then use that as the parent
                // Otherwise, use the distributed context as the parent
                var spanContext = tracer.ActiveScope?.Span.Context;
                if (spanContext is null)
                {
                    spanContext = ExtractPropagatedContext(metadata);
                }

                var serviceName = tracer.DefaultServiceName ?? "grpc-server";
                string operationName = tracer.CurrentTraceSettings.Schema.Server.GetOperationNameForProtocol("grpc");
                scope = tracer.StartActiveInternal(operationName, parent: spanContext, tags: tags, serviceName: serviceName);

                var span = scope.Span;
                span.Type = SpanTypes.Grpc;
                span.ResourceName = method.FullName;

                if (metadata?.Count > 0)
                {
                    span.SetHeaderTags(new MetadataHeadersCollection(metadata), tracer.Settings.GrpcTagsInternal, GrpcCommon.RequestMetadataTagPrefix);
                }

                tracer.TracerManager.Telemetry.IntegrationGeneratedSpan(IntegrationId.Grpc);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating server span for GRPC call");
            }

            return scope;
        }

        private static SpanContext? ExtractPropagatedContext(IMetadata? metadata)
        {
            try
            {
                if (metadata is not null)
                {
                    return SpanContextPropagator.Instance.Extract(new MetadataHeadersCollection(metadata));
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error extracting propagated HTTP headers.");
            }

            return null;
        }
    }
}

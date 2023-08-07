//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="WcfCommon.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#if NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.DuckTyping;
using DatadogTestLogger.Vendors.Datadog.Trace.ExtensionMethods;
using DatadogTestLogger.Vendors.Datadog.Trace.Headers;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging;
using DatadogTestLogger.Vendors.Datadog.Trace.Propagators;
using DatadogTestLogger.Vendors.Datadog.Trace.Tagging;
using DatadogTestLogger.Vendors.Datadog.Trace.Util;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Wcf
{
    internal class WcfCommon
    {
        private const string HttpRequestMessagePropertyTypeName = "System.ServiceModel.Channels.HttpRequestMessageProperty";
        private static readonly Lazy<Func<object>> _getCurrentOperationContext = new Lazy<Func<object>>(CreateGetCurrentOperationContextDelegate, isThreadSafe: true);
        private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor(typeof(WcfCommon));

        internal const string IntegrationName = nameof(IntegrationId.Wcf);
        internal const IntegrationId IntegrationId = Configuration.IntegrationId.Wcf;

        public static Func<object> GetCurrentOperationContext => _getCurrentOperationContext.Value;

        public static ConditionalWeakTable<object, Scope> Scopes { get; } = new();

        internal static Scope CreateScope<TRequestContext>(TRequestContext requestContext)
            where TRequestContext : IRequestContext
        {
            var requestMessage = requestContext.RequestMessage;

            if (requestMessage == null)
            {
                return null;
            }

            var tracer = Tracer.Instance;

            if (!tracer.Settings.IsIntegrationEnabled(IntegrationId))
            {
                // integration disabled, don't create a scope, skip this trace
                return null;
            }

            Scope scope = null;

            try
            {
                SpanContext propagatedContext = null;
                string host = null;
                string userAgent = null;
                string httpMethod = null;
                WebHeadersCollection? headers = null;

                IDictionary<string, object> requestProperties = requestMessage.Properties;
                if (requestProperties.TryGetValue("httpRequest", out object httpRequestProperty) &&
                    httpRequestProperty.GetType().FullName.Equals(HttpRequestMessagePropertyTypeName, StringComparison.OrdinalIgnoreCase))
                {
                    var httpRequestPropertyProxy = httpRequestProperty.DuckCast<HttpRequestMessagePropertyStruct>();
                    var webHeaderCollection = httpRequestPropertyProxy.Headers;

                    // we're using an http transport
                    host = webHeaderCollection[HttpRequestHeader.Host];
                    userAgent = webHeaderCollection[HttpRequestHeader.UserAgent];
                    httpMethod = httpRequestPropertyProxy.Method?.ToUpperInvariant();

                    // try to extract propagated context values from http headers
                    if (tracer.ActiveScope == null)
                    {
                        try
                        {
                            headers = webHeaderCollection.Wrap();
                            propagatedContext = SpanContextPropagator.Instance.Extract(headers.Value);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "Error extracting propagated HTTP headers.");
                        }
                    }
                }

                if (propagatedContext == null && requestMessage.Headers != null)
                {
                    Log.Debug("Extracting from WCF headers if any as http headers hadn't been found.");
                    try
                    {
                        propagatedContext = SpanContextPropagator.Instance.Extract(requestMessage.Headers, GetHeaderValues);

                        static IEnumerable<string> GetHeaderValues(IMessageHeaders headers, string name)
                        {
                            try
                            {
                                const string ns = "datadog";
                                var index = headers.FindHeader(name, ns);
                                if (index >= 0)
                                {
                                    return new[] { headers.GetHeader<string>(name, ns) };
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, "Error extracting propagated WCF headers.");
                            }

                            return Enumerable.Empty<string>();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Error extracting propagated WCF headers.");
                    }
                }

                string operationName = tracer.CurrentTraceSettings.Schema.Server.GetOperationNameForComponent("wcf");
                var tags = new WcfTags();
                scope = tracer.StartActiveInternal(operationName, propagatedContext, tags: tags);
                var span = scope.Span;

                var requestHeaders = requestMessage.Headers;
                string action = requestHeaders.Action;
                Uri requestHeadersTo = requestHeaders.To;

                span.DecorateWebServerSpan(
                    resourceName: GetResourceName(requestHeaders),
                    httpMethod,
                    host,
                    httpUrl: requestHeadersTo?.AbsoluteUri,
                    userAgent,
                    tags);

                if (headers is not null)
                {
                    var headerTagsProcessor = new SpanContextPropagator.SpanTagHeaderTagProcessor(span);
                    SpanContextPropagator.Instance.ExtractHeaderTags(ref headerTagsProcessor, headers.Value, tracer.Settings.HeaderTagsInternal, SpanContextPropagator.HttpRequestHeadersTagPrefix);
                }

                tags.SetAnalyticsSampleRate(IntegrationId, tracer.Settings, enabledWithGlobalSetting: true);
                tracer.TracerManager.Telemetry.IntegrationGeneratedSpan(IntegrationId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating or populating scope.");
            }

            // always returns the scope, even if it's null
            return scope;
        }

        private static string GetResourceName(IMessageHeaders requestHeaders)
        {
            var action = requestHeaders.Action;
            if (!string.IsNullOrEmpty(action))
            {
                return action;
            }

            if (Tracer.Instance.Settings.WcfObfuscationEnabled)
            {
                return UriHelpers.GetCleanUriPath(requestHeaders.To?.LocalPath);
            }

            return requestHeaders.To?.LocalPath;
        }

        private static Func<object> CreateGetCurrentOperationContextDelegate()
        {
            var operationContextType = Type.GetType("System.ServiceModel.OperationContext, System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", throwOnError: false);
            if (operationContextType is not null)
            {
                var property = operationContextType.GetProperty("Current", BindingFlags.Public | BindingFlags.Static);
                var method = property.GetGetMethod();
                return (Func<object>)method.CreateDelegate(typeof(Func<object>));
            }

            return null;
        }
    }
}
#endif

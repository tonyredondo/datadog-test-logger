//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="AwsSqsCommon.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging;
using DatadogTestLogger.Vendors.Datadog.Trace.Tagging;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.AWS.SQS
{
    internal static class AwsSqsCommon
    {
        private const string DatadogAwsSqsServiceName = "aws-sqs";
        private const string SqsOperationName = "sqs.request";
        private const string SqsServiceName = "SQS";
        private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor(typeof(AwsSqsCommon));

        internal const string IntegrationName = nameof(Configuration.IntegrationId.AwsSqs);
        internal const IntegrationId IntegrationId = Configuration.IntegrationId.AwsSqs;

        public static Scope CreateScope(Tracer tracer, string operation, out AwsSqsTags tags, ISpanContext parentContext = null)
        {
            tags = null;

            if (!tracer.Settings.IsIntegrationEnabled(IntegrationId) || !tracer.Settings.IsIntegrationEnabled(AwsConstants.IntegrationId))
            {
                // integration disabled, don't create a scope, skip this trace
                return null;
            }

            Scope scope = null;

            try
            {
                tags = new AwsSqsTags();
                string serviceName = tracer.Settings.GetServiceName(tracer, DatadogAwsSqsServiceName);
                scope = tracer.StartActiveInternal(SqsOperationName, parent: parentContext, tags: tags, serviceName: serviceName);
                var span = scope.Span;

                span.Type = SpanTypes.Http;
                span.ResourceName = $"{SqsServiceName}.{operation}";

                tags.Service = SqsServiceName;
                tags.Operation = operation;
                tags.SetAnalyticsSampleRate(IntegrationId, tracer.Settings, enabledWithGlobalSetting: false);
                tracer.TracerManager.Telemetry.IntegrationGeneratedSpan(IntegrationId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating or populating scope.");
            }

            // always returns the scope, even if it's null because we couldn't create it,
            // or we couldn't populate it completely (some tags is better than no tags)
            return scope;
        }
    }
}

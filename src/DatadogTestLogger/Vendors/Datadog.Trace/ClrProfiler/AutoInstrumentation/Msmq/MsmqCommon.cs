//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="MsmqCommon.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using DatadogTestLogger.Vendors.Datadog.Trace.Logging;
using DatadogTestLogger.Vendors.Datadog.Trace.Tagging;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler.AutoInstrumentation.Msmq
{
    internal static class MsmqCommon
    {
        private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor(typeof(MsmqCommon));

        internal static Scope CreateScope<TMessageQueue>(Tracer tracer, string command, string spanKind, TMessageQueue messageQueue, bool? messagePartofTransaction = null)
            where TMessageQueue : IMessageQueue
        {
            if (!tracer.Settings.IsIntegrationEnabled(MsmqConstants.IntegrationId))
            {
                // integration disabled, don't create a scope, skip this trace
                return null;
            }

            Scope scope = null;

            try
            {
                var tags = new MsmqTags(spanKind)
                {
                    Command = command,
                    IsTransactionalQueue = messageQueue.Transactional.ToString(),
                    Path = messageQueue.Path,
                };
                if (messagePartofTransaction.HasValue)
                {
                    tags.MessageWithTransaction = messagePartofTransaction.ToString();
                }

                var serviceName = tracer.Settings.GetServiceName(tracer, MsmqConstants.ServiceName);

                scope = tracer.StartActiveInternal(MsmqConstants.OperationName, serviceName: serviceName, tags: tags);

                var span = scope.Span;
                span.Type = SpanTypes.Queue;
                span.ResourceName = $"{command} {messageQueue.Path}";
                tracer.TracerManager.Telemetry.IntegrationGeneratedSpan(MsmqConstants.IntegrationId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating or populating scope.");
            }

            return scope;
        }
    }
}

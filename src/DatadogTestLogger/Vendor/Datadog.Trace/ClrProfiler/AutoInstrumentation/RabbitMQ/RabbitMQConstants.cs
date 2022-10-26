// <copyright file="RabbitMQConstants.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using Vendor.Datadog.Trace.Configuration;

namespace Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.RabbitMQ
{
    internal static class RabbitMQConstants
    {
        internal const string IntegrationName = nameof(Configuration.IntegrationId.RabbitMQ);
        internal const string IBasicPropertiesTypeName = "RabbitMQ.Client.IBasicProperties";
        internal const string IDictionaryArgumentsTypeName = "System.Collections.Generic.IDictionary`2[System.String,System.Object]";
        internal const string OperationName = "amqp.command";
        internal const string ServiceName = "rabbitmq";
        internal const IntegrationId IntegrationId = Configuration.IntegrationId.RabbitMQ;
    }
}

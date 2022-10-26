﻿// <copyright file="ServiceRemotingConstants.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using Vendor.Datadog.Trace.Configuration;

namespace Vendor.Datadog.Trace.ServiceFabric
{
    internal static class ServiceRemotingConstants
    {
        public const string AssemblyName = "Microsoft.ServiceFabric.Services.Remoting";

        public const string ClientEventsTypeName = "Microsoft.ServiceFabric.Services.Remoting.V2.Client.ServiceRemotingClientEvents";

        public const string ServiceEventsTypeName = "Microsoft.ServiceFabric.Services.Remoting.V2.Runtime.ServiceRemotingServiceEvents";

        public const string SendRequestEventName = "SendRequest";

        public const string ReceiveResponseEventName = "ReceiveResponse";

        public const string ReceiveRequestEventName = "ReceiveRequest";

        public const string SendResponseEventName = "SendResponse";

        public const IntegrationId IntegrationId = Configuration.IntegrationId.ServiceRemoting;
    }
}

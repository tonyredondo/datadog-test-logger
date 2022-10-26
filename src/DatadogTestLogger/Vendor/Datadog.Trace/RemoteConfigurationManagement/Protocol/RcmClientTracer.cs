// <copyright file="RcmClientTracer.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using Vendor.Datadog.Trace.Vendors.Newtonsoft.Json;

namespace Vendor.Datadog.Trace.RemoteConfigurationManagement.Protocol
{
    internal class RcmClientTracer
    {
        public RcmClientTracer(string runtimeId, string tracerVersion, string service, string env, string appVersion)
        {
            RuntimeId = runtimeId;
            Language = TracerConstants.Language;
            TracerVersion = tracerVersion;
            Service = service;
            Env = env;
            AppVersion = appVersion;
        }

        [JsonProperty("runtime_id")]
        public string RuntimeId { get; }

        [JsonProperty("language")]
        public string Language { get; }

        [JsonProperty("tracer_version")]
        public string TracerVersion { get; }

        [JsonProperty("service")]
        public string Service { get; }

        [JsonProperty("env")]
        public string Env { get; }

        [JsonProperty("app_version")]
        public string AppVersion { get; }
    }
}

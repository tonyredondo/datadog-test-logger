// <copyright file="RcmCachedTargetFileHash.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using Vendor.Datadog.Trace.Vendors.Newtonsoft.Json;

namespace Vendor.Datadog.Trace.RemoteConfigurationManagement.Protocol
{
    internal class RcmCachedTargetFileHash
    {
        public RcmCachedTargetFileHash(string algorithm, string hash)
        {
            Algorithm = algorithm;
            Hash = hash;
        }

        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        [JsonProperty("hash")]
        public string Hash { get; }
    }
}

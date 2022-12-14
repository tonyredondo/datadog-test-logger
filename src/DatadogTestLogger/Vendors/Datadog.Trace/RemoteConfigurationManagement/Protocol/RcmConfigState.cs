//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="RcmConfigState.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Newtonsoft.Json;

namespace DatadogTestLogger.Vendors.Datadog.Trace.RemoteConfigurationManagement.Protocol
{
    internal class RcmConfigState
    {
        public RcmConfigState(string id, int version, string product, uint applyState, string applyError = null)
        {
            Id = id;
            Version = version;
            Product = product;
            ApplyState = applyState;
            ApplyError = applyError;
        }

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("version")]
        public int Version { get; }

        [JsonProperty("product")]
        public string Product { get; }

        [JsonProperty("apply_state")]
        public uint ApplyState { get; }

        [JsonProperty("apply_error")]
        public string ApplyError { get; }
    }
}

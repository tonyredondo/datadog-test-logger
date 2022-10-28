//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="RcmCachedTargetFile.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System.Collections.Generic;
using Datadog.Trace.Vendors.Datadog.Trace.Vendors.Newtonsoft.Json;

namespace Datadog.Trace.Vendors.Datadog.Trace.RemoteConfigurationManagement.Protocol
{
    internal class RcmCachedTargetFile
    {
        public RcmCachedTargetFile(string path, int length, List<RcmCachedTargetFileHash> hashes)
        {
            Path = path;
            Length = length;
            Hashes = hashes;
        }

        [JsonProperty("path")]
        public string Path { get; }

        [JsonProperty("length")]
        public int Length { get; }

        [JsonProperty("hashes")]
        public List<RcmCachedTargetFileHash> Hashes { get; }
    }
}
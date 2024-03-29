//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="AspNetTags.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using DatadogTestLogger.Vendors.Datadog.Trace.SourceGenerators;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Tagging
{
    internal partial class AspNetTags : WebTags
    {
        [Tag(Trace.Tags.AspNetRoute)]
        public string AspNetRoute { get; set; }

        [Tag(Trace.Tags.AspNetController)]
        public string AspNetController { get; set; }

        [Tag(Trace.Tags.AspNetAction)]
        public string AspNetAction { get; set; }

        [Tag(Trace.Tags.AspNetArea)]
        public string AspNetArea { get; set; }

        [Tag(Tags.HttpRoute)]
        public string HttpRoute { get; set; }
    }
}

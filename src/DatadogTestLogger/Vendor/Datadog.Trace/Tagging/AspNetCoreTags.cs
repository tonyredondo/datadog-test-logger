// <copyright file="AspNetCoreTags.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using Vendor.Datadog.Trace.SourceGenerators;

namespace Vendor.Datadog.Trace.Tagging
{
    internal partial class AspNetCoreTags : WebTags
    {
        private const string ComponentName = "aspnet_core";

        [Tag(Trace.Tags.InstrumentationName)]
        public string InstrumentationName => ComponentName;

        [Tag(Trace.Tags.AspNetCoreRoute)]
        public string AspNetCoreRoute { get; set; }
    }
}

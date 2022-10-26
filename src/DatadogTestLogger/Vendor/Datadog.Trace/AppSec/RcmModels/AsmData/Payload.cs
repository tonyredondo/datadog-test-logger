// <copyright file="Payload.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
#nullable enable
using Vendor.Datadog.Trace.Vendors.Newtonsoft.Json;

namespace Vendor.Datadog.Trace.AppSec.RcmModels.AsmData;

internal class Payload
{
    [JsonProperty("rules_data")]
    public RuleData[]? RulesData { get; set; }
}

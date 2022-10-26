﻿// <copyright file="Parameter.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using Vendor.Datadog.Trace.Vendors.Newtonsoft.Json;

namespace Vendor.Datadog.Trace.AppSec.Waf.ReturnTypes.Managed
{
    internal class Parameter
    {
        /// <summary>
        /// Gets or sets the address containing the value that triggered the rule. For example
        /// ``http.server.query``.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the part of the value that triggered the rule.
        /// </summary>
        [JsonProperty("highlight")]
        public string[] Highlight { get; set; }

        /// <summary>
        /// Gets or sets the path of the value that triggered the rule. For example ``["query", 0]`` to refer to
        /// the value in ``{"query": ["triggering value"]}``.
        /// </summary>
        [JsonProperty("key_path")]
        public object[] KeyPath { get; set; }

        /// <summary>
        /// Gets or sets the value that triggered the rule.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}

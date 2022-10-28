//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="HttpBypassHelper.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;

namespace Datadog.Trace.Vendors.Datadog.Trace.ClrProfiler.Helpers
{
    internal static class HttpBypassHelper
    {
        public static bool UriContainsAnyOf(Uri requestUri, string[] substrings)
        {
            if (requestUri == null || substrings == null || substrings.Length == 0)
            {
                return false;
            }

            var uriString = requestUri.ToString().ToUpperInvariant();
            for (var index = 0; index < substrings.Length; index++)
            {
                if (uriString.Contains(substrings[index]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
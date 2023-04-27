//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="ServiceNames.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Configuration
{
    internal class ServiceNames
    {
        private readonly Dictionary<string, string> _mappings = null;
        private readonly bool _unifyServiceNames;

        public ServiceNames(IDictionary<string, string> mappings, string metadataSchemaVersion)
        {
            _unifyServiceNames = metadataSchemaVersion == "v0" ? false : true;
            if (mappings?.Count > 0)
            {
                _mappings = new Dictionary<string, string>(mappings);
            }
        }

        public string GetServiceName(string applicationName, string key)
        {
            if (_mappings is not null && _mappings.TryGetValue(key, out var name))
            {
                return name;
            }
            else if (_unifyServiceNames)
            {
                return applicationName;
            }
            else
            {
                return $"{applicationName}-{key}";
            }
        }

        public bool TryGetServiceName(string key, out string name)
        {
            if (_mappings is not null && _mappings.TryGetValue(key, out name))
            {
                return true;
            }

            name = null;
            return false;
        }
    }
}

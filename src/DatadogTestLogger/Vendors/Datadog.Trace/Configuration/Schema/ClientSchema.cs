//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="ClientSchema.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable

using System.Collections.Generic;
using DatadogTestLogger.Vendors.Datadog.Trace.ServiceFabric;
using DatadogTestLogger.Vendors.Datadog.Trace.Tagging;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Configuration.Schema
{
    internal class ClientSchema
    {
        private readonly SchemaVersion _version;
        private readonly bool _peerServiceTagsEnabled;
        private readonly bool _removeClientServiceNamesEnabled;
        private readonly string _defaultServiceName;
        private readonly IReadOnlyDictionary<string, string>? _serviceNameMappings;

        public ClientSchema(SchemaVersion version, bool peerServiceTagsEnabled, bool removeClientServiceNamesEnabled, string defaultServiceName, IReadOnlyDictionary<string, string>? serviceNameMappings)
        {
            _version = version;
            _peerServiceTagsEnabled = peerServiceTagsEnabled;
            _removeClientServiceNamesEnabled = removeClientServiceNamesEnabled;
            _defaultServiceName = defaultServiceName;
            _serviceNameMappings = serviceNameMappings;
        }

        public string GetOperationNameForProtocol(string protocol) =>
            _version switch
            {
                SchemaVersion.V0 => $"{protocol}.request",
                _ => $"{protocol}.client.request",
            };

        public string GetOperationNameForRequestType(string requestType) =>
            _version switch
            {
                SchemaVersion.V0 => $"{requestType}",
                _ => $"{requestType}.request",
            };

        public string GetServiceName(string component)
        {
            if (_serviceNameMappings is not null && _serviceNameMappings.TryGetValue(component, out var mappedServiceName))
            {
                return mappedServiceName;
            }

            return _version switch
            {
                SchemaVersion.V0 when !_removeClientServiceNamesEnabled => $"{_defaultServiceName}-{component}",
                _ => _defaultServiceName,
            };
        }

        public HttpTags CreateHttpTags()
            => _version switch
            {
                SchemaVersion.V0 when !_peerServiceTagsEnabled => new HttpTags(),
                _ => new HttpV1Tags(),
            };

        public GrpcClientTags CreateGrpcClientTags()
            => _version switch
            {
                SchemaVersion.V0 when !_peerServiceTagsEnabled => new GrpcClientTags(),
                _ => new GrpcClientV1Tags(),
            };

        public ServiceRemotingClientTags CreateServiceRemotingClientTags()
            => _version switch
            {
                SchemaVersion.V0 when !_peerServiceTagsEnabled => new ServiceRemotingClientTags(),
                _ => new ServiceRemotingClientV1Tags(),
            };
    }
}

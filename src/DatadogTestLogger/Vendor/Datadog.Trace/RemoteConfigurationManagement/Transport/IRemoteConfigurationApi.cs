// <copyright file="IRemoteConfigurationApi.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System.Threading.Tasks;
using Vendor.Datadog.Trace.RemoteConfigurationManagement.Protocol;

namespace Vendor.Datadog.Trace.RemoteConfigurationManagement.Transport
{
    internal interface IRemoteConfigurationApi
    {
        Task<GetRcmResponse> GetConfigs(GetRcmRequest request);
    }
}

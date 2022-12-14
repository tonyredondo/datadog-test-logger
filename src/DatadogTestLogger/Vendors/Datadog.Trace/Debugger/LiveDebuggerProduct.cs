//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="LiveDebuggerProduct.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using DatadogTestLogger.Vendors.Datadog.Trace.Debugger.Helpers;
using DatadogTestLogger.Vendors.Datadog.Trace.RemoteConfigurationManagement;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Debugger;

internal class LiveDebuggerProduct : Product
{
    private readonly string _serviceName;
    private readonly string _uuid;

    public LiveDebuggerProduct(string serviceName)
    {
        _uuid = serviceName.ToUUID();
        _serviceName = serviceName;
    }

    public static string ProductName => "LIVE_DEBUGGING";

    public override string Name => ProductName;

    protected override bool RemoteConfigurationPredicate(RemoteConfiguration configuration)
    {
        return configuration.Path.Id == _serviceName || configuration.Path.Id == _uuid;
    }
}

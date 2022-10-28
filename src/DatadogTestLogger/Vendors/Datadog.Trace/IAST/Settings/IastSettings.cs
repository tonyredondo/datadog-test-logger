//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="IastSettings.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#nullable enable

using Datadog.Trace.Vendors.Datadog.Trace.Configuration;

namespace Datadog.Trace.Vendors.Datadog.Trace.Iast.Settings;

internal class IastSettings
{
    public static readonly string WeakCipherAlgorithmsDefault = "DES,TRIPLEDES,RC2";
    public static readonly string WeakHashAlgorithmsDefault = "HMACMD5,MD5,HMACSHA1,SHA1";

    public IastSettings(IConfigurationSource source)
    {
        WeakCipherAlgorithms = source?.GetString(ConfigurationKeys.Iast.WeakCipherAlgorithms) ?? WeakCipherAlgorithmsDefault;
        WeakCipherAlgorithmsArray = WeakCipherAlgorithms.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        WeakHashAlgorithms = source?.GetString(ConfigurationKeys.Iast.WeakHashAlgorithms) ?? WeakHashAlgorithmsDefault;
        WeakHashAlgorithmsArray = WeakHashAlgorithms.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        Enabled = (source?.GetBool(ConfigurationKeys.Iast.Enabled) ?? false) &&
            (WeakHashAlgorithmsArray.Length > 0 || WeakCipherAlgorithmsArray.Length > 0);
        DeduplicationEnabled = source?.GetBool(ConfigurationKeys.Iast.IsIastDeduplicationEnabled) ?? true;
    }

    public bool Enabled { get; set; }

    public string[] WeakHashAlgorithmsArray { get; }

    public bool DeduplicationEnabled { get; }

    public string WeakHashAlgorithms { get; }

    public string[] WeakCipherAlgorithmsArray { get; }

    public string WeakCipherAlgorithms { get; }

    public static IastSettings FromDefaultSources()
    {
        return new IastSettings(GlobalConfigurationSource.Instance);
    }
}
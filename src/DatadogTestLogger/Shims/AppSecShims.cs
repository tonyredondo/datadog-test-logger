// ReSharper disable once CheckNamespace

using DatadogTestLogger.Vendors.Datadog.Trace.RemoteConfigurationManagement;

namespace DatadogTestLogger.Vendors.Datadog.Trace.AppSec;

/// <summary>
/// Shim to make vendoring easier
/// </summary>
internal class SecuritySettings
{
    public bool Enabled => false;
    public string? Rules => null;
    public string? TraceRateLimit => null;
}

internal interface IEvent
{
}

internal class Security
{
    public static Security Instance { get; } = new();

    public SecuritySettings Settings { get; } = new();

    public string? DdlibWafVersion => null;

    public bool WafExportsErrorHappened => false;
}

internal static class AsmRemoteConfigurationProducts
{
    public static AsmShimProduct AsmFeaturesProduct { get; } = new();

    public static AsmShimProduct AsmDataProduct { get; } = new();

    public static AsmShimProduct AsmDDProduct { get; } = new();
}

internal class AsmShimProduct : Product
{
    public override string Name => "SHIM";
}

internal class BlockException : Exception
{
}
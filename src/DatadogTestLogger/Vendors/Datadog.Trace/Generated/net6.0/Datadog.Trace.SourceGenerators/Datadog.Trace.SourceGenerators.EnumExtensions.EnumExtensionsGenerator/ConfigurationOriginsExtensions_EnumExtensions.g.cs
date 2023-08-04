//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NET6_0_OR_GREATER
// <auto-generated />

#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.Configuration.Telemetry;

/// <summary>
/// Extension methods for <see cref="Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins" />
/// </summary>
internal static partial class ConfigurationOriginsExtensions
{
    /// <summary>
    /// The number of members in the enum.
    /// This is a non-distinct count of defined names.
    /// </summary>
    public const int Length = 7;

    /// <summary>
    /// Returns the string representation of the <see cref="Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins"/> value.
    /// If the attribute is decorated with a <c>[Description]</c> attribute, then
    /// uses the provided value. Otherwise uses the name of the member, equivalent to
    /// calling <c>ToString()</c> on <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to retrieve the string value for</param>
    /// <returns>The string representation of the value</returns>
    public static string ToStringFast(this Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins value)
        => value switch
        {
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.EnvVars => "env_var",
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.Code => "code",
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.DdConfig => "dd_config",
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.RemoteConfig => "remote_config",
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.AppConfig => "app.config",
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.Default => "default",
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.Unknown => "unknown",
            _ => value.ToString(),
        };

    /// <summary>
    /// Retrieves an array of the values of the members defined in
    /// <see cref="Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins" />.
    /// Note that this returns a new array with every invocation, so
    /// should be cached if appropriate.
    /// </summary>
    /// <returns>An array of the values defined in <see cref="Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins" /></returns>
    public static Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins[] GetValues()
        => new []
        {
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.EnvVars,
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.Code,
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.DdConfig,
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.RemoteConfig,
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.AppConfig,
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.Default,
            Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.Unknown,
        };

    /// <summary>
    /// Retrieves an array of the names of the members defined in
    /// <see cref="Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins" />.
    /// Note that this returns a new array with every invocation, so
    /// should be cached if appropriate.
    /// Ignores <c>[Description]</c> definitions.
    /// </summary>
    /// <returns>An array of the names of the members defined in <see cref="Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins" /></returns>
    public static string[] GetNames()
        => new []
        {
            nameof(Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.EnvVars),
            nameof(Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.Code),
            nameof(Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.DdConfig),
            nameof(Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.RemoteConfig),
            nameof(Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.AppConfig),
            nameof(Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.Default),
            nameof(Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins.Unknown),
        };

    /// <summary>
    /// Retrieves an array of the names of the members defined in
    /// <see cref="Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins" />.
    /// Note that this returns a new array with every invocation, so
    /// should be cached if appropriate.
    /// Uses <c>[Description]</c> definition if available, otherwise uses the name of the property
    /// </summary>
    /// <returns>An array of the names of the members defined in <see cref="Datadog.Trace.Configuration.Telemetry.ConfigurationOrigins" /></returns>
    public static string[] GetDescriptions()
        => new []
        {
            "env_var",
            "code",
            "dd_config",
            "remote_config",
            "app.config",
            "default",
            "unknown",
        };
}
#endif
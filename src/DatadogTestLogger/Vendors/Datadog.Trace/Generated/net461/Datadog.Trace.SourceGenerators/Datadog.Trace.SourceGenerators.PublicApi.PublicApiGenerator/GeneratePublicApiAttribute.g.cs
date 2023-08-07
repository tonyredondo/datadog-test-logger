//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NETFRAMEWORK
// <auto-generated/>
#nullable enable

namespace DatadogTestLogger.Vendors.Datadog.Trace.SourceGenerators;

/// <summary>
/// Used to generate a public property for a decorated field,
/// allowing adding aspect-oriented changes such as telemetry etc.
/// Any documentation added to the field is copied to the public API
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
internal class GeneratePublicApiAttribute : System.Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PublicApiAttribute"/> class.
    /// Adds a getter and a setter.
    /// </summary>
    /// <param name="getApiUsage">Gets the name of the public API used for the property getter</param>
    /// <param name="setApiUsage">Gets the name of the public API used for the property setter</param>
    public GeneratePublicApiAttribute(
        Datadog.Trace.Telemetry.Metrics.PublicApiUsage getApiUsage,
        Datadog.Trace.Telemetry.Metrics.PublicApiUsage setApiUsage)
    {
        Getter = getApiUsage;
        Setter = setApiUsage;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PublicApiAttribute"/> class.
    /// Adds a getter only.
    /// </summary>
    /// <param name="getApiUsage">Gets the name of the public API used for the property getter. If null, no getter will be generated.</param>
    public GeneratePublicApiAttribute(Datadog.Trace.Telemetry.Metrics.PublicApiUsage getApiUsage)
    {
        Getter = getApiUsage;
    }

    /// <summary>
    /// Gets the name of the public API used for the getter
    /// </summary>
    public Datadog.Trace.Telemetry.Metrics.PublicApiUsage Getter { get; }

    /// <summary>
    /// Gets the name of the public API used for the setter
    /// </summary>
    public Datadog.Trace.Telemetry.Metrics.PublicApiUsage? Setter { get; }
}

/// <summary>
/// A marker attribute added to a public API to indicate it should only be
/// called by consumers. Used by analyzers to confirm we're not calling a public API method.
/// </summary>
[System.Diagnostics.Conditional("DEBUG")]
[System.AttributeUsage(
    System.AttributeTargets.Field
  | System.AttributeTargets.Property
  | System.AttributeTargets.Method
  | System.AttributeTargets.Constructor)]
internal sealed class PublicApiAttribute : System.Attribute
{
}

#endif
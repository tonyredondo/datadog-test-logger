//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
using Datadog.Trace.Vendors.Datadog.Trace.Vendors.Serilog.Events;

namespace Datadog.Trace.Vendors.Datadog.Trace.Vendors.Serilog.Core
{
    /// <summary>
    /// Determine how a simple value is carried through the logging
    /// pipeline as an immutable <see cref="ScalarValue"/>.
    /// </summary>
    interface IScalarConversionPolicy
    {
        /// <summary>
        /// If supported, convert the provided value into an immutable scalar.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="result">The converted value, or null.</param>
        /// <returns>True if the value could be converted under this policy.</returns>
        bool TryConvertToScalar(object value, out ScalarValue result);
    }
}
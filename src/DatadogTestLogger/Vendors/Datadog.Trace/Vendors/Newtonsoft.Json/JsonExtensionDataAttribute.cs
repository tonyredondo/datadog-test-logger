//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011, SYSLIB0032
#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8604, CS8618, CS8620, CS8714, CS8762, CS8765, CS8766, CS8767, CS8768, CS8769, CS8612
#nullable enable
using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Newtonsoft.Json
{
    /// <summary>
    /// Instructs the <see cref="JsonSerializer"/> to deserialize properties with no matching class member into the specified collection
    /// and write values during serialization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    internal class JsonExtensionDataAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value that indicates whether to write extension data when serializing the object.
        /// </summary>
        /// <value>
        /// 	<c>true</c> to write extension data when serializing the object; otherwise, <c>false</c>. The default is <c>true</c>.
        /// </value>
        public bool WriteData { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to read extension data when deserializing the object.
        /// </summary>
        /// <value>
        /// 	<c>true</c> to read extension data when deserializing the object; otherwise, <c>false</c>. The default is <c>true</c>.
        /// </value>
        public bool ReadData { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonExtensionDataAttribute"/> class.
        /// </summary>
        public JsonExtensionDataAttribute()
        {
            WriteData = true;
            ReadData = true;
        }
    }
}
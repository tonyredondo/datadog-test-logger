//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011, SYSLIB0032
#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8604, CS8618, CS8620, CS8714, CS8762, CS8765, CS8766, CS8767, CS8768, CS8769, CS8612
#nullable enable
using System;

namespace Vendor.Datadog.Trace.Vendors.Newtonsoft.Json.Linq
{
    /// <summary>
    /// Specifies the settings used when selecting JSON.
    /// </summary>
    internal class JsonSelectSettings
    {
#if HAVE_REGEX_TIMEOUTS
        /// <summary>
        /// Gets or sets a timeout that will be used when executing regular expressions.
        /// </summary>
        /// <value>The timeout that will be used when executing regular expressions.</value>
        public TimeSpan? RegexMatchTimeout { get; set; }
#endif

        /// <summary>
        /// Gets or sets a flag that indicates whether an error should be thrown if
        /// no tokens are found when evaluating part of the expression.
        /// </summary>
        /// <value>
        /// A flag that indicates whether an error should be thrown if
        /// no tokens are found when evaluating part of the expression.
        /// </value>
        public bool ErrorWhenNoMatch { get; set; }
    }
}

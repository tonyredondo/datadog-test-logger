//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8604, CS8618, CS8620, CS8714, CS8762, CS8765, CS8766, CS8767, CS8768, CS8769, CS8612, CS8629, CS8774
#nullable enable
#if NETCOREAPP3_1_OR_GREATER
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.IndieSystem.Text.RegularExpressions
{
    /// <summary>
    /// When a regular expression specifies the option <see cref="RegexOptions.IgnoreCase"/> then comparisons between the input and the
    /// pattern will made case-insensitively. In order to support this, we need to define which case mappings shall be  used for the comparisons.
    /// A case mapping exists whenever you have two characters 'A' and 'B', where either 'A' is the ToLower() representation of 'B' or both 'A' and 'B' lowercase to the
    /// same character. Note that we don't consider a mapping when the only relationship between 'A' and 'B' is that one is the ToUpper() representation of the other. This
    /// is for backwards compatibility since, in Regex, we have only consider ToLower() for case insensitive comparisons. Given the case mappings vary depending on the culture,
    /// Regex supports 3 main different behaviors or mappings: Invariant, NonTurkish, and Turkish. This is in order to match the behavior of all .NET supported cultures
    /// current behavior for ToLower(). As a side note, there should be no cases where 'A'.ToLower() == 'B' but 'A'.ToLower() != 'B'.ToLower().
    /// </summary>
    internal enum RegexCaseBehavior
    {
        /// <summary>
        /// This means that the RegexCaseBehavior hasn't been calculated based on a passed in culture yet, so it will need to be calculated before the first
        /// equivalence check by calling <see cref="RegexCaseEquivalences.GetRegexBehavior(CultureInfo)"/>
        /// </summary>
        NotSet,

        /// <summary>
        /// Invariant case-mappings are used. This includes all of the common mappings across cultures. This behavior is used when either the  user
        /// specified <see cref="RegexOptions.CultureInvariant"/> or when the CurrentCulture is <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        Invariant,

        /// <summary>
        /// These are all the same mappings used by Invariant behavior, with an additional one: \u0130 => \u0069
        /// This mode will be used when CurrentCulture is not Invariant or any of the tr/az cultures.
        /// </summary>
        NonTurkish,

        /// <summary>
        /// These are all the same mappings used by non-Turkish behavior, with the exception of: \u0049 => \u0069 which mapping doesn't exist
        /// on this behavior and with the additional mapping of: \u0069 => \u0131. This mode will be used when CurrentCulture is any of the tr/az cultures.
        /// </summary>
        Turkish
    }
}

#endif
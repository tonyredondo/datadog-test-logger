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

using System;
using System.Collections.Generic;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.IndieSystem.Text.RegularExpressions.Symbolic
{
    /// <summary>
    /// Provides support for operating over sets, including and/or/not operations, converting to/from
    /// <see cref="BDD"/> representations of those sets, and determining whether an element is in the set.
    /// </summary>
    internal interface ISolver<TSet>
    {
        /// <summary>Creates a set from a <see cref="BDD"/> representation.</summary>
        TSet ConvertFromBDD(BDD set, CharSetSolver solver);

        /// <summary>Gets the minterms from the set.</summary>
        TSet[]? GetMinterms();

        /// <summary>Gets a full set (one that contains all values).</summary>
        TSet Full { get; }

        /// <summary>Gets an empty set (one that contains no values).</summary>
        TSet Empty { get; }

        /// <summary>Intersects two sets to produce a new one that contains only the elements that's in both (conjunction).</summary>
        TSet And(TSet set1, TSet set2);

        /// <summary>Unions two sets to produce a new one that contains elements that are in either or both (disjunction).</summary>
        TSet Or(TSet set1, TSet set2);

        /// <summary>Unions all of the sets in <paramref name="sets"/> to produce a new one that contains elements that are in any of the sets (disjunction).</summary>
        TSet Or(ReadOnlySpan<TSet> sets);

        /// <summary>Negates the set, producing a new one that contains the elements and only the elements not in the original.</summary>
        TSet Not(TSet set);

        /// <summary>Gets whether the set contains no elements.</summary>
        bool IsEmpty(TSet set);

        /// <summary>Gets whether the set contains every element.</summary>
        bool IsFull(TSet set);

#if DEBUG
        /// <summary>Formats the contents of the specified set for human consumption.</summary>
        string PrettyPrint(TSet set, CharSetSolver solver);

        /// <summary>Converts the set into a <see cref="BDD"/> representation.</summary>
        BDD ConvertToBDD(TSet set, CharSetSolver solver);
#endif
    }
}

#endif
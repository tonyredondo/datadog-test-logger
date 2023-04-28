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

#if DEBUG
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.IndieSystem.Text.RegularExpressions.Symbolic;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.IndieSystem.Text.RegularExpressions
{
    internal partial class Regex
    {
        /// <summary>Unwind the regex and save the resulting state graph in DGML</summary>
        /// <param name="writer">Writer to which the DGML is written.</param>
        /// <param name="maxLabelLength">maximum length of labels in nodes anything over that length is indicated with .. </param>
        [ExcludeFromCodeCoverage(
#if NET5_0_OR_GREATER
            Justification = "Debug only"
#endif
            )]
        internal void SaveDGML(TextWriter writer, int maxLabelLength)
        {
            if (factory is not SymbolicRegexRunnerFactory srmFactory)
            {
                throw new NotSupportedException();
            }

            srmFactory._matcher.SaveDGML(writer, maxLabelLength);
        }

        /// <summary>
        /// Generates UnicodeCategoryRanges.cs for the namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.IndieSystem.Text.RegularExpressions.Symbolic.Unicode
        /// in the given directory path. Only avaliable in DEBUG mode.
        /// </summary>
        [ExcludeFromCodeCoverage(
#if NET5_0_OR_GREATER
           Justification = "Debug only"
#endif
            )]
        internal static void GenerateUnicodeTables(string path)
        {
            UnicodeCategoryRangesGenerator.Generate("System.Text.RegularExpressions.Symbolic", "UnicodeCategoryRanges", path);
        }

        /// <summary>
        /// Generates up to k random strings matched by the regex
        /// </summary>
        /// <param name="k">upper bound on the number of generated strings</param>
        /// <param name="randomseed">random seed for the generator, 0 means no random seed</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage(
#if NET5_0_OR_GREATER
            Justification = "Debug only"
#endif
            )]
        internal IEnumerable<string> SampleMatches(int k, int randomseed)
        {
            if (factory is not SymbolicRegexRunnerFactory srmFactory)
            {
                throw new NotSupportedException();
            }

            return srmFactory._matcher.SampleMatches(k, randomseed);
        }

        /// <summary>
        /// Explore transitions of the DFA and/or NFA exhaustively. DFA exploration, if requested, is done only up to the
        /// DFA state limit. NFA exploration, if requested, continues from the states unexplored by the DFA exploration,
        /// or from the initial states if DFA exploration was not requested. NFA exploration will always finish.
        /// </summary>
        /// <remarks>
        /// This may result in a different automaton being explored than matching would produce, since if the limit for
        /// the number of DFA states is reached then the order in which states and transitions are explored is significant.
        /// During matching that order is driven by the input, while this function may use any order (currently it is
        /// breadth-first).
        /// </remarks>
        /// <param name="includeDotStarred">whether to explore the .*? prefixed version of the pattern</param>
        /// <param name="includeReverse">whether to explore the reversed pattern</param>
        /// <param name="includeOriginal">whether to explore the original pattern</param>
        /// <param name="exploreDfa">whether to explore DFA transitions</param>
        /// <param name="exploreNfa">whether to explore NFA transitions</param>
        [ExcludeFromCodeCoverage(
#if NET5_0_OR_GREATER
            Justification = "Debug only"
#endif
            )]
        internal void Explore(bool includeDotStarred, bool includeReverse, bool includeOriginal, bool exploreDfa, bool exploreNfa)
        {
            if (factory is not SymbolicRegexRunnerFactory srmFactory)
            {
                throw new NotSupportedException();
            }

            srmFactory._matcher.Explore(includeDotStarred, includeReverse, includeOriginal, exploreDfa, exploreNfa);
        }
    }
}
#endif

#endif
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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.IndieSystem.Text.RegularExpressions
{
    internal partial class Regex
    {
        /// <summary>
        /// Splits the <paramref name="input "/>string at the position defined
        /// by <paramref name="pattern"/>.
        /// </summary>
        public static string[] Split(string input, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern) =>
            RegexCache.GetOrAdd(pattern).Split(input);

        /// <summary>
        /// Splits the <paramref name="input "/>string at the position defined by <paramref name="pattern"/>.
        /// </summary>
        public static string[] Split(string input, [StringSyntax(StringSyntaxAttribute.Regex, "options")] string pattern, RegexOptions options) =>
            RegexCache.GetOrAdd(pattern, options, s_defaultMatchTimeout).Split(input);

        public static string[] Split(string input, [StringSyntax(StringSyntaxAttribute.Regex, "options")] string pattern, RegexOptions options, TimeSpan matchTimeout) =>
            RegexCache.GetOrAdd(pattern, options, matchTimeout).Split(input);

        /// <summary>
        /// Splits the <paramref name="input"/> string at the position defined by a
        /// previous pattern.
        /// </summary>
        public string[] Split(string input)
        {
            if (input is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
            }

            return Split(this, input, 0, RightToLeft ? input.Length : 0);
        }

        /// <summary>
        /// Splits the <paramref name="input"/> string at the position defined by a
        /// previous pattern.
        /// </summary>
        public string[] Split(string input, int count)
        {
            if (input is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
            }

            return Split(this, input, count, RightToLeft ? input.Length : 0);
        }

        /// <summary>
        /// Splits the <paramref name="input"/> string at the position defined by a previous pattern.
        /// </summary>
        public string[] Split(string input, int count, int startat)
        {
            if (input is null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
            }

            return Split(this, input, count, startat);
        }

        /// <summary>
        /// Does a split. In the right-to-left case we reorder the
        /// array to be forwards.
        /// </summary>
        private static string[] Split(Regex regex, string input, int count, int startat)
        {
            if (count < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.CountTooSmall);
            }
            if ((uint)startat > (uint)input.Length)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startat, ExceptionResource.BeginIndexNotNegative);
            }

            if (count == 1)
            {
                return new[] { input };
            }

            count--;
            var state = (results: new List<string>(), prevat: 0, input, count);

            if (!regex.RightToLeft)
            {
                regex.RunAllMatchesWithCallback(input, startat, ref state, static (ref (List<string> results, int prevat, string input, int count) state, Match match) =>
                {
                    state.results.Add(state.input.Substring(state.prevat, match.Index - state.prevat));
                    state.prevat = match.Index + match.Length;

                    // add all matched capture groups to the list.
                    for (int i = 1; i < match.Groups.Count; i++)
                    {
                        if (match.IsMatched(i))
                        {
                            state.results.Add(match.Groups[i].Value);
                        }
                    }

                    return --state.count != 0;
                }, RegexRunnerMode.FullMatchRequired, reuseMatchObject: true);

                if (state.results.Count == 0)
                {
                    return new[] { input };
                }

                state.results.Add(input.Substring(state.prevat));
            }
            else
            {
                state.prevat = input.Length;

                regex.RunAllMatchesWithCallback(input, startat, ref state, static (ref (List<string> results, int prevat, string input, int count) state, Match match) =>
                {
                    state.results.Add(state.input.Substring(match.Index + match.Length, state.prevat - match.Index - match.Length));
                    state.prevat = match.Index;

                    // add all matched capture groups to the list.
                    for (int i = 1; i < match.Groups.Count; i++)
                    {
                        if (match.IsMatched(i))
                        {
                            state.results.Add(match.Groups[i].Value);
                        }
                    }

                    return --state.count != 0;
                }, RegexRunnerMode.FullMatchRequired, reuseMatchObject: true);

                if (state.results.Count == 0)
                {
                    return new[] { input };
                }

                state.results.Add(input.Substring(0, state.prevat));
                state.results.Reverse(0, state.results.Count);
            }

            return state.results.ToArray();
        }
    }
}

#endif
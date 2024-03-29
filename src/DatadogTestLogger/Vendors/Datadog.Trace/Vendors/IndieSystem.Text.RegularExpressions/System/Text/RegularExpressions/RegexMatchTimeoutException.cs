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
using System.Runtime.Serialization;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.IndieSystem.Text.RegularExpressions
{
    /// <summary>
    /// This is the exception that is thrown when a RegEx matching timeout occurs.
    /// </summary>
    [Serializable]
    [System.Runtime.CompilerServices.TypeForwardedFrom("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
    internal class RegexMatchTimeoutException : TimeoutException, ISerializable
    {
        /// <summary>
        /// Constructs a new RegexMatchTimeoutException.
        /// </summary>
        /// <param name="regexInput">Matching timeout occurred during matching within the specified input.</param>
        /// <param name="regexPattern">Matching timeout occurred during matching to the specified pattern.</param>
        /// <param name="matchTimeout">Matching timeout occurred because matching took longer than the specified timeout.</param>
        public RegexMatchTimeoutException(string regexInput, string regexPattern, TimeSpan matchTimeout)
            : base(SR.RegexMatchTimeoutException_Occurred)
        {
            Input = regexInput;
            Pattern = regexPattern;
            MatchTimeout = matchTimeout;
        }

        /// <summary>
        /// This constructor is provided in compliance with common .NET Framework design patterns;
        /// developers should prefer using the constructor
        /// <code>public RegexMatchTimeoutException(string input, string pattern, TimeSpan matchTimeout)</code>.
        /// </summary>
        public RegexMatchTimeoutException() { }

        /// <summary>
        /// This constructor is provided in compliance with common .NET Framework design patterns;
        /// developers should prefer using the constructor
        /// <code>public RegexMatchTimeoutException(string input, string pattern, TimeSpan matchTimeout)</code>.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public RegexMatchTimeoutException(string message) : base(message) { }

        /// <summary>
        /// This constructor is provided in compliance with common .NET Framework design patterns;
        /// developers should prefer using the constructor
        /// <code>public RegexMatchTimeoutException(string input, string pattern, TimeSpan matchTimeout)</code>.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a <code>null</code>.</param>
        public RegexMatchTimeoutException(string message, Exception inner) : base(message, inner) { }

        protected RegexMatchTimeoutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Input = info.GetString("regexInput")!;
            Pattern = info.GetString("regexPattern")!;
            MatchTimeout = new TimeSpan(info.GetInt64("timeoutTicks"));
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("regexInput", Input);
            info.AddValue("regexPattern", Pattern);
            info.AddValue("timeoutTicks", MatchTimeout.Ticks);
        }

        public string Input { get; } = string.Empty;

        public string Pattern { get; } = string.Empty;

        public TimeSpan MatchTimeout { get; } = TimeSpan.FromTicks(-1);
    }
}

#endif
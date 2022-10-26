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
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

#if NETFRAMEWORK || NETSTANDARD


// This file provides helpers used to help compile some Regex source code (e.g. RegexParser) as part of the netstandard2.0 generator assembly.

namespace System.Text
{
    internal static class StringBuilderExtensions
    {
        public static unsafe StringBuilder Append(this StringBuilder stringBuilder, ReadOnlySpan<char> span)
        {
            fixed (char* ptr = &MemoryMarshal.GetReference(span))
            {
                return stringBuilder.Append(ptr, span.Length);
            }
        }

        public static ReadOnlyMemory<char>[] GetChunks(this StringBuilder stringBuilder)
        {
            var chars = new char[stringBuilder.Length];
            stringBuilder.CopyTo(0, chars, 0, chars.Length);
            return new[] { new ReadOnlyMemory<char>(chars) };
        }
    }
}

namespace System
{
    internal static partial class StringExtensions
    {
        public static string Create<TState>(int length, TState state, SpanAction<char, TState> action)
        {
            Span<char> span = length <= 256 ? stackalloc char[length] : new char[length];
            action(span, state);
            return span.ToString();
        }
    }
}

namespace System.Buffers
{
    internal delegate void SpanAction<T, in TArg>(Span<T> span, TArg arg);
}

#endif

namespace System.Threading
{
    internal static class InterlockedExtensions
    {
        public static uint Or(ref uint location1, uint value)
        {
            uint current = location1;
            while (true)
            {
                uint newValue = current | value;
                uint oldValue = (uint)Interlocked.CompareExchange(ref Unsafe.As<uint, int>(ref location1), (int)newValue, (int)current);
                if (oldValue == current)
                {
                    return oldValue;
                }
                current = oldValue;
            }
        }
    }
}

namespace System
{
    internal static partial class StringExtensions
    {
        public static int CommonPrefixLength(this ReadOnlySpan<char> span, ReadOnlySpan<char> other)
        {
            int length = Math.Min(span.Length, other.Length);

            for (int i = 0; i < length; i++)
            {
                if (span[i] != other[i])
                {
                    return i;
                }
            }

            return length;
        }
    }
}

#endif
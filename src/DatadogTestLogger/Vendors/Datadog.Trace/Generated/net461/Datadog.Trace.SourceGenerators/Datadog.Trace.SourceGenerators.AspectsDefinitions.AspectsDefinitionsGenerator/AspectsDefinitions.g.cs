//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NETFRAMEWORK
// <auto-generated/>
#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DatadogTestLogger.Vendors.Datadog.Trace.ClrProfiler
{
    internal static partial class AspectDefinitions
    {
        public static string[] Aspects = new string[] {
"[AspectClass(\"mscorlib,System.Security.Cryptography.Primitives,System.Security.Cryptography\",[None],Propagation,[])] Datadog.Trace.Iast.Aspects.HashAlgorithmAspect",
"  [AspectMethodInsertBefore(\"System.Security.Cryptography.HashAlgorithm::ComputeHash(System.Byte[])\",\"\",[1],[False],[None],Propagation,[])] ComputeHash(System.Security.Cryptography.HashAlgorithm)",
"  [AspectMethodInsertBefore(\"System.Security.Cryptography.HashAlgorithm::ComputeHash(System.Byte[],System.Int32,System.Int32)\",\"\",[3],[False],[None],Propagation,[])] ComputeHash(System.Security.Cryptography.HashAlgorithm)",
"  [AspectMethodInsertBefore(\"System.Security.Cryptography.HashAlgorithm::ComputeHash(System.IO.Stream)\",\"\",[1],[False],[None],Propagation,[])] ComputeHash(System.Security.Cryptography.HashAlgorithm)",
"  [AspectMethodInsertBefore(\"System.Security.Cryptography.HashAlgorithm::ComputeHashAsync(System.IO.Stream,System.Threading.CancellationToken)\",\"\",[2],[False],[None],Propagation,[])] ComputeHash(System.Security.Cryptography.HashAlgorithm)",
        };
    }
}

#endif
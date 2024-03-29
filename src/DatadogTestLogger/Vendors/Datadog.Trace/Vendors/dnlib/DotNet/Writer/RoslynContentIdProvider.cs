//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.Writer {
	static class RoslynContentIdProvider {
		public static void GetContentId(byte[] hash, out Guid guid, out uint timestamp) {
			if (hash.Length < 20)
				throw new InvalidOperationException();
			var guidBytes = new byte[16];
			Array.Copy(hash, 0, guidBytes, 0, guidBytes.Length);
			guidBytes[7] = (byte)((guidBytes[7] & 0x0F) | 0x40);
			guidBytes[8] = (byte)((guidBytes[8] & 0x3F) | 0x80);
			guid = new Guid(guidBytes);
			timestamp = 0x80000000 | (uint)((hash[19] << 24) | (hash[18] << 16) | (hash[17] << 8) | hash[16]);
		}
	}
}

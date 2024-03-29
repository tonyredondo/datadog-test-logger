//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.Writer {
	/// <summary>
	/// Extension methods
	/// </summary>
	static partial class Extensions {
		/// <summary>
		/// Write zeros
		/// </summary>
		/// <param name="writer">this</param>
		/// <param name="count">Number of zeros</param>
		public static void WriteZeroes(this DataWriter writer, int count) {
			while (count >= 8) {
				writer.WriteUInt64(0);
				count -= 8;
			}
			for (int i = 0; i < count; i++)
				writer.WriteByte(0);
		}
	}
}

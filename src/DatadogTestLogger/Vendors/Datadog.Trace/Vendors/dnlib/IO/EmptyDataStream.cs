//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

using System.Text;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.IO {
	sealed unsafe class EmptyDataStream : DataStream {
		public static readonly DataStream Instance = new EmptyDataStream();

		EmptyDataStream() { }

		public override void ReadBytes(uint offset, void* destination, int length) {
			var p = (byte*)destination;
			for (int i = 0; i < length; i++)
				*p = 0;
		}
		public override void ReadBytes(uint offset, byte[] destination, int destinationIndex, int length) {
			for (int i = 0; i < length; i++)
				destination[destinationIndex + i] = 0;
		}
		public override byte ReadByte(uint offset) => 0;
		public override ushort ReadUInt16(uint offset) => 0;
		public override uint ReadUInt32(uint offset) => 0;
		public override ulong ReadUInt64(uint offset) => 0;
		public override float ReadSingle(uint offset) => 0;
		public override double ReadDouble(uint offset) => 0;
		public override string ReadUtf16String(uint offset, int chars) => string.Empty;
		public override string ReadString(uint offset, int length, Encoding encoding) => string.Empty;
		public override bool TryGetOffsetOf(uint offset, uint endOffset, byte value, out uint valueOffset) {
			valueOffset = 0;
			return false;
		}
	}
}

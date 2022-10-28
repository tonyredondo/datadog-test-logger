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

using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.IO {
	/// <summary>
	/// Creates <see cref="DataReader"/>s that read native memory
	/// </summary>
	internal sealed unsafe class NativeMemoryDataReaderFactory : DataReaderFactory {
		/// <summary>
		/// The filename or null if the data is not from a file
		/// </summary>
		public override string Filename => filename;

		/// <summary>
		/// Gets the total length of the data
		/// </summary>
		public override uint Length => length;

		DataStream stream;
		string filename;
		uint length;

		NativeMemoryDataReaderFactory(byte* data, uint length, string filename) {
			this.filename = filename;
			this.length = length;
			stream = DataStreamFactory.Create(data);
		}

		internal void SetLength(uint length) => this.length = length;

		/// <summary>
		/// Creates a <see cref="NativeMemoryDataReaderFactory"/> instance
		/// </summary>
		/// <param name="data">Pointer to data</param>
		/// <param name="length">Length of data</param>
		/// <param name="filename">The filename or null if the data is not from a file</param>
		/// <returns></returns>
		public static NativeMemoryDataReaderFactory Create(byte* data, uint length, string filename) {
			if (data is null)
				throw new ArgumentNullException(nameof(data));
			return new NativeMemoryDataReaderFactory(data, length, filename);
		}

		/// <summary>
		/// Creates a data reader
		/// </summary>
		/// <param name="offset">Offset of data</param>
		/// <param name="length">Length of data</param>
		/// <returns></returns>
		public override DataReader CreateReader(uint offset, uint length) => CreateReader(stream, offset, length);

		/// <summary>
		/// This method doesn't need to be called since this instance doesn't own the native memory
		/// </summary>
		public override void Dispose() {
			stream = EmptyDataStream.Instance;
			length = 0;
			filename = null;
		}
	}
}

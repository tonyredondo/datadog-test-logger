//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

using System;
using Vendor.Datadog.Trace.Vendors.dnlib.IO;
using Vendor.Datadog.Trace.Vendors.dnlib.PE;

namespace Vendor.Datadog.Trace.Vendors.dnlib.DotNet.Writer {
	/// <summary>
	/// A <see cref="DataReader"/> chunk
	/// </summary>
	internal class DataReaderChunk : IChunk {
		FileOffset offset;
		RVA rva;
		DataReader data;
		readonly uint virtualSize;
		bool setOffsetCalled;

		/// <inheritdoc/>
		public FileOffset FileOffset => offset;

		/// <inheritdoc/>
		public RVA RVA => rva;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="data">The data</param>
		public DataReaderChunk(DataReader data)
			: this(ref data) {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="data">The data</param>
		/// <param name="virtualSize">Virtual size of <paramref name="data"/></param>
		public DataReaderChunk(DataReader data, uint virtualSize)
			: this(ref data, virtualSize) {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="data">The data</param>
		internal DataReaderChunk(ref DataReader data)
			: this(ref data, (uint)data.Length) {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="data">The data</param>
		/// <param name="virtualSize">Virtual size of <paramref name="data"/></param>
		internal DataReaderChunk(ref DataReader data, uint virtualSize) {
			this.data = data;
			this.virtualSize = virtualSize;
		}

		/// <summary>
		/// Gets the data reader
		/// </summary>
		public DataReader CreateReader() => data;

		/// <summary>
		/// Replaces the old data with new data. The new data must be the same size as the old data if
		/// <see cref="SetOffset(FileOffset, RVA)"/> has been called. That method gets called after
		/// event <see cref="ModuleWriterEvent.BeginCalculateRvasAndFileOffsets"/>
		/// </summary>
		/// <param name="newData"></param>
		public void SetData(DataReader newData) {
			if (setOffsetCalled && newData.Length != data.Length)
				throw new InvalidOperationException("New data must be the same size as the old data after SetOffset() has been called");
			data = newData;
		}

		/// <inheritdoc/>
		public void SetOffset(FileOffset offset, RVA rva) {
			this.offset = offset;
			this.rva = rva;
			setOffsetCalled = true;
		}

		/// <inheritdoc/>
		public uint GetFileLength() => (uint)data.Length;

		/// <inheritdoc/>
		public uint GetVirtualSize() => virtualSize;

		/// <inheritdoc/>
		public void WriteTo(DataWriter writer) {
			data.Position = 0;
			data.CopyTo(writer);
		}
	}
}

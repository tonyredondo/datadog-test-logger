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
using System.Runtime.InteropServices;
using System.Threading;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.MD;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.Pdb.Dss {
	sealed unsafe class ReaderMetaDataImport : MetaDataImport, IDisposable {
		Metadata metadata;
		byte* blobPtr;
		IntPtr addrToFree;

		public ReaderMetaDataImport(Metadata metadata) {
			this.metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
			var reader = metadata.BlobStream.CreateReader();
			addrToFree = Marshal.AllocHGlobal((int)reader.BytesLeft);
			blobPtr = (byte*)addrToFree;
			if (blobPtr is null)
				throw new OutOfMemoryException();
			reader.ReadBytes(blobPtr, (int)reader.BytesLeft);
		}

		~ReaderMetaDataImport() => Dispose(false);

		public override void GetTypeRefProps(uint tr, uint* ptkResolutionScope, ushort* szName, uint cchName, uint* pchName) {
			var token = new MDToken(tr);
			if (token.Table != Table.TypeRef)
				throw new ArgumentException();
			if (!metadata.TablesStream.TryReadTypeRefRow(token.Rid, out var row))
				throw new ArgumentException();
			if (ptkResolutionScope is not null)
				*ptkResolutionScope = row.ResolutionScope;
			if (szName is not null || pchName is not null) {
				var typeNamespace = metadata.StringsStream.ReadNoNull(row.Namespace);
				var typeName = metadata.StringsStream.ReadNoNull(row.Name);
				CopyTypeName(typeNamespace, typeName, szName, cchName, pchName);
			}
		}

		public override void GetTypeDefProps(uint td, ushort* szTypeDef, uint cchTypeDef, uint* pchTypeDef, uint* pdwTypeDefFlags, uint* ptkExtends) {
			var token = new MDToken(td);
			if (token.Table != Table.TypeDef)
				throw new ArgumentException();
			if (!metadata.TablesStream.TryReadTypeDefRow(token.Rid, out var row))
				throw new ArgumentException();
			if (pdwTypeDefFlags is not null)
				*pdwTypeDefFlags = row.Flags;
			if (ptkExtends is not null)
				*ptkExtends = row.Extends;
			if (szTypeDef is not null || pchTypeDef is not null) {
				var typeNamespace = metadata.StringsStream.ReadNoNull(row.Namespace);
				var typeName = metadata.StringsStream.ReadNoNull(row.Name);
				CopyTypeName(typeNamespace, typeName, szTypeDef, cchTypeDef, pchTypeDef);
			}
		}

		public override void GetSigFromToken(uint mdSig, byte** ppvSig, uint* pcbSig) {
			var token = new MDToken(mdSig);
			if (token.Table != Table.StandAloneSig)
				throw new ArgumentException();
			if (!metadata.TablesStream.TryReadStandAloneSigRow(token.Rid, out var row))
				throw new ArgumentException();
			if (!metadata.BlobStream.TryCreateReader(row.Signature, out var reader))
				throw new ArgumentException();
			if (ppvSig is not null)
				*ppvSig = blobPtr + (reader.StartOffset - (uint)metadata.BlobStream.StartOffset);
			if (pcbSig is not null)
				*pcbSig = reader.Length;
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		void Dispose(bool disposing) {
			metadata = null;
			var addrToFreeTmp = Interlocked.Exchange(ref addrToFree, IntPtr.Zero);
			blobPtr = null;
			if (addrToFreeTmp != IntPtr.Zero)
				Marshal.FreeHGlobal(addrToFreeTmp);
		}
	}
}

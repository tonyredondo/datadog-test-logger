//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

using System;
using System.IO;
using System.Security;
using Vendor.Datadog.Trace.Vendors.dnlib.IO;

namespace Vendor.Datadog.Trace.Vendors.dnlib.DotNet.Pdb {
	static class DataReaderFactoryUtils {
		public static DataReaderFactory TryCreateDataReaderFactory(string filename) {
			try {
				if (!File.Exists(filename))
					return null;
				// Don't use memory mapped I/O
				return ByteArrayDataReaderFactory.Create(File.ReadAllBytes(filename), filename);
			}
			catch (IOException) {
			}
			catch (UnauthorizedAccessException) {
			}
			catch (SecurityException) {
			}
			return null;
		}
	}
}

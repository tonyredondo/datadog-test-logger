//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2015 Jb Evain
// Copyright (c) 2008 - 2011 Novell, Inc.
//
// Licensed under the MIT/X11 license.
//

using System;

using RVA = System.UInt32;

namespace DatadogCollector.Vendors.Mono.Cecil.PE {

	struct DataDirectory {

		public readonly RVA VirtualAddress;
		public readonly uint Size;

		public bool IsZero {
			get { return VirtualAddress == 0 && Size == 0; }
		}

		public DataDirectory (RVA rva, uint size)
		{
			this.VirtualAddress = rva;
			this.Size = size;
		}
	}
}
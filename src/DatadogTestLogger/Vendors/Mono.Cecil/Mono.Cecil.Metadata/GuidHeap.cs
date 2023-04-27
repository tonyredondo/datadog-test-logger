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

namespace DatadogTestLogger.Vendors.Mono.Cecil.Metadata {

	sealed class GuidHeap : Heap {

		public GuidHeap (byte [] data)
			: base (data)
		{
		}

		public Guid Read (uint index)
		{
			const int guid_size = 16;

			if (index == 0 || ((index - 1) + guid_size) > data.Length)
				return new Guid ();

			var buffer = new byte [guid_size];

			Buffer.BlockCopy (this.data, (int) ((index - 1) * guid_size), buffer, 0, guid_size);

			return new Guid (buffer);
		}
	}
}

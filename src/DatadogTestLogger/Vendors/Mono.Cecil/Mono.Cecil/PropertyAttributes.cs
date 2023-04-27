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

namespace DatadogTestLogger.Vendors.Mono.Cecil {

	[Flags]
	internal enum PropertyAttributes : ushort {
		None			= 0x0000,
		SpecialName		= 0x0200,	// Property is special
		RTSpecialName	= 0x0400,	// Runtime(metadata internal APIs) should check name encoding
		HasDefault		= 0x1000,	// Property has default
		Unused			= 0xe9ff	 // Reserved: shall be zero in a conforming implementation
	}
}

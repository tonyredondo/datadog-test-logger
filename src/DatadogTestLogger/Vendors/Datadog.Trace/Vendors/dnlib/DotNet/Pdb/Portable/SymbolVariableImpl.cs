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

using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.Pdb.Symbols;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.Pdb.Portable {
	sealed class SymbolVariableImpl : SymbolVariable {
		readonly string name;
		readonly PdbLocalAttributes attributes;
		readonly int index;
		readonly PdbCustomDebugInfo[] customDebugInfos;

		public override string Name => name;
		public override PdbLocalAttributes Attributes => attributes;
		public override int Index => index;
		public override PdbCustomDebugInfo[] CustomDebugInfos => customDebugInfos;

		public SymbolVariableImpl(string name, PdbLocalAttributes attributes, int index, PdbCustomDebugInfo[] customDebugInfos) {
			this.name = name;
			this.attributes = attributes;
			this.index = index;
			this.customDebugInfos = customDebugInfos;
		}
	}
}

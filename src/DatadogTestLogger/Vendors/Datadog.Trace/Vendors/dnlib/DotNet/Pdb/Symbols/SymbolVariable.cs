//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.Pdb.Symbols {
	/// <summary>
	/// A variable
	/// </summary>
	internal abstract class SymbolVariable {
		/// <summary>
		/// Gets the name
		/// </summary>
		public abstract string Name { get; }

		/// <summary>
		/// Gets the attributes
		/// </summary>
		public abstract PdbLocalAttributes Attributes { get; }

		/// <summary>
		/// Gets the index of the variable
		/// </summary>
		public abstract int Index { get; }

		/// <summary>
		/// Gets all custom debug infos
		/// </summary>
		public abstract PdbCustomDebugInfo[] CustomDebugInfos { get; }
	}
}

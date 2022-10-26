//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

using System.Collections.Generic;

namespace Vendor.Datadog.Trace.Vendors.dnlib.DotNet.Pdb {
	/// <summary>
	/// A constant in a method scope, eg. "const int SomeConstant = 123;"
	/// </summary>
	internal sealed class PdbConstant : IHasCustomDebugInformation {
		string name;
		TypeSig type;
		object value;

		/// <summary>
		/// Gets/sets the name
		/// </summary>
		public string Name {
			get => name;
			set => name = value;
		}

		/// <summary>
		/// Gets/sets the type of the constant
		/// </summary>
		public TypeSig Type {
			get => type;
			set => type = value;
		}

		/// <summary>
		/// Gets/sets the value of the constant
		/// </summary>
		public object Value {
			get => value;
			set => this.value = value;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public PdbConstant() {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Name of constant</param>
		/// <param name="type">Type of constant</param>
		/// <param name="value">Constant value</param>
		public PdbConstant(string name, TypeSig type, object value) {
			this.name = name;
			this.type = type;
			this.value = value;
		}

		/// <inheritdoc/>
		public int HasCustomDebugInformationTag => 25;

		/// <inheritdoc/>
		public bool HasCustomDebugInfos => CustomDebugInfos.Count > 0;

		/// <summary>
		/// Gets all custom debug infos
		/// </summary>
		public IList<PdbCustomDebugInfo> CustomDebugInfos => customDebugInfos;
		readonly IList<PdbCustomDebugInfo> customDebugInfos = new List<PdbCustomDebugInfo>();

		/// <summary>
		/// ToString()
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			var type = Type;
			return (type is null ? "" : type.ToString()) + " " + Name + " = " + (Value is null ? "null" : Value.ToString() + " (" + Value.GetType().FullName + ")");
		}
	}
}

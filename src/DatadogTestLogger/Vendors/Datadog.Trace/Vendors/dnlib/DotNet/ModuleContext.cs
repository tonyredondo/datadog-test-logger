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

using System.Threading;
using Datadog.Trace.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.Emit;

namespace Datadog.Trace.Vendors.Datadog.Trace.Vendors.dnlib.DotNet {
	/// <summary>
	/// <see cref="ModuleDef"/> context
	/// </summary>
	internal class ModuleContext {
		IAssemblyResolver assemblyResolver;
		IResolver resolver;
		readonly OpCode[][] experimentalOpCodes = new OpCode[12][];

		/// <summary>
		/// Gets/sets the assembly resolver. This is never <c>null</c>.
		/// </summary>
		public IAssemblyResolver AssemblyResolver {
			get {
				if (assemblyResolver is null)
					Interlocked.CompareExchange(ref assemblyResolver, NullResolver.Instance, null);
				return assemblyResolver;
			}
			set => assemblyResolver = value;
		}

		/// <summary>
		/// Gets/sets the resolver. This is never <c>null</c>.
		/// </summary>
		public IResolver Resolver {
			get {
				if (resolver is null)
					Interlocked.CompareExchange(ref resolver, NullResolver.Instance, null);
				return resolver;
			}
			set => resolver = value;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ModuleContext() {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="assemblyResolver">Assembly resolver or <c>null</c></param>
		public ModuleContext(IAssemblyResolver assemblyResolver)
			: this(assemblyResolver, new Resolver(assemblyResolver)) {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="resolver">Type/method/field resolver or <c>null</c></param>
		public ModuleContext(IResolver resolver)
			: this(null, resolver) {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="assemblyResolver">Assembly resolver or <c>null</c></param>
		/// <param name="resolver">Type/method/field resolver or <c>null</c></param>
		public ModuleContext(IAssemblyResolver assemblyResolver, IResolver resolver) {
			this.assemblyResolver = assemblyResolver;
			this.resolver = resolver;
			if (resolver is null && assemblyResolver is not null)
				this.resolver = new Resolver(assemblyResolver);
		}

		/// <summary>
		/// Registers an experimental CIL opcode. It must be a 2-byte opcode
		/// where the first byte lies within the range <c>0xF0..0xFB</c>.
		/// </summary>
		public void RegisterExperimentalOpCode(OpCode opCode) {
			byte high = (byte)((ushort)opCode.Value >> 8);
			byte low = (byte)opCode.Value;
			OpCode[] array = experimentalOpCodes[high - 0xF0] ??= new OpCode[256];

			array[low] = opCode;
		}

		/// <summary>
		/// Clears an experimental CIL opcode.
		/// </summary>
		public void ClearExperimentalOpCode(byte high, byte low) {
			OpCode[] array = experimentalOpCodes[high - 0xF0];

			if (array != null)
				array[low] = null;
		}

		/// <summary>
		/// Attempts to get an experimental CIL opcode.
		/// </summary>
		public OpCode GetExperimentalOpCode(byte high, byte low) {
			return experimentalOpCodes[high - 0xF0]?[low];
		}
	}
}
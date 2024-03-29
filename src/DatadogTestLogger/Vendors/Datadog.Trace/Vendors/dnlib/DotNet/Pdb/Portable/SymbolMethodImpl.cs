//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

using System.Collections.Generic;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.Emit;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.Pdb.Symbols;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.Pdb.Portable {
	sealed class SymbolMethodImpl : SymbolMethod {
		readonly PortablePdbReader reader;
		readonly int token;
		readonly SymbolScope rootScope;
		readonly SymbolSequencePoint[] sequencePoints;
		readonly int kickoffMethod;

		public override int Token => token;
		public override SymbolScope RootScope => rootScope;
		public override IList<SymbolSequencePoint> SequencePoints => sequencePoints;
		public int KickoffMethod => kickoffMethod;

		public SymbolMethodImpl(PortablePdbReader reader, int token, SymbolScope rootScope, SymbolSequencePoint[] sequencePoints, int kickoffMethod) {
			this.reader = reader;
			this.token = token;
			this.rootScope = rootScope;
			this.sequencePoints = sequencePoints;
			this.kickoffMethod = kickoffMethod;
		}

		public override void GetCustomDebugInfos(MethodDef method, CilBody body, IList<PdbCustomDebugInfo> result) =>
			reader.GetCustomDebugInfos(this, method, body, result);
	}
}

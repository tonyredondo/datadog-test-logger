//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

using System.Diagnostics;
using System.Text;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.Pdb.Symbols {
	/// <summary>
	/// Sequence point
	/// </summary>
	[DebuggerDisplay("{GetDebuggerString(),nq}")]
	internal struct SymbolSequencePoint {
		/// <summary>
		/// IL offset
		/// </summary>
		public int Offset;

		/// <summary>
		/// Document
		/// </summary>
		public SymbolDocument Document;

		/// <summary>
		/// Start line
		/// </summary>
		public int Line;

		/// <summary>
		/// Start column
		/// </summary>
		public int Column;

		/// <summary>
		/// End line
		/// </summary>
		public int EndLine;

		/// <summary>
		/// End column
		/// </summary>
		public int EndColumn;

		readonly string GetDebuggerString() {
			var sb = new StringBuilder();
			if (Line == 0xFEEFEE && EndLine == 0xFEEFEE)
				sb.Append("<hidden>");
			else {
				sb.Append("(");
				sb.Append(Line);
				sb.Append(",");
				sb.Append(Column);
				sb.Append(")-(");
				sb.Append(EndLine);
				sb.Append(",");
				sb.Append(EndColumn);
				sb.Append(")");
			}
			sb.Append(": ");
			sb.Append(Document.URL);
			return sb.ToString();
		}
	}
}

//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

using System.Collections.Generic;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet {
	/// <summary>
	/// Returns types without getting stuck in an infinite loop
	/// </summary>
	readonly struct AllTypesHelper {
		/// <summary>
		/// Gets a list of all types and nested types
		/// </summary>
		/// <param name="types">A list of types</param>
		public static IEnumerable<TypeDef> Types(IEnumerable<TypeDef> types) {
			var visited = new Dictionary<TypeDef, bool>();
			var stack = new Stack<IEnumerator<TypeDef>>();
			if (types is not null)
				stack.Push(types.GetEnumerator());
			while (stack.Count > 0) {
				var enumerator = stack.Pop();
				while (enumerator.MoveNext()) {
					var type = enumerator.Current;
					if (visited.ContainsKey(type))
						continue;
					visited[type] = true;
					yield return type;
					if (type.NestedTypes.Count > 0) {
						stack.Push(enumerator);
						enumerator = type.NestedTypes.GetEnumerator();
					}
				}
			}
		}
	}
}

//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.dnlib.DotNet.Emit {
	/// <summary>
	/// A CIL method exception handler
	/// </summary>
	internal sealed class ExceptionHandler {
		/// <summary>
		/// First instruction of try block
		/// </summary>
		public Instruction TryStart;

		/// <summary>
		/// One instruction past the end of try block or <c>null</c> if it ends at the end
		/// of the method.
		/// </summary>
		public Instruction TryEnd;

		/// <summary>
		/// Start of filter handler or <c>null</c> if none. The end of filter handler is
		/// always <see cref="HandlerStart"/>.
		/// </summary>
		public Instruction FilterStart;

		/// <summary>
		/// First instruction of try handler block
		/// </summary>
		public Instruction HandlerStart;

		/// <summary>
		/// One instruction past the end of try handler block or <c>null</c> if it ends at the end
		/// of the method.
		/// </summary>
		public Instruction HandlerEnd;

		/// <summary>
		/// The catch type if <see cref="IsCatch"/> is <see langword="true" />
		/// </summary>
		public ITypeDefOrRef CatchType;

		/// <summary>
		/// Type of exception handler clause
		/// </summary>
		public ExceptionHandlerType HandlerType;

		/// <summary>
		/// Checks if it's a `catch` handler
		/// </summary>
		public bool IsCatch => ((uint)HandlerType & 7) == (uint)ExceptionHandlerType.Catch;

		/// <summary>
		/// Checks if it's a `filter` handler
		/// </summary>
		public bool IsFilter => (HandlerType & ExceptionHandlerType.Filter) != 0;

		/// <summary>
		/// Checks if it's a `finally` handler
		/// </summary>
		public bool IsFinally => (HandlerType & ExceptionHandlerType.Finally) != 0;

		/// <summary>
		/// Checks if it's a `fault` handler
		/// </summary>
		public bool IsFault => (HandlerType & ExceptionHandlerType.Fault) != 0;
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public ExceptionHandler() {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="handlerType">Exception clause type</param>
		public ExceptionHandler(ExceptionHandlerType handlerType) => HandlerType = handlerType;
	}
}

//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032

using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.MessagePack.Formatters;
using System;
using System.Reflection;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.MessagePack
{
    internal interface IFormatterResolver
    {
        IMessagePackFormatter<T> GetFormatter<T>();
    }

    internal static class FormatterResolverExtensions
    {
        public static IMessagePackFormatter<T> GetFormatterWithVerify<T>(this IFormatterResolver resolver)
        {
            IMessagePackFormatter<T> formatter;
            try
            {
                formatter = resolver.GetFormatter<T>();
            }
            catch (TypeInitializationException ex)
            {
#if NETSTANDARD || NETFRAMEWORK
                // The fact that we're using static constructors to initialize this is an internal detail.
                // Rethrow the inner exception if there is one.
                // Do it carefully so as to not stomp on the original callstack.
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex.InnerException ?? ex).Throw();
                throw new InvalidOperationException("Unreachable"); // keep the compiler happy
#else
                var data = ex.Data; // suppress warning about not using `ex`
                throw;
#endif
            }

            if (formatter == null)
            {
                throw new FormatterNotRegisteredException(typeof(T).FullName + " is not registered in this resolver. resolver:" + resolver.GetType().Name);
            }

            return formatter;
        }

#if !UNITY_WSA

        public static object GetFormatterDynamic(this IFormatterResolver resolver, Type type)
        {
            var methodInfo = typeof(IFormatterResolver).GetRuntimeMethod("GetFormatter", Type.EmptyTypes);

            var formatter = methodInfo.MakeGenericMethod(type).Invoke(resolver, null);
            return formatter;
        }

#endif
    }

    internal class FormatterNotRegisteredException : Exception
    {
        public FormatterNotRegisteredException(string message) : base(message)
        {
        }
    }
}

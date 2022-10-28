//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.MessagePack.Formatters;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.MessagePack.Internal;
using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.MessagePack.Resolvers
{
    internal sealed class NativeDateTimeResolver : IFormatterResolver
    {
        public static readonly IFormatterResolver Instance = new NativeDateTimeResolver();

        NativeDateTimeResolver()
        {

        }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                formatter = (IMessagePackFormatter<T>)NativeDateTimeResolverGetFormatterHelper.GetFormatter(typeof(T));
            }
        }
    }
}

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.MessagePack.Internal
{
    internal static class NativeDateTimeResolverGetFormatterHelper
    {
        internal static object GetFormatter(Type t)
        {
            if (t == typeof(DateTime))
            {
                return NativeDateTimeFormatter.Instance;
            }
            else if (t == typeof(DateTime?))
            {
                return new StaticNullableFormatter<DateTime>(NativeDateTimeFormatter.Instance);
            }
            else if (t == typeof(DateTime[]))
            {
                return NativeDateTimeArrayFormatter.Instance;
            }

            return null;
        }
    }
}
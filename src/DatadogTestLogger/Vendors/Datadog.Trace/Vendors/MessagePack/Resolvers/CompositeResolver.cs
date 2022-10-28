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
using System;
using System.Reflection;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.MessagePack.Resolvers
{
    internal sealed class CompositeResolver : IFormatterResolver
    {
        public static readonly CompositeResolver Instance = new CompositeResolver();

        static bool isFreezed = false;
        static IMessagePackFormatter[] formatters = new IMessagePackFormatter[0];
        static IFormatterResolver[] resolvers = new IFormatterResolver[0];

        CompositeResolver()
        {
        }

        public static void Register(params IFormatterResolver[] resolvers)
        {
            if (isFreezed)
            {
                throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
            }

            CompositeResolver.resolvers = resolvers;
        }

        public static void Register(params IMessagePackFormatter[] formatters)
        {
            if (isFreezed)
            {
                throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
            }

            CompositeResolver.formatters = formatters;
        }

        public static void Register(IMessagePackFormatter[] formatters, IFormatterResolver[] resolvers)
        {
            if (isFreezed)
            {
                throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
            }

            CompositeResolver.resolvers = resolvers;
            CompositeResolver.formatters = formatters;
        }

        public static void RegisterAndSetAsDefault(params IFormatterResolver[] resolvers)
        {
            Register(resolvers);
            MessagePack.MessagePackSerializer.SetDefaultResolver(CompositeResolver.Instance);
        }

        public static void RegisterAndSetAsDefault(params IMessagePackFormatter[] formatters)
        {
            Register(formatters);
            MessagePack.MessagePackSerializer.SetDefaultResolver(CompositeResolver.Instance);
        }

        public static void RegisterAndSetAsDefault(IMessagePackFormatter[] formatters, IFormatterResolver[] resolvers)
        {
            Register(formatters);
            Register(resolvers);
            MessagePack.MessagePackSerializer.SetDefaultResolver(CompositeResolver.Instance);
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
                isFreezed = true;

                foreach (var item in formatters)
                {
                    foreach (var implInterface in item.GetType().GetTypeInfo().ImplementedInterfaces)
                    {
                        var ti = implInterface.GetTypeInfo();
                        if (ti.IsGenericType && ti.GenericTypeArguments[0] == typeof(T))
                        {
                            formatter = (IMessagePackFormatter<T>)item;
                            return;
                        }
                    }
                }

                foreach (var item in resolvers)
                {
                    var f = item.GetFormatter<T>();
                    if (f != null)
                    {
                        formatter = f;
                        return;
                    }
                }
            }
        }
    }
}

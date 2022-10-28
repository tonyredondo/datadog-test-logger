//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
using System;

namespace Datadog.Trace.Vendors.Datadog.Trace.Vendors.StatsdClient
{
    internal class ThreadSafeRandom
    {
        private static readonly Random _global = new Random();

        [ThreadStatic]
        private static Random _local;

        private Random Local
        {
            get
            {
                if (_local == null)
                {
                    int seed;
                    lock (_global)
                    {
                        seed = _global.Next();
                    }

                    _local = new Random(seed);
                }

                return _local;
            }
        }

        public double NextDouble()
        {
            return Local.NextDouble();
        }
    }
}
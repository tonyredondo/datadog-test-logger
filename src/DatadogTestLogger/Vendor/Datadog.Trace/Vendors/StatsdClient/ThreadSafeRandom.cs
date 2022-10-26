//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
using System;

namespace Vendor.Datadog.Trace.Vendors.StatsdClient
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

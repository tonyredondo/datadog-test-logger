//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient.Utils
{
    internal abstract class AbstractPoolObject : IDisposable
    {
        private readonly IPool _pool;
        private bool _enqueue = false;

        public AbstractPoolObject(IPool pool)
        {
            _pool = pool;
        }

        ~AbstractPoolObject()
        {
            try
            {
                Dispose(false);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Reset()
        {
            _enqueue = false;
            DoReset();
        }

        protected abstract void DoReset();

        protected void Dispose(bool disposing)
        {
            if (!_enqueue)
            {
                _pool.Enqueue(this);
            }

            _enqueue = true;
        }
    }
}
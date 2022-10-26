//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
using System.Collections.Concurrent;
using System.Threading;

namespace Vendor.Datadog.Trace.Vendors.StatsdClient.Worker
{
    /// <summary>
    /// ConcurrentBoundedQueue is a ConcurrentQueue with a bounded number of items.
    /// Note: Value is not enqueued when the queue is full.
    /// </summary>
    internal class ConcurrentBoundedQueue<T>
    {
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();

        // Queue size. It is much faster than calling ConcurrentQueue<T>.Count
        private int _queueCurrentSize = 0;

        public ConcurrentBoundedQueue(int maxItemCount)
        {
            MaxItemCount = maxItemCount;
        }

        public int QueueCurrentSize => _queueCurrentSize;

        public int MaxItemCount { get; }

        public virtual bool TryEnqueue(T value)
        {
            if (_queueCurrentSize >= MaxItemCount)
            {
                value = default(T);
                return false;
            }

            _queue.Enqueue(value);
            Interlocked.Increment(ref _queueCurrentSize);
            return true;
        }

        public virtual bool TryDequeue(out T value)
        {
            if (_queue.TryDequeue(out value))
            {
                Interlocked.Decrement(ref _queueCurrentSize);
                return true;
            }

            return false;
        }
    }
}
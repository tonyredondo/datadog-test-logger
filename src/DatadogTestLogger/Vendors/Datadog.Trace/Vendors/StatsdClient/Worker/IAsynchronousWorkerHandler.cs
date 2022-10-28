//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient.Worker
{
    internal interface IAsynchronousWorkerHandler<T>
    {
        /// <summary>
        /// Called when a new value is ready to be handled by the worker.
        /// </summary>
        void OnNewValue(T v);

        /// <summary>
        /// Called when the worker is waiting for new value to handle.
        /// </summary>
        /// <returns>Return true to make the worker in a sleep state, false otherwise.</returns>
        bool OnIdle();

        /// <summary>
        /// Called when AsynchronousWorker request a flush operation.
        /// </summary>
        void Flush();
    }
}
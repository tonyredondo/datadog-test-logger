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

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.StatsdClient.Worker
{
    internal interface IManualResetEvent
    {
        bool Wait(TimeSpan duration);

        void Set();

        void Reset();
    }
}
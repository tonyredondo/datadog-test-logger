//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="DiagnosticObserver.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

#if !NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Datadog.Trace.Vendors.Datadog.Trace.AppSec;
using Datadog.Trace.Vendors.Datadog.Trace.Logging;

namespace Datadog.Trace.Vendors.Datadog.Trace.DiagnosticListeners
{
    internal abstract class DiagnosticObserver : IObserver<KeyValuePair<string, object>>
    {
        private static readonly IDatadogLogger Log = DatadogLogging.GetLoggerFor<DiagnosticObserver>();

        /// <summary>
        /// Gets the name of the <see cref="DiagnosticListener"/> that should be instrumented.
        /// </summary>
        /// <value>The name of the <see cref="DiagnosticListener"/> that should be instrumented.</value>
        protected abstract string ListenerName { get; }

        public virtual bool IsSubscriberEnabled()
        {
            return true;
        }

        public virtual IDisposable SubscribeIfMatch(DiagnosticListener diagnosticListener)
        {
            if (diagnosticListener.Name == ListenerName)
            {
                return diagnosticListener.Subscribe(this, IsEventEnabled);
            }

            return null;
        }

        void IObserver<KeyValuePair<string, object>>.OnCompleted()
        {
        }

        void IObserver<KeyValuePair<string, object>>.OnError(Exception error)
        {
        }

        void IObserver<KeyValuePair<string, object>>.OnNext(KeyValuePair<string, object> value)
        {
            try
            {
                OnNext(value.Key, value.Value);
            }
            catch (Exception ex) when (ex is not BlockException)
            {
                Log.Error(ex, "Event Exception: {EventName}", value.Key);

#if DEBUG
                // In debug mode we allow exceptions to be catch in the test suite
                throw;
#endif
            }
        }

        protected virtual bool IsEventEnabled(string eventName)
        {
            return true;
        }

        protected abstract void OnNext(string eventName, object arg);
    }
}
#endif
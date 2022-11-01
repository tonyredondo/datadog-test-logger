#if !NETFRAMEWORK
// ReSharper disable once CheckNamespace
namespace DatadogTestLogger.Vendors.Datadog.Trace.DiagnosticListeners;

internal class AspNetCoreDiagnosticObserver : DiagnosticObserver
{
    protected override string ListenerName { get; } = "AspNetCoreDiagnosticObserver";
    protected override void OnNext(string eventName, object arg)
    {
    }
}
#endif
using DatadogTestLogger.Vendors.Datadog.Trace.Configuration;
using DatadogTestLogger.Vendors.Datadog.Trace.Iast.Settings;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Iast
{
    internal class IastRequestContext
    {
        public void AddIastVulnerabilitiesToSpan(Datadog.Trace.Span span)
        {
        }

        public void AddIastDisabledFlagToSpan(Datadog.Trace.Span span)
        {
        }
    }

    internal class OverheadController
    {
        public static OverheadController Instance { get; } = new();

        public void ReleaseRequest()
        {
        }
    }

    internal class Iast
    {
        public static Iast Instance { get; } = new();

        public Settings.IastSettings Settings { get; } = new();
    }

    internal class IastModule
    {
        public static void OnSqlQuery(string? commandText, IntegrationId integrationId) {}
        public static Scope? OnHashingAlgorithm(string? commandText, IntegrationId integrationId) => null;
        public static Scope? OnCipherAlgorithm(string? commandText, IntegrationId integrationId) => null;
        public static Scope? OnCipherAlgorithm(Type? type, IntegrationId integrationId) => null;
        public static void OnCommandInjection(string fileName, string arguments, IEnumerable<string> argumentList, IntegrationId integrationId) {}
    }
}

namespace DatadogTestLogger.Vendors.Datadog.Trace.Iast.Settings
{

    internal class IastSettings
    {
        public bool Enabled { get; set; }
        
        public bool DeduplicationEnabled { get; set; }
        
        public bool WeakHashAlgorithms { get; set; }

        public string WeakCipherAlgorithms { get; set; } = string.Empty;
    }
}


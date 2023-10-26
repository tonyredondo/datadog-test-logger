using System.Collections.Concurrent;
using DatadogTestLogger.Vendors.Datadog.Trace.Util;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Newtonsoft.Json;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollector.InProcDataCollector;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.InProcDataCollector;

namespace DatadogCollector;

internal class TestCaseInProcDataCollection : InProcDataCollection
{
    private readonly ConcurrentDictionary<Guid, TestCaseMetadata> _testCaseMetadatas = new();

    public void Initialize(IDataCollectionSink dataCollectionSink)
    {
        _ = Clock.UtcNow;
    }

    public void TestSessionStart(TestSessionStartArgs testSessionStartArgs)
    {
    }

    public void TestCaseStart(TestCaseStartArgs testCaseStartArgs)
    {
        if (testCaseStartArgs?.TestCase?.Id is { } id)
        {
            var metadata = _testCaseMetadatas.GetOrAdd(id, _ => new TestCaseMetadata());
            metadata.Start = Clock.UtcNow;
        }
    }

    public void TestCaseEnd(TestCaseEndArgs testCaseEndArgs)
    {
        if (testCaseEndArgs?.DataCollectionContext?.TestCase is { } testCase && testCase.Id is { } id)
        {
            var metadata = _testCaseMetadatas.GetOrAdd(id, _ => new TestCaseMetadata());
            metadata.End = Clock.UtcNow;
        }
    }

    public void TestSessionEnd(TestSessionEndArgs testSessionEndArgs)
    {
        try
        {
            // Try to write the json file.
            using var sw = new StreamWriter($"testcase_metadata.json", false);
            var serializer = JsonSerializer.CreateDefault();
            serializer.Serialize(sw, _testCaseMetadatas);
        }
        catch
        {
            // .
        }
    }

    internal class TestCaseMetadata
    {
        [JsonProperty("start")]
        public DateTime Start { get; set; }
        [JsonProperty("end")]
        public DateTime End { get; set; }
    }
}
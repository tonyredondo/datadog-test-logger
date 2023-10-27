using System.Collections.Concurrent;
using DatadogTestLogger.Vendors.Datadog.Trace;
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
        _ = TraceClock.Instance.UtcNow.DateTime;
    }

    public void TestSessionStart(TestSessionStartArgs testSessionStartArgs)
    {
    }

    public void TestCaseStart(TestCaseStartArgs testCaseStartArgs)
    {
        var time = TraceClock.Instance.UtcNow.DateTime;
        if (testCaseStartArgs?.TestCase?.Id is { } id)
        {
            var metadata = _testCaseMetadatas.GetOrAdd(id, _ => new TestCaseMetadata());
            metadata.Start = time;
        }
    }

    public void TestCaseEnd(TestCaseEndArgs testCaseEndArgs)
    {
        var time = TraceClock.Instance.UtcNow.DateTime;
        if (testCaseEndArgs?.DataCollectionContext?.TestCase is { } testCase && testCase.Id is { } id)
        {
            var metadata = _testCaseMetadatas.GetOrAdd(id, _ => new TestCaseMetadata());
            metadata.End = time;
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
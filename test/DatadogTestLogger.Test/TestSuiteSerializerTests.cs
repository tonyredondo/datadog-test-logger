namespace DatadogTestLogger.Test;

public class TestSuiteSerializerTests
{
    public static IEnumerable<object[]> TryGetTypeAndMethodInfo_LogFileCases()
    {
        yield return new object[]
        {
            "Datadog.Trace.ClrProfiler.IntegrationTests",
            "HotChocolateSchemaV0Tests.SubmitsTracesWebsockets",
            "Datadog.Trace.ClrProfiler.IntegrationTests.HotChocolateSchemaV0Tests",
            "SubmitsTracesWebsockets",
        };

        yield return new object[]
        {
            "Datadog.Trace.ClrProfiler.IntegrationTests",
            "HotChocolateSchemaV0Tests.SubmitsTracesHttp",
            "Datadog.Trace.ClrProfiler.IntegrationTests.HotChocolateSchemaV0Tests",
            "SubmitsTracesHttp",
        };

        yield return new object[]
        {
            "Datadog.Trace.ClrProfiler.IntegrationTests",
            "GraphQL2SchemaV0Tests.SubmitsTraces",
            "Datadog.Trace.ClrProfiler.IntegrationTests.GraphQL2SchemaV0Tests",
            "SubmitsTraces",
        };

        yield return new object[]
        {
            "Datadog.Trace.ClrProfiler.IntegrationTests",
            "GraphQL3SchemaV0Tests.SubmitsTraces",
            "Datadog.Trace.ClrProfiler.IntegrationTests.GraphQL3SchemaV0Tests",
            "SubmitsTraces",
        };

        yield return new object[]
        {
            "Datadog.Trace.ClrProfiler.IntegrationTests",
            "GraphQL4SchemaV0Tests.SubmitsTraces",
            "Datadog.Trace.ClrProfiler.IntegrationTests.GraphQL4SchemaV0Tests",
            "SubmitsTraces",
        };

        yield return new object[]
        {
            "Datadog.Trace.ClrProfiler.IntegrationTests",
            "GraphQL7SchemaV0Tests.SubmitsTraces",
            "Datadog.Trace.ClrProfiler.IntegrationTests.GraphQL7SchemaV0Tests",
            "SubmitsTraces",
        };

        yield return new object[]
        {
            "Datadog.Trace.ClrProfiler.IntegrationTests",
            "GraphQL7SchemaV0Tests.SubmitsTracesWebsockets",
            "Datadog.Trace.ClrProfiler.IntegrationTests.GraphQL7SchemaV0Tests",
            "SubmitsTracesWebsockets",
        };

        yield return new object[]
        {
            "Datadog.Trace.ClrProfiler.IntegrationTests.AspNetCore",
            "AspNetCoreIpCollectionTests+AspNetCoreMvcMinimapApiCollectionTests.CollectsIpWhenEnabled",
            "Datadog.Trace.ClrProfiler.IntegrationTests.AspNetCore.AspNetCoreIpCollectionTests+AspNetCoreMvcMinimapApiCollectionTests",
            "CollectsIpWhenEnabled",
        };
    }

    public static IEnumerable<object[]> TryGetTypeAndMethodInfo_AdditionalCases()
    {
        yield return new object[]
        {
            typeof(ReflectionCases.SimpleTests).FullName!,
            "SimpleMethod",
            typeof(ReflectionCases.SimpleTests).FullName!,
            "SimpleMethod",
        };

        yield return new object[]
        {
            typeof(ReflectionCases.SimpleTests).FullName!,
            "SimpleTests.SimpleMethod",
            typeof(ReflectionCases.SimpleTests).FullName!,
            "SimpleMethod",
        };

        yield return new object[]
        {
            typeof(ReflectionCases.OuterTests).Namespace!,
            "OuterTests+InnerTests.InnerMethod",
            typeof(ReflectionCases.OuterTests.InnerTests).FullName!,
            "InnerMethod",
        };

        yield return new object[]
        {
            typeof(ReflectionCases.OuterTests).Namespace!,
            "OuterTests.InnerTests.InnerMethod",
            typeof(ReflectionCases.OuterTests.InnerTests).FullName!,
            "InnerMethod",
        };

        yield return new object[]
        {
            typeof(ReflectionCases.OuterTests).Namespace!,
            typeof(ReflectionCases.OuterTests.InnerTests).FullName!.Replace('+', '.') + ".InnerMethod",
            typeof(ReflectionCases.OuterTests.InnerTests).FullName!,
            "InnerMethod",
        };

        yield return new object[]
        {
            typeof(ReflectionCases.OuterTests).FullName!,
            "OuterTests+InnerTests.InnerMethod",
            typeof(ReflectionCases.OuterTests.InnerTests).FullName!,
            "InnerMethod",
        };

        yield return new object[]
        {
            typeof(ReflectionCases.OuterTests).FullName!,
            "OuterTests.InnerTests.InnerMethod",
            typeof(ReflectionCases.OuterTests.InnerTests).FullName!,
            "InnerMethod",
        };

        yield return new object[]
        {
            typeof(ReflectionCases.GenericTests<>).Namespace!,
            "GenericTests`1.GenericMethod",
            typeof(ReflectionCases.GenericTests<>).FullName!,
            "GenericMethod",
        };

        yield return new object[]
        {
            typeof(ReflectionCases.GenericTests<>).Namespace!,
            "GenericTests<T>.GenericMethod",
            typeof(ReflectionCases.GenericTests<>).FullName!,
            "GenericMethod",
        };

        yield return new object[]
        {
            typeof(ReflectionCases.DerivedTests).FullName!,
            "ProtectedBaseMethod",
            typeof(ReflectionCases.DerivedTests).FullName!,
            "ProtectedBaseMethod",
        };
    }

    [Theory]
    [MemberData(nameof(TryGetTypeAndMethodInfo_LogFileCases))]
    [MemberData(nameof(TryGetTypeAndMethodInfo_AdditionalCases))]
    public void TryGetTypeAndMethodInfo_ResolvesTypeAndMethod(string testSuiteName, string testName, string expectedTypeName, string expectedMethodName)
    {
        var assembly = typeof(ReflectionCases.SimpleTests).Assembly;
        var result = global::DatadogTestLogger.TestSuiteSerializer.TryGetTypeAndMethodInfo(assembly, testSuiteName, testName, out var suiteType, out var methodInfo);

        Assert.True(result);
        Assert.NotNull(suiteType);
        Assert.NotNull(methodInfo);
        Assert.Equal(expectedTypeName, suiteType!.FullName);
        Assert.Equal(expectedMethodName, methodInfo!.Name);
    }

    [Fact]
    public void TryGetTypeAndMethodInfo_ReturnsFalse_WhenNotFound()
    {
        var assembly = typeof(ReflectionCases.SimpleTests).Assembly;
        var result = global::DatadogTestLogger.TestSuiteSerializer.TryGetTypeAndMethodInfo(assembly, "Does.Not.Exist", "Nope", out var suiteType, out var methodInfo);

        Assert.False(result);
        Assert.Null(suiteType);
        Assert.Null(methodInfo);
    }
}
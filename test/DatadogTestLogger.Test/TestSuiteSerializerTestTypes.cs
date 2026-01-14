namespace Datadog.Trace.ClrProfiler.IntegrationTests
{
    internal class HotChocolateSchemaTestsBase
    {
        public void SubmitsTracesWebsockets()
        {
        }

        public void SubmitsTracesHttp()
        {
        }
    }

    internal class HotChocolateSchemaV0Tests : HotChocolateSchemaTestsBase
    {
    }

    internal class GraphQL2SchemaV0Tests
    {
        public void SubmitsTraces()
        {
        }
    }

    internal class GraphQL3SchemaV0Tests
    {
        public void SubmitsTraces()
        {
        }
    }

    internal class GraphQL4SchemaV0Tests
    {
        public void SubmitsTraces()
        {
        }
    }

    internal class GraphQL7SchemaV0Tests
    {
        public void SubmitsTraces()
        {
        }

        public void SubmitsTracesWebsockets()
        {
        }
    }
}

namespace Datadog.Trace.ClrProfiler.IntegrationTests.AspNetCore
{
    internal abstract class AspNetCoreIpCollectionTests
    {
        internal class AspNetCoreMvcMinimapApiCollectionTests
        {
            public void CollectsIpWhenEnabled()
            {
            }
        }
    }
}

namespace DatadogTestLogger.Test.ReflectionCases
{
    internal class SimpleTests
    {
        public void SimpleMethod()
        {
        }
    }

    internal class BaseTests
    {
        protected void ProtectedBaseMethod()
        {
        }
    }

    internal class DerivedTests : BaseTests
    {
    }

    internal class OuterTests
    {
        internal class InnerTests
        {
            public void InnerMethod()
            {
            }
        }
    }

    internal class GenericTests<T>
    {
        public void GenericMethod()
        {
        }
    }
}

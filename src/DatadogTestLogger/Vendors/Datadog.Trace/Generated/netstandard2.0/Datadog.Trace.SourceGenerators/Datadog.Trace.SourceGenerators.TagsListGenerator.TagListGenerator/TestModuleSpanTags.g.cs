//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if !NETFRAMEWORK && !NETCOREAPP3_1_OR_GREATER
// <auto-generated/>
#nullable enable

using DatadogTestLogger.Vendors.Datadog.Trace.Processors;
using DatadogTestLogger.Vendors.Datadog.Trace.Tagging;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Ci.Tagging
{
    partial class TestModuleSpanTags
    {
        // TypeBytes = System.Text.Encoding.UTF8.GetBytes("test.type");
        private static readonly byte[] TypeBytes = new byte[] { 116, 101, 115, 116, 46, 116, 121, 112, 101 };
        // ModuleBytes = System.Text.Encoding.UTF8.GetBytes("test.module");
        private static readonly byte[] ModuleBytes = new byte[] { 116, 101, 115, 116, 46, 109, 111, 100, 117, 108, 101 };
        // BundleBytes = System.Text.Encoding.UTF8.GetBytes("test.bundle");
        private static readonly byte[] BundleBytes = new byte[] { 116, 101, 115, 116, 46, 98, 117, 110, 100, 108, 101 };
        // FrameworkBytes = System.Text.Encoding.UTF8.GetBytes("test.framework");
        private static readonly byte[] FrameworkBytes = new byte[] { 116, 101, 115, 116, 46, 102, 114, 97, 109, 101, 119, 111, 114, 107 };
        // FrameworkVersionBytes = System.Text.Encoding.UTF8.GetBytes("test.framework_version");
        private static readonly byte[] FrameworkVersionBytes = new byte[] { 116, 101, 115, 116, 46, 102, 114, 97, 109, 101, 119, 111, 114, 107, 95, 118, 101, 114, 115, 105, 111, 110 };
        // RuntimeNameBytes = System.Text.Encoding.UTF8.GetBytes("runtime.name");
        private static readonly byte[] RuntimeNameBytes = new byte[] { 114, 117, 110, 116, 105, 109, 101, 46, 110, 97, 109, 101 };
        // RuntimeVersionBytes = System.Text.Encoding.UTF8.GetBytes("runtime.version");
        private static readonly byte[] RuntimeVersionBytes = new byte[] { 114, 117, 110, 116, 105, 109, 101, 46, 118, 101, 114, 115, 105, 111, 110 };
        // RuntimeArchitectureBytes = System.Text.Encoding.UTF8.GetBytes("runtime.architecture");
        private static readonly byte[] RuntimeArchitectureBytes = new byte[] { 114, 117, 110, 116, 105, 109, 101, 46, 97, 114, 99, 104, 105, 116, 101, 99, 116, 117, 114, 101 };
        // OSArchitectureBytes = System.Text.Encoding.UTF8.GetBytes("os.architecture");
        private static readonly byte[] OSArchitectureBytes = new byte[] { 111, 115, 46, 97, 114, 99, 104, 105, 116, 101, 99, 116, 117, 114, 101 };
        // OSPlatformBytes = System.Text.Encoding.UTF8.GetBytes("os.platform");
        private static readonly byte[] OSPlatformBytes = new byte[] { 111, 115, 46, 112, 108, 97, 116, 102, 111, 114, 109 };
        // OSVersionBytes = System.Text.Encoding.UTF8.GetBytes("os.version");
        private static readonly byte[] OSVersionBytes = new byte[] { 111, 115, 46, 118, 101, 114, 115, 105, 111, 110 };

        public override string? GetTag(string key)
        {
            return key switch
            {
                "test.type" => Type,
                "test.module" => Module,
                "test.bundle" => Bundle,
                "test.framework" => Framework,
                "test.framework_version" => FrameworkVersion,
                "runtime.name" => RuntimeName,
                "runtime.version" => RuntimeVersion,
                "runtime.architecture" => RuntimeArchitecture,
                "os.architecture" => OSArchitecture,
                "os.platform" => OSPlatform,
                "os.version" => OSVersion,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "test.type": 
                    Type = value;
                    break;
                case "test.module": 
                    Module = value;
                    break;
                case "test.framework": 
                    Framework = value;
                    break;
                case "test.framework_version": 
                    FrameworkVersion = value;
                    break;
                case "runtime.name": 
                    RuntimeName = value;
                    break;
                case "runtime.version": 
                    RuntimeVersion = value;
                    break;
                case "runtime.architecture": 
                    RuntimeArchitecture = value;
                    break;
                case "os.architecture": 
                    OSArchitecture = value;
                    break;
                case "os.platform": 
                    OSPlatform = value;
                    break;
                case "os.version": 
                    OSVersion = value;
                    break;
                case "test.bundle": 
                    Logger.Value.Warning("Attempted to set readonly tag {TagName} on {TagType}. Ignoring.", key, nameof(TestModuleSpanTags));
                    break;
                default: 
                    base.SetTag(key, value);
                    break;
            }
        }

        public override void EnumerateTags<TProcessor>(ref TProcessor processor)
        {
            if (Type is not null)
            {
                processor.Process(new TagItem<string>("test.type", Type, TypeBytes));
            }

            if (Module is not null)
            {
                processor.Process(new TagItem<string>("test.module", Module, ModuleBytes));
            }

            if (Bundle is not null)
            {
                processor.Process(new TagItem<string>("test.bundle", Bundle, BundleBytes));
            }

            if (Framework is not null)
            {
                processor.Process(new TagItem<string>("test.framework", Framework, FrameworkBytes));
            }

            if (FrameworkVersion is not null)
            {
                processor.Process(new TagItem<string>("test.framework_version", FrameworkVersion, FrameworkVersionBytes));
            }

            if (RuntimeName is not null)
            {
                processor.Process(new TagItem<string>("runtime.name", RuntimeName, RuntimeNameBytes));
            }

            if (RuntimeVersion is not null)
            {
                processor.Process(new TagItem<string>("runtime.version", RuntimeVersion, RuntimeVersionBytes));
            }

            if (RuntimeArchitecture is not null)
            {
                processor.Process(new TagItem<string>("runtime.architecture", RuntimeArchitecture, RuntimeArchitectureBytes));
            }

            if (OSArchitecture is not null)
            {
                processor.Process(new TagItem<string>("os.architecture", OSArchitecture, OSArchitectureBytes));
            }

            if (OSPlatform is not null)
            {
                processor.Process(new TagItem<string>("os.platform", OSPlatform, OSPlatformBytes));
            }

            if (OSVersion is not null)
            {
                processor.Process(new TagItem<string>("os.version", OSVersion, OSVersionBytes));
            }

            base.EnumerateTags(ref processor);
        }

        protected override void WriteAdditionalTags(System.Text.StringBuilder sb)
        {
            if (Type is not null)
            {
                sb.Append("test.type (tag):")
                  .Append(Type)
                  .Append(',');
            }

            if (Module is not null)
            {
                sb.Append("test.module (tag):")
                  .Append(Module)
                  .Append(',');
            }

            if (Bundle is not null)
            {
                sb.Append("test.bundle (tag):")
                  .Append(Bundle)
                  .Append(',');
            }

            if (Framework is not null)
            {
                sb.Append("test.framework (tag):")
                  .Append(Framework)
                  .Append(',');
            }

            if (FrameworkVersion is not null)
            {
                sb.Append("test.framework_version (tag):")
                  .Append(FrameworkVersion)
                  .Append(',');
            }

            if (RuntimeName is not null)
            {
                sb.Append("runtime.name (tag):")
                  .Append(RuntimeName)
                  .Append(',');
            }

            if (RuntimeVersion is not null)
            {
                sb.Append("runtime.version (tag):")
                  .Append(RuntimeVersion)
                  .Append(',');
            }

            if (RuntimeArchitecture is not null)
            {
                sb.Append("runtime.architecture (tag):")
                  .Append(RuntimeArchitecture)
                  .Append(',');
            }

            if (OSArchitecture is not null)
            {
                sb.Append("os.architecture (tag):")
                  .Append(OSArchitecture)
                  .Append(',');
            }

            if (OSPlatform is not null)
            {
                sb.Append("os.platform (tag):")
                  .Append(OSPlatform)
                  .Append(',');
            }

            if (OSVersion is not null)
            {
                sb.Append("os.version (tag):")
                  .Append(OSVersion)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
    }
}

#endif
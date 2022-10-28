//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#if NETFRAMEWORK
// <auto-generated/>
#nullable enable

using DatadogTestLogger.Vendors.Datadog.Trace.Processors;
using DatadogTestLogger.Vendors.Datadog.Trace.Tagging;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Iast
{
    partial class IastTags
    {
        // IastJsonBytes = System.Text.Encoding.UTF8.GetBytes("_dd.iast.json");
        private static readonly byte[] IastJsonBytes = new byte[] { 95, 100, 100, 46, 105, 97, 115, 116, 46, 106, 115, 111, 110 };
        // IastEnabledBytes = System.Text.Encoding.UTF8.GetBytes("_dd.iast.enabled");
        private static readonly byte[] IastEnabledBytes = new byte[] { 95, 100, 100, 46, 105, 97, 115, 116, 46, 101, 110, 97, 98, 108, 101, 100 };

        public override string? GetTag(string key)
        {
            return key switch
            {
                "_dd.iast.json" => IastJson,
                "_dd.iast.enabled" => IastEnabled,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "_dd.iast.json": 
                    IastJson = value;
                    break;
                case "_dd.iast.enabled": 
                    IastEnabled = value;
                    break;
                default: 
                    base.SetTag(key, value);
                    break;
            }
        }

        public override void EnumerateTags<TProcessor>(ref TProcessor processor)
        {
            if (IastJson is not null)
            {
                processor.Process(new TagItem<string>("_dd.iast.json", IastJson, IastJsonBytes));
            }

            if (IastEnabled is not null)
            {
                processor.Process(new TagItem<string>("_dd.iast.enabled", IastEnabled, IastEnabledBytes));
            }

            base.EnumerateTags(ref processor);
        }

        protected override void WriteAdditionalTags(System.Text.StringBuilder sb)
        {
            if (IastJson is not null)
            {
                sb.Append("_dd.iast.json (tag):")
                  .Append(IastJson)
                  .Append(',');
            }

            if (IastEnabled is not null)
            {
                sb.Append("_dd.iast.enabled (tag):")
                  .Append(IastEnabled)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
    }
}

#endif
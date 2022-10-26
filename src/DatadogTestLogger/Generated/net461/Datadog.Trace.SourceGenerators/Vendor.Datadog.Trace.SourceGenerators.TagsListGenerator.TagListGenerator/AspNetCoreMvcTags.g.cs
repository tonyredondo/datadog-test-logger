﻿// <auto-generated/>
#nullable enable

using Vendor.Datadog.Trace.Processors;
using Vendor.Datadog.Trace.Tagging;

namespace Vendor.Datadog.Trace.Tagging
{
    partial class AspNetCoreMvcTags
    {
        // AspNetCoreControllerBytes = System.Text.Encoding.UTF8.GetBytes("aspnet_core.controller");
        private static readonly byte[] AspNetCoreControllerBytes = new byte[] { 97, 115, 112, 110, 101, 116, 95, 99, 111, 114, 101, 46, 99, 111, 110, 116, 114, 111, 108, 108, 101, 114 };
        // AspNetCoreActionBytes = System.Text.Encoding.UTF8.GetBytes("aspnet_core.action");
        private static readonly byte[] AspNetCoreActionBytes = new byte[] { 97, 115, 112, 110, 101, 116, 95, 99, 111, 114, 101, 46, 97, 99, 116, 105, 111, 110 };
        // AspNetCoreAreaBytes = System.Text.Encoding.UTF8.GetBytes("aspnet_core.area");
        private static readonly byte[] AspNetCoreAreaBytes = new byte[] { 97, 115, 112, 110, 101, 116, 95, 99, 111, 114, 101, 46, 97, 114, 101, 97 };
        // AspNetCorePageBytes = System.Text.Encoding.UTF8.GetBytes("aspnet_core.page");
        private static readonly byte[] AspNetCorePageBytes = new byte[] { 97, 115, 112, 110, 101, 116, 95, 99, 111, 114, 101, 46, 112, 97, 103, 101 };

        public override string? GetTag(string key)
        {
            return key switch
            {
                "aspnet_core.controller" => AspNetCoreController,
                "aspnet_core.action" => AspNetCoreAction,
                "aspnet_core.area" => AspNetCoreArea,
                "aspnet_core.page" => AspNetCorePage,
                _ => base.GetTag(key),
            };
        }

        public override void SetTag(string key, string value)
        {
            switch(key)
            {
                case "aspnet_core.controller": 
                    AspNetCoreController = value;
                    break;
                case "aspnet_core.action": 
                    AspNetCoreAction = value;
                    break;
                case "aspnet_core.area": 
                    AspNetCoreArea = value;
                    break;
                case "aspnet_core.page": 
                    AspNetCorePage = value;
                    break;
                default: 
                    base.SetTag(key, value);
                    break;
            }
        }

        public override void EnumerateTags<TProcessor>(ref TProcessor processor)
        {
            if (AspNetCoreController is not null)
            {
                processor.Process(new TagItem<string>("aspnet_core.controller", AspNetCoreController, AspNetCoreControllerBytes));
            }

            if (AspNetCoreAction is not null)
            {
                processor.Process(new TagItem<string>("aspnet_core.action", AspNetCoreAction, AspNetCoreActionBytes));
            }

            if (AspNetCoreArea is not null)
            {
                processor.Process(new TagItem<string>("aspnet_core.area", AspNetCoreArea, AspNetCoreAreaBytes));
            }

            if (AspNetCorePage is not null)
            {
                processor.Process(new TagItem<string>("aspnet_core.page", AspNetCorePage, AspNetCorePageBytes));
            }

            base.EnumerateTags(ref processor);
        }

        protected override void WriteAdditionalTags(System.Text.StringBuilder sb)
        {
            if (AspNetCoreController is not null)
            {
                sb.Append("aspnet_core.controller (tag):")
                  .Append(AspNetCoreController)
                  .Append(',');
            }

            if (AspNetCoreAction is not null)
            {
                sb.Append("aspnet_core.action (tag):")
                  .Append(AspNetCoreAction)
                  .Append(',');
            }

            if (AspNetCoreArea is not null)
            {
                sb.Append("aspnet_core.area (tag):")
                  .Append(AspNetCoreArea)
                  .Append(',');
            }

            if (AspNetCorePage is not null)
            {
                sb.Append("aspnet_core.page (tag):")
                  .Append(AspNetCorePage)
                  .Append(',');
            }

            base.WriteAdditionalTags(sb);
        }
    }
}

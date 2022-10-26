// <copyright file="InstrumentationDefinitions.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vendor.Datadog.Trace.ClrProfiler
{
    internal static partial class InstrumentationDefinitions
    {
        private static string assemblyFullName = typeof(InstrumentationDefinitions).Assembly.FullName;

        internal static Payload GetAllDefinitions(InstrumentationCategory instrumentationFilter = InstrumentationCategory.Tracing)
        {
            return GetDefinitionsArray(instrumentationFilter);
        }

        internal static Payload GetDerivedDefinitions(InstrumentationCategory instrumentationFilter = InstrumentationCategory.Tracing)
        {
            return GetDerivedDefinitionsArray(instrumentationFilter);
        }

        internal static Payload GetInterfaceDefinitions(InstrumentationCategory instrumentationFilter = InstrumentationCategory.Tracing)
        {
            return GetInterfaceDefinitionsArray(instrumentationFilter);
        }

        internal static NativeCallTargetDefinition[] GetAllDefinitionsNative()
        {
            return InstrumentationsNatives.ToArray();
        }

        internal static NativeCallTargetDefinition[] GetAllDerivedDefinitionsNative()
        {
            return DerivedInstrumentationsNatives.ToArray();
        }

        internal static NativeCallTargetDefinition[] GetAllInterfaceDefinitionsNative()
        {
            return InterfaceInstrumentationsNatives.ToArray();
        }

        internal static TraceMethodPayload GetTraceAttributeDefinitions()
        {
            return new TraceMethodPayload
            {
                // Fixed Id for definitions payload (to avoid loading same integrations from multiple AppDomains)
                DefinitionsId = "9C6EB897BD4946D0BB492E062FB0AE67",
                AssemblyName = assemblyFullName,
                TypeName = typeof(Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.TraceAnnotations.TraceAnnotationsIntegration).FullName
            };
        }

        internal static TraceMethodPayload GetTraceMethodDefinitions()
        {
            return new TraceMethodPayload
            {
                // Fixed Id for definitions payload (to avoid loading same integrations from multiple AppDomains)
                DefinitionsId = "CDEF904668434E7693E99DBD91341808",
                AssemblyName = assemblyFullName,
                TypeName = typeof(Vendor.Datadog.Trace.ClrProfiler.AutoInstrumentation.TraceAnnotations.TraceAnnotationsIntegration).FullName
            };
        }

        internal static void Dispose()
        {
            foreach (var def in InstrumentationsNatives)
            {
                def.Dispose();
            }

            foreach (var def in DerivedInstrumentationsNatives)
            {
                def.Dispose();
            }

            foreach (var def in InterfaceInstrumentationsNatives)
            {
                def.Dispose();
            }
        }

        internal struct Payload
        {
            public string DefinitionsId { get; set; }

            public NativeCallTargetDefinition[] Definitions { get; set; }
        }

        internal struct TraceMethodPayload
        {
            public string DefinitionsId { get; set; }

            public string AssemblyName { get; set; }

            public string TypeName { get; set; }
        }
    }
}

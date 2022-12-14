//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="Capture.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Debugger.Configurations.Models
{
    internal struct Capture : IEquatable<Capture>
    {
        public int MaxReferenceDepth { get; set; }

        public int MaxCollectionSize { get; set; }

        public int MaxLength { get; set; }

        public int MaxFieldDepth { get; set; }

        public int MaxFieldCount { get; set; }

        public bool Equals(Capture other)
        {
            return MaxReferenceDepth == other.MaxReferenceDepth && MaxCollectionSize == other.MaxCollectionSize && MaxLength == other.MaxLength && MaxFieldDepth == other.MaxFieldDepth && MaxFieldCount == other.MaxFieldCount;
        }

        public override bool Equals(object obj)
        {
            return obj is Capture other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MaxReferenceDepth, MaxCollectionSize, MaxLength, MaxFieldDepth, MaxFieldCount);
        }
    }
}

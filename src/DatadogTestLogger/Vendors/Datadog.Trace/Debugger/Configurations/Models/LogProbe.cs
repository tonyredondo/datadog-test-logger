//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
// <copyright file="LogProbe.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using DatadogTestLogger.Vendors.Datadog.Trace.Debugger.Helpers;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Debugger.Configurations.Models
{
    internal class LogProbe : ProbeDefinition, IEquatable<LogProbe>
    {
        public bool CaptureSnapshot { get; set; }

        public Capture? Capture { get; set; }

        public Sampling? Sampling { get; set; }

        public string Template { get; set; }

        public SnapshotSegment[] Segments { get; set; }

        public SnapshotSegment When { get; set; }

        public bool Equals(LogProbe other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return base.Equals(other) && Equals(Capture, other.Capture) && Equals(Sampling, other.Sampling) && Template == other.Template && CaptureSnapshot == other.CaptureSnapshot && Segments.NullableSequentialEquals(other.Segments) && Equals(When, other.When);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((LogProbe)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                base.GetHashCode(),
                Capture,
                Sampling,
                Template,
                CaptureSnapshot,
                Segments);
        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#nullable enable
// <copyright file="LogarithmicMapping.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2022 Datadog, Inc.
// </copyright>

#pragma warning disable SA1600 // Elements should be documented

using System;
using DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Datadog.Sketches.Serialization;

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.Datadog.Sketches.Mappings;

/// <summary>
/// LogarithmicMapping is an IndexMapping that is memory-optimal,
/// that is to say that given a targeted relative accuracy, it
/// requires the least number of indices to cover a given range of values.
/// This is done by logarithmically mapping floating-point values to integers.
/// </summary>
internal class LogarithmicMapping : IIndexMapping, ISerializable
{
    private const double MinNormal = 2.2250738585072014E-308;
    private readonly double _multiplier;

    public LogarithmicMapping(double relativeAccuracy)
        : this(ComputeGamma(RequireValidRelativeAccuracy(relativeAccuracy), 1), 0)
    {
    }

    public LogarithmicMapping(double gamma, double indexOffset)
    {
        if (gamma <= 1)
        {
            throw new ArgumentOutOfRangeException(nameof(gamma), "Gamma must be greater than 1");
        }

        Gamma = gamma;
        IndexOffset = indexOffset;
        _multiplier = 1 / Math.Log(gamma);
        RelativeAccuracy = (gamma - 1) / (gamma + 1);

        unchecked
        {
            MinIndexableValue = Math.Max(
                Math.Exp(((int.MinValue - indexOffset) / _multiplier) + 1),
                MinNormal * (1 + RelativeAccuracy) / (1 - RelativeAccuracy));

            MaxIndexableValue = Math.Min(
                Math.Exp(((int.MaxValue - indexOffset) / _multiplier) - 1),
                double.MaxValue / (1 + RelativeAccuracy));
        }
    }

    /// <inheritdoc />
    public double RelativeAccuracy { get; }

    /// <inheritdoc />
    public double MinIndexableValue { get; }

    /// <inheritdoc />
    public double MaxIndexableValue { get; }

    internal double Gamma { get; }

    internal double IndexOffset { get; }

    /// <inheritdoc />
    public int GetIndex(double value)
    {
        var index = (Math.Log(value) * _multiplier) + IndexOffset;
        return index >= 0 ? (int)index : (int)index - 1;
    }

    /// <inheritdoc />
    public double GetValue(int index)
    {
        return GetLowerBound(index) * (1 + RelativeAccuracy);
    }

    /// <inheritdoc />
    public double GetLowerBound(int index)
    {
        return Math.Exp((index - IndexOffset) / _multiplier);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        var other = (LogarithmicMapping)obj;

        return IndexOffset.Equals(other.IndexOffset) && _multiplier.Equals(other._multiplier);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            return (IndexOffset.GetHashCode() * 397) ^ _multiplier.GetHashCode();
        }
    }

    /// <inheritdoc />
    int ISerializable.ComputeSerializedSize()
    {
        return Serializer.DoubleFieldSize(1, Gamma) + Serializer.DoubleFieldSize(2, IndexOffset) + Serializer.FieldSize(3, 0);
    }

    /// <inheritdoc />
    void ISerializable.Serialize(Serializer serializer)
    {
        serializer.WriteDouble(1, Gamma);
        serializer.WriteDouble(2, IndexOffset);
        serializer.WriteUnsignedInt32(3, 0); // Interpolation = None
    }

    /// <summary>
    /// Calculates the (minimal) base that needs to be used for the mapping to be relatively accurate
    /// with the provided relative accuracy.
    /// </summary>
    /// <param name="relativeAccuracy">The relative accuracy that we want the index mapping to guarantee</param>
    /// <param name="correctingFactor">A measure of how well the mapping approximates the logarithm</param>
    /// <returns>The base of the logarithm to use to guarantee the provided relative accuracy</returns>
    private static double ComputeGamma(double relativeAccuracy, double correctingFactor)
    {
        var exactLogGamma = (1 + relativeAccuracy) / (1 - relativeAccuracy);
        return Math.Pow(exactLogGamma, 1 / correctingFactor);
    }

    private static double RequireValidRelativeAccuracy(double relativeAccuracy)
    {
        if (relativeAccuracy <= 0 || relativeAccuracy >= 1)
        {
            throw new ArgumentOutOfRangeException(nameof(relativeAccuracy), "The relative accuracy must be between 0 and 1.");
        }

        return relativeAccuracy;
    }
}

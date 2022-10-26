//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#nullable enable
// <copyright file="DenseStore.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2022 Datadog, Inc.
// </copyright>

#pragma warning disable SA1600 // Elements should be documented

using System;
using System.Collections.Generic;
using Vendor.Datadog.Trace.Vendors.Datadog.Sketches.Serialization;

namespace Vendor.Datadog.Trace.Vendors.Datadog.Sketches.Stores;

/// <summary>
/// DenseStore is a dynamically growing contiguous (non-sparse) store.
/// The number of bins are bound only by the size of the slice that can be allocated.
/// </summary>
internal abstract class DenseStore : Store, ISerializable
{
    private const int DefaultArrayLengthGrowthIncrement = 64;
    private const double DefaultArrayLengthOverheadRatio = 0.1;

    private readonly int _arrayLengthGrowthIncrement;
    private readonly int _arrayLengthOverhead;

    /// <summary>
    /// Initializes a new instance of the <see cref="DenseStore"/> class.
    /// </summary>
    protected DenseStore()
        : this(DefaultArrayLengthGrowthIncrement)
    {
    }

    protected DenseStore(int arrayLengthGrowthIncrement)
        : this(arrayLengthGrowthIncrement, (int)(arrayLengthGrowthIncrement * DefaultArrayLengthOverheadRatio))
    {
    }

    protected DenseStore(int arrayLengthGrowthIncrement, int arrayLengthOverhead)
    {
        if (arrayLengthGrowthIncrement <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayLengthGrowthIncrement), "Value must be greater than 0");
        }

        if (arrayLengthOverhead < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayLengthOverhead), "Value must be greater or equal than 0");
        }

        _arrayLengthGrowthIncrement = arrayLengthGrowthIncrement;
        _arrayLengthOverhead = arrayLengthOverhead;
        Counts = Array.Empty<double>();
        Offset = 0;
        MinIndex = int.MaxValue;
        MaxIndex = int.MinValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DenseStore"/> class from another store.
    /// </summary>
    /// <param name="store">The store to copy the data from.</param>
    protected DenseStore(DenseStore store)
    {
        _arrayLengthGrowthIncrement = store._arrayLengthGrowthIncrement;
        _arrayLengthOverhead = store._arrayLengthOverhead;
        MinIndex = store.MinIndex;
        MaxIndex = store.MaxIndex;

        if (store.Counts.Length > 0 && !store.IsEmpty())
        {
            Counts = new double[store.MaxIndex - store.MinIndex + 1];
            Array.Copy(store.Counts, store.MinIndex - store.Offset, Counts, 0, Counts.Length);
            Offset = store.MinIndex;
        }
        else
        {
            Counts = Array.Empty<double>();
            // Should be zero anyway, but just in case
            Offset = store.Offset;
        }
    }

    /// <summary>
    /// Gets or sets the minimum bin index
    /// </summary>
    protected internal int MinIndex { get; set; }

    /// <summary>
    /// Gets or sets the maximum bin index
    /// </summary>
    protected internal int MaxIndex { get; set; }

    /// <summary>
    /// Gets or sets the offset to get the <see cref="Counts"/> index
    /// </summary>
    protected internal int Offset { get; set; }

    /// <summary>
    /// Gets the storage for the value of the bins
    /// </summary>
    protected internal double[] Counts { get; private set; }

    /// <inheritdoc />
    public override IEnumerator<Bin> GetEnumerator() => EnumerateAscending().GetEnumerator();

    /// <inheritdoc />
    public override void Add(int index)
    {
        var arrayIndex = Normalize(index);
        Counts[arrayIndex]++;
    }

    /// <inheritdoc />
    public override void Add(int index, double count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "The count cannot be negative");
        }

        if (count == 0)
        {
            return;
        }

        var arrayIndex = Normalize(index);
        Counts[arrayIndex] += count;
    }

    /// <inheritdoc />
    public override void Add(Bin bin)
    {
        if (bin.Count == 0)
        {
            return;
        }

        var arrayIndex = Normalize(bin.Index);
        Counts[arrayIndex] += bin.Count;
    }

    /// <inheritdoc />
    public override void Clear()
    {
        if (Counts != null)
        {
            Array.Clear(Counts, 0, Counts.Length);
        }

        MaxIndex = int.MinValue;
        MinIndex = int.MaxValue;
        Offset = 0;
    }

    /// <inheritdoc />
    public override bool IsEmpty() => MaxIndex < MinIndex;

    /// <inheritdoc />
    public override int GetMinIndex()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("No elements in the collection");
        }

        return MinIndex;
    }

    /// <inheritdoc />
    public override int GetMaxIndex()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("No elements in the collection");
        }

        return MaxIndex;
    }

    /// <inheritdoc />
    public override double GetTotalCount() => GetTotalCount(MinIndex, MaxIndex);

    /// <inheritdoc />
    public override IEnumerable<Bin> EnumerateAscending()
    {
        for (var i = MinIndex; i >= MinIndex && i <= MaxIndex; i++)
        {
            var count = Counts[i - Offset];

            if (count > 0)
            {
                yield return new Bin(i, count);
            }
        }
    }

    /// <inheritdoc />
    public override IEnumerable<Bin> EnumerateDescending()
    {
        for (var i = MaxIndex; i >= MinIndex && i <= MaxIndex; i--)
        {
            var count = Counts[i - Offset];

            if (count > 0)
            {
                yield return new Bin(i, count);
            }
        }
    }

    /// <inheritdoc />
    int ISerializable.ComputeSerializedSize()
    {
        if (IsEmpty())
        {
            return 0;
        }

        return Serializer.CompactDoubleArraySize(2, MaxIndex - MinIndex + 1)
            + Serializer.SignedIntFieldSize(3, MinIndex);
    }

    /// <inheritdoc />
    void ISerializable.Serialize(Serializer serializer)
    {
        if (IsEmpty())
        {
            return;
        }

        serializer.WriteCompactArray(2, Counts, MinIndex - Offset, MaxIndex - MinIndex + 1);
        serializer.WriteSignedInt32(3, MinIndex);
    }

    /// <summary>
    /// Computes the sum of the counters between two indexes.
    /// </summary>
    /// <param name="fromIndex">Start index</param>
    /// <param name="toIndex">End index</param>
    /// <returns>The sum of the counters</returns>
    protected double GetTotalCount(int fromIndex, int toIndex)
    {
        if (IsEmpty())
        {
            return 0;
        }

        var fromArrayIndex = Math.Max(fromIndex - Offset, 0);
        var toArrayIndex = Math.Min(toIndex - Offset, Counts.Length - 1);

        double totalCount = 0;

        for (var arrayIndex = fromArrayIndex; arrayIndex <= toArrayIndex; arrayIndex++)
        {
            totalCount += Counts[arrayIndex];
        }

        return totalCount;
    }

    /// <summary>
    /// Normalize the store, if necessary, so that the counter of the specified index can be updated.
    /// </summary>
    /// <param name="index">The index of the counter to be updated.</param>
    /// <returns>The <see cref="Counts"/> array index that matches the counter to be updated.</returns>
    protected abstract int Normalize(int index);

    /// <summary>
    /// Adjusts the <see cref="Counts"/>, the <see cref="Offset"/>, the <see cref="MinIndex"/> and the <see cref="MaxIndex"/>
    /// without resizing the <see cref="Counts"/> array, in order to try making it fit the specified range.
    /// </summary>
    /// <param name="newMinIndex">The minimum index to be stored.</param>
    /// <param name="newMaxIndex">The maximum index to be stored.</param>
    protected abstract void Adjust(int newMinIndex, int newMaxIndex);

    /// <summary>
    /// Resizes the storage to fit the given range.
    /// </summary>
    /// <param name="index">Start and end of the range</param>
    protected void ExtendRange(int index)
    {
        ExtendRange(index, index);
    }

    /// <summary>
    /// Resizes the storage to fit the given range.
    /// </summary>
    /// <param name="newMinIndex">Start of the range</param>
    /// <param name="newMaxIndex">End of the </param>
    protected void ExtendRange(int newMinIndex, int newMaxIndex)
    {
        newMinIndex = Math.Min(newMinIndex, MinIndex);
        newMaxIndex = Math.Max(newMaxIndex, MaxIndex);

        if (IsEmpty())
        {
            var initialLength = (int)GetNewLength(newMinIndex, newMaxIndex);

            if (Counts == null || initialLength >= Counts.Length)
            {
                Counts = new double[initialLength];
            }

            Offset = newMinIndex;
            MinIndex = newMinIndex;
            MaxIndex = newMaxIndex;
            Adjust(newMinIndex, newMaxIndex);
        }
        else if (newMinIndex >= Offset && newMaxIndex < (long)Offset + Counts.Length)
        {
            MinIndex = newMinIndex;
            MaxIndex = newMaxIndex;
        }
        else
        {
            // To avoid shifting too often when nearing the capacity of the array,
            // we may grow it before we actually reach the capacity.
            var newLength = (int)GetNewLength(newMinIndex, newMaxIndex);

            if (newLength > Counts.Length)
            {
                var newCounts = new double[newLength];
                Array.Copy(Counts, newCounts, Counts.Length);
                Counts = newCounts;
            }

            Adjust(newMinIndex, newMaxIndex);
        }
    }

    protected void CenterCounts(int newMinIndex, int newMaxIndex)
    {
        var middleIndex = newMinIndex + ((newMaxIndex - newMinIndex + 1) / 2);
        ShiftCounts(Offset + (Counts.Length / 2) - middleIndex);

        MinIndex = newMinIndex;
        MaxIndex = newMaxIndex;
    }

    protected virtual long GetNewLength(int newMinIndex, int newMaxIndex)
    {
        var desiredLength = (long)newMaxIndex - newMinIndex + 1;

        return (((desiredLength + _arrayLengthOverhead - 1) / _arrayLengthGrowthIncrement) + 1) * _arrayLengthGrowthIncrement;
    }

    protected void ResetCounts()
    {
        ResetCounts(MinIndex, MaxIndex);
    }

    protected void ResetCounts(int fromIndex, int toIndex)
    {
        Helpers.ArrayFill(Counts, fromIndex - Offset, toIndex - Offset + 1, 0);
    }

    protected void ShiftCounts(int shift)
    {
        var minArrayIndex = MinIndex - Offset;
        var maxArrayIndex = MaxIndex - Offset;

        Array.Copy(Counts, minArrayIndex, Counts, minArrayIndex + shift, maxArrayIndex - minArrayIndex + 1);

        if (shift > 0)
        {
            Helpers.ArrayFill(Counts, minArrayIndex, minArrayIndex + shift, 0);
        }
        else
        {
            Helpers.ArrayFill(Counts, maxArrayIndex + 1 + shift, maxArrayIndex + 1, 0);
        }

        Offset -= shift;
    }
}

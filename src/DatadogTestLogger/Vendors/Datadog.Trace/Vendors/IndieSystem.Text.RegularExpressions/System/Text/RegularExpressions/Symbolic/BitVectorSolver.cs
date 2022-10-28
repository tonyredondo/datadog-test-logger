//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8604, CS8618, CS8620, CS8714, CS8762, CS8765, CS8766, CS8767, CS8768, CS8769, CS8612, CS8629, CS8774
#nullable enable
#if NETCOREAPP3_1_OR_GREATER
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Datadog.Trace.Vendors.Datadog.Trace.Vendors.IndieSystem.Text.RegularExpressions.Symbolic
{
    /// <summary>Provides an <see cref="ISolver{BitVector}"/> over arbitrary-length bit vectors.</summary>
    internal sealed class BitVectorSolver : ISolver<BitVector>
    {
        private readonly BDD[] _minterms;
        internal readonly MintermClassifier _classifier;
        private readonly BitVector[] _mintermVectors;

        public BitVectorSolver(BDD[] minterms, CharSetSolver solver)
        {
            _minterms = minterms;

            _classifier = new MintermClassifier(minterms, solver);

            var singleBitVectors = new BitVector[minterms.Length];
            for (int i = 0; i < singleBitVectors.Length; i++)
            {
                singleBitVectors[i] = BitVector.CreateSingleBit(minterms.Length, i);
            }
            _mintermVectors = singleBitVectors;

            Empty = BitVector.CreateFalse(minterms.Length);
            Full = BitVector.CreateTrue(minterms.Length);
        }

        public BitVector Empty { get; }
        public BitVector Full { get; }

        public bool IsFull(BitVector set) => set.Equals(Full);

        public bool IsEmpty(BitVector set) => set.Equals(Empty);

        public BitVector And(BitVector set1, BitVector set2) => BitVector.And(set1, set2);

        public BitVector Not(BitVector set) => BitVector.Not(set);

        public BitVector Or(ReadOnlySpan<BitVector> sets) => BitVector.Or(sets);

        public BitVector Or(BitVector set1, BitVector set2) => BitVector.Or(set1, set2);

        /// <summary>
        /// Assumes that set is a union of some minterms (or empty).
        /// If null then null is returned.
        /// </summary>
        public BitVector ConvertFromBDD(BDD set, CharSetSolver solver)
        {
            BDD[] partition = _minterms;

            BitVector result = Empty;
            for (int i = 0; i < partition.Length; i++)
            {
                if (!solver.IsEmpty(solver.And(partition[i], set)))
                {
                    result = BitVector.Or(result, _mintermVectors[i]);
                }
            }

            return result;
        }

        public BitVector[] GetMinterms() => _mintermVectors;

#if DEBUG
        /// <summary>Pretty print the bitvector bv as the character set it represents.</summary>
        public string PrettyPrint(BitVector bv, CharSetSolver solver) => solver.PrettyPrint(ConvertToBDD(bv, solver));

        public BDD ConvertToBDD(BitVector set, CharSetSolver solver)
        {
            BDD[] partition = _minterms;

            // the result will be the union of all minterms in the set
            BDD result = solver.Empty;
            if (!set.Equals(Empty))
            {
                for (int i = 0; i < partition.Length; i++)
                {
                    // include the i'th minterm in the union if the i'th bit is set
                    if (set[i])
                    {
                        result = solver.Or(result, partition[i]);
                    }
                }
            }

            return result;
        }
#endif
    }
}

#endif
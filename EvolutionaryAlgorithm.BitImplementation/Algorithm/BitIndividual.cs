using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;
using static System.Linq.Enumerable;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm
{
    public class BitIndividual : IBitIndividual
    {
        private int _ones = -1;
        public double Fitness { get; set; }
        public BitArray Genes { get; set; }

        public void CloneGenesTo(IIndividual<BitArray, bool> other)
        {
            other.Genes = new BitArray(Genes);
            other.Fitness = Fitness;
        }

        public void Reset()
        {
            _ones = -1;
        }

        public int Size => Genes.Count;

        public int Ones
        {
            get => _ones == -1 ? _ones = this.Count(e => e) : _ones;
            set => _ones = value;
        }

        public int Zeros => Size - Ones;

        public BitIndividual(int size, bool defaultValue) => Genes = new BitArray(size, defaultValue);

        public BitIndividual(IBitIndividual individual)
        {
            Fitness = individual.Fitness;
            Genes = (BitArray) individual.Genes.Clone();
        }


        public BitIndividual(BitArray bitArray) => Genes = bitArray;

        public BitIndividual(bool[] genes) : this(new BitArray(genes))
        {
        }

        public BitIndividual(int size, Func<bool> generator)
            : this(Range(0, size).Select(_ => generator.Invoke()).ToArray())
        {
        }

        public bool Flip(int i) => Genes[i] = !Genes[i];

        public object Clone() => new BitIndividual(this);

        public IEnumerator<bool> GetEnumerator() => (IEnumerator<bool>) Genes.Cast<bool>().GetEnumerator();

        public int CompareTo(IIndividual<BitArray, bool> other) => Math.Sign(Fitness - other.Fitness);

        public override string ToString() =>
            Fitness + " -> " + string.Join("", Genes.Cast<bool>().Select(e => e ? "1" : "0"));

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
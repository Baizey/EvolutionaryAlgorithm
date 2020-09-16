using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;
using static System.Linq.Enumerable;

namespace EvolutionaryAlgorithm.Core.BitAlgorithm
{
    public class BitIndividual : IBitIndividual
    {
        public double Fitness { get; set; }
        public BitArray Genes { get; set; }
        public int Size => Genes.Count;

        public BitIndividual(int size, bool defaultValue) => Genes = new BitArray(size, defaultValue);
        public BitIndividual(BitArray genes) => Genes = genes;

        public BitIndividual(bool[] genes) : this(new BitArray(genes))
        {
        }

        public BitIndividual(int size, Func<bool> generator)
            : this(Range(0, size).Select(_ => generator.Invoke()).ToArray())
        {
        }

        public bool Flip(int i) => Genes[i] = !Genes[i];

        public object Clone() => new BitIndividual((BitArray) Genes.Clone());

        public IEnumerator<bool> GetEnumerator() => (IEnumerator<bool>) Genes.Cast<bool>().GetEnumerator();

        public int CompareTo(IIndividual<BitArray, bool> other) => Math.Sign(Fitness - other.Fitness);

        public override string ToString() =>
            Fitness + " -> " + string.Join("", Genes.Cast<bool>().Select(e => e ? "1" : "0"));

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using static System.Linq.Enumerable;

// ReSharper disable all MemberCanBePrivate.Global
namespace EvolutionaryAlgorithm.Core.Individual
{
    public class BooleanIndividual : IBooleanIndividual
    {
        public double? Fitness { get; set; }
        public BitArray Genes { get; set; }
        public int Count => Genes.Count;

        public BooleanIndividual(BitArray genes) => Genes = genes;

        public BooleanIndividual(bool[] genes) : this(new BitArray(genes))
        {
        }

        public BooleanIndividual(int size, Func<bool> generator)
            : this(Range(0, size).Select(_ => generator.Invoke()).ToArray())
        {
        }

        public bool Flip(int i) => Genes[i] = !Genes[i];

        public IEnumerator GetEnumerator() => Genes.GetEnumerator();

        public object Clone() => new BooleanIndividual((BitArray) Genes.Clone());

        public int CompareTo(IIndividual<BitArray> other)
        {
            Debug.Assert(Fitness != null, nameof(Fitness) + " != null");
            Debug.Assert(other.Fitness != null, "other.Fitness != null");
            return Math.Sign((decimal) (Fitness - other.Fitness));
        }
    }
}
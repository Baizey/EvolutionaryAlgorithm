using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Linq.Enumerable;

namespace EvolutionaryAlgorithm.Core.Individual
{
    public class Individual<T> : IObjectIndividual<T> where T : ICloneable
    {
        public double? Fitness { get; set; }
        public List<T> Genes { get; set; }

        public int Count => Genes.Count;

        public Individual(List<T> genes) => Genes = genes;

        public Individual(int size, Func<T> generator)
            : this(Range(0, size).Select(_ => generator.Invoke()).ToList())
        {
        }

        public object Clone() => new Individual<T>(Genes.Select(gene => (T) gene.Clone()).ToList());


        public int CompareTo(IIndividual<List<T>> other)
        {
            Debug.Assert(Fitness != null, nameof(Fitness) + " != null");
            Debug.Assert(other.Fitness != null, "other.Fitness != null");
            return Math.Sign((decimal) (Fitness - other.Fitness));
        }

        public override string ToString() => Genes.ToString();
        public IEnumerator GetEnumerator() => Genes.GetEnumerator();
    }
}
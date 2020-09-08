using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Gene;

namespace GeneticAlgorithm.Individual
{
    public abstract class Individual<T> : ComplexIndividual<List<Gene<T>>>
    {
        public override string ToString() => Genes.ToString();

        protected Individual(List<Gene<T>> value) : base(value)
        {
        }

        protected Individual(Func<List<Gene<T>>> generator) : base(generator)
        {
        }

        protected Individual(int size, Func<Gene<T>> generator) : base((List<Gene<T>>) null) =>
            Genes = Enumerable.Range(0, size).Select(_ => generator.Invoke()).ToList();
    }
}
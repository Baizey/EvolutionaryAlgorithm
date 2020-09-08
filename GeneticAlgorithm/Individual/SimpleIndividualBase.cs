using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Gene;

namespace GeneticAlgorithm.Individual
{
    public abstract class SimpleIndividualBase<T> : ComplexIndividualBase<List<GeneBase<T>>>
    {
        protected SimpleIndividualBase(List<GeneBase<T>> value) : base(value)
        {
        }

        protected SimpleIndividualBase(Func<List<GeneBase<T>>> generator) : base(generator)
        {
        }

        protected SimpleIndividualBase(int size, Func<GeneBase<T>> generator) : base((List<GeneBase<T>>) null) =>
            Genes = Enumerable.Range(0, size).Select(_ => generator.Invoke()).ToList();


        public GeneBase<T> this[int i]
        {
            get => Genes[i];
            set => Genes[i] = value;
        }

        public override string ToString() => Genes.ToString();
    }
}
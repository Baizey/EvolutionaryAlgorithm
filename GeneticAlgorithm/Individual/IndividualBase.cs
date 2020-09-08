using System;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Individual
{
    public abstract class IndividualBase<T> : IIndividual
    {
        public abstract int Count { get; }
        public double? Fitness { get; set; }
        public T Genes;

        public IndividualBase(T genes) => Genes = genes;

        public override string ToString() => Genes.ToString();

        public abstract IIndividual Clone();
    }
}
using System;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Individual
{
    public abstract class ComplexIndividual<T> : IIndividual
    {
        public T Genes;

        public ComplexIndividual(T value) => Genes = value;

        public ComplexIndividual(Func<T> generator) => Genes = generator.Invoke();

        public override string ToString() => Genes.ToString();
    }
}
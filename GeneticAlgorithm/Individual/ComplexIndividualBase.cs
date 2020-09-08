using System;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Individual
{
    public abstract class ComplexIndividualBase<T> : IIndividual
    {
        public T Genes;

        public ComplexIndividualBase(T value) => Genes = value;

        public ComplexIndividualBase(Func<T> generator) => Genes = generator.Invoke();

        public override string ToString() => Genes.ToString();
    }
}
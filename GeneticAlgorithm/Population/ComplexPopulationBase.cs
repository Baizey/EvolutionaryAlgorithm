using System;
using System.Collections.Generic;
using GeneticAlgorithm.Individual;

namespace GeneticAlgorithm.Population
{
    public abstract class ComplexPopulationBase<T> : PopulationBase<ComplexIndividualBase<T>>
    {
        public ComplexPopulationBase(List<ComplexIndividualBase<T>> value) : base(value)
        {
        }

        public ComplexPopulationBase(Func<List<ComplexIndividualBase<T>>> generator) : base(generator)
        {
        }

        public ComplexPopulationBase(int size, Func<ComplexIndividualBase<T>> generator) : base(size, generator)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using GeneticAlgorithm.Individual;

namespace GeneticAlgorithm.Population
{
    public abstract class SimplePopulationBase<T> : PopulationBase<SimpleIndividualBase<T>>
    {
        protected SimplePopulationBase(List<SimpleIndividualBase<T>> value) : base(value)
        {
        }

        protected SimplePopulationBase(Func<List<SimpleIndividualBase<T>>> generator) : base(generator)
        {
        }

        protected SimplePopulationBase(int size, Func<SimpleIndividualBase<T>> generator) : base(size, generator)
        {
        }
    }
}
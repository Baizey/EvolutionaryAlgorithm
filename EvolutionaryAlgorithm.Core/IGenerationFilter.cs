using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Core
{
    public interface IGenerationFilter<T> where T : ICloneable
    {
        IPopulation<T> Filter(IPopulation<T> population, List<IIndividual<T>> newcomers);
    }
}
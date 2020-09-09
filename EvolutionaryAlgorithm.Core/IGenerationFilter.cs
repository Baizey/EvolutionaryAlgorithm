using System;

namespace EvolutionaryAlgorithm.Core
{
    public interface IGenerationFilter<T> where T : ICloneable
    {
        IPopulation<T> Filter(IPopulation<T> population);
    }
}
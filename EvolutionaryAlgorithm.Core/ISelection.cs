using System;

namespace EvolutionaryAlgorithm.Core
{
    public interface ISelection<T> where T : ICloneable
    {
        IPopulation<T> Select(IPopulation<T> population);
    }
}
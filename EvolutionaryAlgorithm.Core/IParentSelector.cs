using System;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Core
{
    public interface IParentSelector<T> where T : ICloneable
    {
        public IIndividual<T> Select(IPopulation<T> population);
    }
}
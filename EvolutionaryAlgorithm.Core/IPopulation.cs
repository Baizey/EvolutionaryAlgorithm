using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Core
{
    public interface IPopulation<T> : IEnumerable<IIndividual<T>>, ICloneable
        where T : ICloneable
    {
        public List<IIndividual<T>> Individuals { get; set; }
        public int Generation { get; set; }

        public IIndividual<T> this[int i]
        {
            get => Individuals[i];
            set => Individuals[i] = value;
        }
    }
}
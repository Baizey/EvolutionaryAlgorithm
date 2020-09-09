using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core
{
    public interface IPopulation<T> : IEnumerable<T>, ICloneable
        where T : ICloneable
    {
        public List<T> Individuals { get; set; }
        public int Generation { get; set; }

        public T this[int i]
        {
            get => Individuals[i];
            set => Individuals[i] = value;
        }
    }
}
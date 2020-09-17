using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IPopulation<TIndividual, TGeneStructure, TGene> :
        ICloneable,
        IEnumerable<TIndividual>
        where TGeneStructure : ICloneable
    {
        public int Count { get; }
        public List<TIndividual> Individuals { get; set; }
        public TIndividual Best { get; }

        public TIndividual this[int i]
        {
            get => Individuals[i];
            set => Individuals[i] = value;
        }
    }
}
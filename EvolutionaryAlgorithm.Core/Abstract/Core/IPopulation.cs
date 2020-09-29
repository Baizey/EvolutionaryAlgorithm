using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;

namespace EvolutionaryAlgorithm.Core.Abstract.Core
{
    public interface IPopulation<TIndividual, TGeneStructure, TGene> :
        IEvolutionary<TIndividual, TGeneStructure, TGene>,
        ICloneable,
        IEnumerable<TIndividual>
        where TGeneStructure : ICloneable 
        where TIndividual : IIndividual<TGeneStructure, TGene>
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
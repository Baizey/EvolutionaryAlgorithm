using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;

namespace EvolutionaryAlgorithm.Core.Abstract.MutationPhase.Helpers
{
    public interface IMultiParentSelector<TIndividual, TGeneStructure, TGene> 
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<TIndividual> Select(IPopulation<TIndividual, TGeneStructure, TGene> population);
    }
}
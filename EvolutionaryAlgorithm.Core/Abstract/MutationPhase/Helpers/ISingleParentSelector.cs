using System;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;

namespace EvolutionaryAlgorithm.Core.Abstract.MutationPhase.Helpers
{
    public interface ISingleParentSelector<TIndividual, TGeneStructure, TGene> 
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public TIndividual Select(int index, IPopulation<TIndividual, TGeneStructure, TGene> population);
    }
}
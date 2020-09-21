using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IParentSelector<TIndividual, TGeneStructure, TGene> 
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public TIndividual Select(IPopulation<TIndividual, TGeneStructure, TGene> population);
    }
}
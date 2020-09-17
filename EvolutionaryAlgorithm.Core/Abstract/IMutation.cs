using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IMutation<TIndividual, TGeneStructure, TGene> 
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        TIndividual Mutate(TIndividual child, TIndividual parent);
    }
}
using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IMutation<TIndividual, TGeneStructure, TGene>
        : IInitialization<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        void Mutate(TIndividual child);
    }
}
using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IMutation<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        void Mutate(int index, TIndividual child);
    }
}
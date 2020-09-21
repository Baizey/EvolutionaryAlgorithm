using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface ITermination<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public bool ShouldTerminate();
    }
}
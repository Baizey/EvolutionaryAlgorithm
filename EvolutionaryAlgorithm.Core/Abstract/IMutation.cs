using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IMutation<TGeneStructure, TGene> where TGeneStructure : ICloneable
    {
        public IEvolutionaryAlgorithm<TGeneStructure, TGene> Algorithm { get; set; }
        IIndividual<TGeneStructure, TGene> Mutate(IIndividual<TGeneStructure, TGene> child,
            IIndividual<TGeneStructure, TGene> parent);
    }
}
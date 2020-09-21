using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        public void Initialize();
        public void Update();
    }
}
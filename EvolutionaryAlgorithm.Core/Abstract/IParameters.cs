using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IParameters<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public int GeneCount { get; set; }
        public int Mu { get; set; }
        public int Lambda { get; set; }
    }
}
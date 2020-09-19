using System;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IParameters<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public int Mu { get; set; }
        public int Lambda { get; set; }
        public double MutationFactor { get; set; }
        EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public void UpdateParameters();
    }
}
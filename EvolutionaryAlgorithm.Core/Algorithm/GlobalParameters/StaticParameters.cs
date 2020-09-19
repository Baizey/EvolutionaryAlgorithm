using System;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm.GlobalParameters;

namespace EvolutionaryAlgorithm.Core.Algorithm.GlobalParameters
{
    public class StaticParameters<TIndividual, TGeneStructure, TGene>
        : IParameters<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public int Mu { get; set; }
        public int Lambda { get; set; }
        public double MutationFactor { get; set; }
        public EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public void UpdateParameters()
        {
        }
    }
}
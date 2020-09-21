using System;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Parameters
{
    public class StaticParameters<TIndividual, TGeneStructure, TGene>
        : IParameters<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public void Initialize()
        {
        }
        public void Update()
        {
        }

        public int GeneCount { get; set; }
        public int Mu { get; set; }
        public int Lambda { get; set; }
    }
}
using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IFitness<TGeneStructure, TGene> where TGeneStructure : ICloneable
    {
        public IEvolutionaryAlgorithm<TGeneStructure, TGene> Algorithm { get; set; }
        double Evaluate(IIndividual<TGeneStructure, TGene> individual);
    }
}
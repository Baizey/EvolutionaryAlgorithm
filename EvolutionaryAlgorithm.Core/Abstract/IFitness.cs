using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IFitness<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        double Evaluate(TIndividual individual);
    }
}
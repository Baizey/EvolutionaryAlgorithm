using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IFitness<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        double Evaluate(TIndividual individual);
    }
}
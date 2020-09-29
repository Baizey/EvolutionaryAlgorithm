using System;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;

namespace EvolutionaryAlgorithm.Core.Abstract.Core
{
    public interface IFitness<TIndividual, TGeneStructure, TGene> : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        double Evaluate(TIndividual individual);
    }
}
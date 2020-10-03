using System;
using EvolutionaryAlgorithm.Core.Infrastructure;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.Fitness
{
    public interface IFitness<TIndividual, TGeneStructure, TGene> 
        : IInitializes, IKeepsReference<TIndividual, TGeneStructure, TGene> 
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        double Evaluate(TIndividual individual);
    }
}
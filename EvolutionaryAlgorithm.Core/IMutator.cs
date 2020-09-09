using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Individual;
using EvolutionaryAlgorithm.Template.Algorithm;

namespace EvolutionaryAlgorithm.Core
{
    public interface IMutator<T> where T : ICloneable
    {
        int NewIndividuals { get; set; }
        IParentSelector<T> InitialSelector { get; set; }
        List<MutationStep<T>> MutationSteps { get; set; }
        IMutator<T> Then(IMutation<T> mutation, IParentSelector<T> parentSelector = null);
        List<IIndividual<T>> Create(IPopulation<T> population, IFitness<T> fitness);
    }
}
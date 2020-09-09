using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Template.Algorithm
{
    public interface IMutator<T> where T : ICloneable
    {
        int Λ { get; set; }
        IParentSelector<T> InitialSelector { get; set; }
        List<MutationStep<T>> MutationSteps { get; set; }
        Mutator<T> Then(IMutation<T> mutation, IParentSelector<T> parentSelector = null);
        List<IIndividual<T>> Create(IPopulation<T> population);
    }
}
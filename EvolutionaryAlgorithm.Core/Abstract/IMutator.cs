using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IMutator<TDataStructure, TGene> where TDataStructure : ICloneable
    {
        int NewIndividuals { get; set; }
        IParentSelector<TDataStructure, TGene> InitialSelector { get; set; }
        List<MutationStep<TDataStructure, TGene>> MutationSteps { get; set; }
        IMutator<TDataStructure, TGene> Then(IMutation<TDataStructure, TGene> mutation, IParentSelector<TDataStructure, TGene> parentSelector = null);
        List<IIndividual<TDataStructure, TGene>> Create(IPopulation<TDataStructure, TGene> population, IFitness<TDataStructure, TGene> fitness);
    }
}
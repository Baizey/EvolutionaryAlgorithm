using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IMutator<TGeneStructure, TGene> where TGeneStructure : ICloneable
    {
        public IEvolutionaryAlgorithm<TGeneStructure, TGene> Algorithm { get; set; }

        int NewIndividuals { get; set; }
        IParentSelector<TGeneStructure, TGene> InitialSelector { get; set; }
        List<MutationStep<TGeneStructure, TGene>> MutationSteps { get; set; }

        IMutator<TGeneStructure, TGene> Then(IMutation<TGeneStructure, TGene> mutation,
            IParentSelector<TGeneStructure, TGene> parentSelector = null);

        List<IIndividual<TGeneStructure, TGene>> Create(IPopulation<TGeneStructure, TGene> population);
    }
}
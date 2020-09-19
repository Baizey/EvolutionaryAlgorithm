using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IMutator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        IParentSelector<TIndividual, TGeneStructure, TGene> InitialSelector { get; set; }
        List<IMutation<TIndividual, TGeneStructure, TGene>> Mutations { get; set; }
        IMutator<TIndividual, TGeneStructure, TGene> ThenApply(IMutation<TIndividual, TGeneStructure, TGene> mutation);
        void Mutate(IPopulation<TIndividual, TGeneStructure, TGene> oldIndividuals, List<TIndividual> newIndividuals);
    }
}
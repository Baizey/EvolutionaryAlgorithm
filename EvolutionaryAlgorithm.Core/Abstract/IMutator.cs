using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IMutator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public List<TIndividual> Reserves { get; set; }

        int Size => Reserves.Count;
        IParentSelector<TIndividual, TGeneStructure, TGene> InitialSelector { get; set; }
        List<MutationStep<TIndividual, TGeneStructure, TGene>> MutationSteps { get; set; }
        
        IMutator<TIndividual, TGeneStructure, TGene> Then(IMutation<TIndividual, TGeneStructure, TGene> mutation,
            IParentSelector<TIndividual, TGeneStructure, TGene> parentSelector = null);
        
        List<TIndividual> Create(IPopulation<TIndividual, TGeneStructure, TGene> population);
    }
}
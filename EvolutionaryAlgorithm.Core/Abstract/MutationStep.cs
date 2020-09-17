using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public class MutationStep<TIndividual, TGeneStructure, TGene> 
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public MutationStep(IMutation<TIndividual, TGeneStructure, TGene> mutation,
            IParentSelector<TIndividual, TGeneStructure, TGene> parentSelector)
        {
            Mutation = mutation;
            ParentSelector = parentSelector;
        }

        public readonly IMutation<TIndividual, TGeneStructure, TGene> Mutation;
        public readonly IParentSelector<TIndividual, TGeneStructure, TGene> ParentSelector;
    }
}
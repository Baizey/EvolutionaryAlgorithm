using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public class MutationStep<TDataStructure, TGene> where TDataStructure : ICloneable
    {
        public MutationStep(IMutation<TDataStructure, TGene> mutation, IParentSelector<TDataStructure, TGene> parentSelector)
        {
            Mutation = mutation;
            ParentSelector = parentSelector;
        }

        public readonly IMutation<TDataStructure, TGene> Mutation;
        public readonly IParentSelector<TDataStructure, TGene> ParentSelector;
    }
}
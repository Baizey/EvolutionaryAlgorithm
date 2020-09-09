using System;
using EvolutionaryAlgorithm.Core;

namespace EvolutionaryAlgorithm.Template.Algorithm
{
    public class MutationStep<T> where T : ICloneable
    {
        public MutationStep(IMutation<T> mutation, IParentSelector<T> parentSelector)
        {
            Mutation = mutation;
            ParentSelector = parentSelector;
        }

        public readonly IMutation<T> Mutation;
        public readonly IParentSelector<T> ParentSelector;
    }
}
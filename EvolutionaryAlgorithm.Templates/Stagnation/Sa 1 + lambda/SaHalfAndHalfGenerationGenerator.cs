﻿using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Stagnation
{
    public class SaHalfAndHalfGenerationGenerator : BitGenerationGenerator<IEndogenousBitIndividual>
    {
        public SaHalfAndHalfGenerationGenerator()
        {
            Mutator = new BitMutator<IEndogenousBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IEndogenousBitIndividual>())
                .ThenApply(new SaHalfAndHalfMutation());
            Filter = new SaHalfAndHalfGenerationFilter();
        }
    }
}
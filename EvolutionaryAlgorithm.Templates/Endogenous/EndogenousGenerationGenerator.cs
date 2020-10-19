using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Endogenous
{
    public class EndogenousGenerationGenerator : BitGenerationGenerator<IEndogenousBitIndividual>
    {
        public EndogenousGenerationGenerator(int learningRate)
        {
            Mutator = new Mutator<IEndogenousBitIndividual, BitArray, bool>()
                .CloneGenesFrom(new FirstParentSelector<IEndogenousBitIndividual>())
                .ThenApply(new EndogenousMutation(learningRate));
            Filter = new EndogenousGenerationFilter();
        }
    }
}
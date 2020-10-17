using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.BitImplementation.Templates;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;
using EvolutionaryAlgorithm.Template.Endogenous;

namespace EvolutionaryAlgorithm.Template.LambdaLambdaEndogenous
{
    public class LambdaLambdaEndogenousGenerationGenerator : BitGenerationGenerator<IEndogenousBitIndividual>
    {
        public LambdaLambdaEndogenousGenerationGenerator()
        {
            Mutator = new BitMutator<IEndogenousBitIndividual>()
                .ThenApply(new BitMiddlePointCrossover<IEndogenousBitIndividual>(
                    new MultiTournamentSelection<IEndogenousBitIndividual, BitArray, bool>(2)))
                .ThenApply(new EndogenousMutation(6));

            Filter = new BitElitismGenerationFilter<IEndogenousBitIndividual>(true);
        }
    }
}
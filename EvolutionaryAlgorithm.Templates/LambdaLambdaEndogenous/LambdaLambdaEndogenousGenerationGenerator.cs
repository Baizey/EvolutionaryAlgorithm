using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.BitImplementation.Templates;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;
using EvolutionaryAlgorithm.Template.Endogenous;

namespace EvolutionaryAlgorithm.Template.LambdaLambdaEndogenous
{
    public class LambdaLambdaEndogenousGenerationGenerator : BitGenerationGenerator<IEndogenousBitIndividual>
    {
        public LambdaLambdaEndogenousGenerationGenerator(int learningRate)
        {
            Mutator = new BitMutator<IEndogenousBitIndividual>()
                .Crossover(new BitMiddlePointCrossover<IEndogenousBitIndividual>(
                    new MultiTournamentSelection<IEndogenousBitIndividual, BitArray, bool>(2)))
                .ThenApply(new EndogenousMutation(learningRate));

            Filter = new BitElitismGenerationFilter<IEndogenousBitIndividual>(true);
        }
    }
}
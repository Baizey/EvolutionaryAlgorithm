using System;
using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Template;
using EvolutionaryAlgorithm.Template.Asymmetric;
using EvolutionaryAlgorithm.Template.Basics.Fitness;
using EvolutionaryAlgorithm.Template.Endogenous;
using EvolutionaryAlgorithm.Template.HeavyTail;
using EvolutionaryAlgorithm.Template.OneLambdaLambda;
using EvolutionaryAlgorithm.Template.Stagnation;

namespace EvolutionaryAlgorithm
{
    internal class Program
    {
        private static async Task Main()
        {
            const int geneCount = 500;
            const int mutationRate = 2;
            const int learningRate = 2;
            const int observationPhase = 5;
            const double beta = 1.5;

            var endogenous = new BitEvolutionaryAlgorithm<IEndogenousBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Self adapting
                    MutationRate = mutationRate,
                    // Constant, based on gene count
                    Lambda = (int) (3 * Math.Log(geneCount)),
                    // Always 1
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingEndogenousRandomPopulation(mutationRate)
                .UsingHeuristic(new EndogenousGenerationGenerator(learningRate))
                .UsingEvaluation(new OneMaxFitness<IEndogenousBitIndividual>());
            endogenous.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

            var stagnation = new BitEvolutionaryAlgorithm<IEndogenousBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Self adapting
                    MutationRate = mutationRate,
                    // Constant, based on gene count
                    Lambda = (int) (3 * Math.Log(geneCount)),
                    // Always 1
                    Mu = 1,
                })
                .UsingStagnationStatistics()
                .UsingEndogenousRandomPopulation(learningRate)
                .UsingHeuristic(new StagnationDetectionHyperHeuristic(mutationRate))
                .UsingEvaluation(new JumpFitness<IEndogenousBitIndividual>(5));
            stagnation.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

            var asymmetric = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Self adapting, based on learning rate
                    MutationRate = mutationRate,
                    // Always 1
                    Lambda = 1,
                    // Always 1
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingRandomPopulation()
                .UsingHeuristic(new AsymmetricGenerationGenerator(learningRate, observationPhase))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());
            asymmetric.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

            // TODO: Missing mutation implementation
            var oneLambdaLambda = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Constant
                    MutationRate = mutationRate,
                    // Self-adapting, initial 1
                    Lambda = 1,
                    // Always 1
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingRandomPopulation()
                .UsingHeuristic(new OneLambdaLambdaGenerationGenerator(learningRate))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());
            oneLambdaLambda.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

            var heavyTail = new BitEvolutionaryAlgorithm<IEndogenousBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Self adapting
                    MutationRate = mutationRate,
                    // Constant
                    Lambda = (int) (3 * Math.Log(geneCount)),
                    // Always 1
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingEndogenousRandomPopulation(mutationRate)
                .UsingHeuristic(new HeavyTailGenerationGenerator(beta))
                .UsingEvaluation(new OneMaxFitness<IEndogenousBitIndividual>());
            heavyTail.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

            // TODO: 6'th algorithm (lambda + lambda) Endogenous

            await Run(stagnation);
        }

        private static async Task Run<T>(IEvolutionaryAlgorithm<T, BitArray, bool> algorithm)
            where T : IBitIndividual
        {
            var n = algorithm.Parameters.GeneCount;
            await algorithm.Evolve(() => algorithm.Statistics.Best.Fitness >= n);
            Console.WriteLine(algorithm.Statistics);
        }
    }
}
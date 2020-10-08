using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Template;
using EvolutionaryAlgorithm.Template.Asymmetric;
using EvolutionaryAlgorithm.Template.Basics.Fitness;
using EvolutionaryAlgorithm.Template.Endogenous;
using EvolutionaryAlgorithm.Template.OneLambdaLambda;
using EvolutionaryAlgorithm.Template.Stagnation;

namespace EvolutionaryAlgorithm
{
    internal class Program
    {
        private static async Task Main()
        {
            const int geneCount = 500;

            var mu = 1;
            var lambda = 15;

            var mutationRate = 2;
            var learningRate = 2;
            var observationPhase = 5;

            var endogenous = new BitEvolutionaryAlgorithm<IEndogenousBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    MutationRate = mutationRate,
                    Lambda = (int) (3 * Math.Log(geneCount)),
                    // Always 1
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingEndogenousRandomPopulation(mutationRate)
                .UsingHeuristic(new EndogenousGenerationGenerator(learningRate))
                .UsingEvaluation(new OneMaxFitness<IEndogenousBitIndividual>());

            var stagnation = new BitEvolutionaryAlgorithm<IEndogenousBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    MutationRate = mutationRate,
                    Lambda = (int) (3 * Math.Log(geneCount)),
                    // Always 1
                    Mu = 1,
                })
                .UsingStagnationStatistics()
                .UsingEndogenousRandomPopulation(learningRate)
                .UsingHeuristic(new StagnationDetectionHyperHeuristic(mutationRate))
                .UsingEvaluation(new JumpFitness<IEndogenousBitIndividual>(5));

            var asymmetric = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
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

            // TODO: Missing mutation implementation
            var oneLambdaLambda = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
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

            // TODO: 5'th algorithm

            var algorithm = stagnation;

            algorithm.OnGenerationProgress = algo => Console.WriteLine(algorithm.Statistics);

            await algorithm.Evolve(a => a.Statistics.Best.Fitness >= geneCount);
            Console.WriteLine(algorithm.Statistics);
        }
    }
}
using System;
using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Terminations;
using EvolutionaryAlgorithm.Template;
using EvolutionaryAlgorithm.Template.FitnessFunctions;
using EvolutionaryAlgorithm.Template.Statistics;
using static EvolutionaryAlgorithm.Template.Mutations.PresetGenerator;

namespace EvolutionaryAlgorithm
{
    internal static class Program
    {
        private static async Task Main()
        {
            const int geneCount = 500;
            const int mutationRate = 2;
            const int repairRate = 2;
            const int learningRate = 2;
            const int observationPhase = 10;
            const double beta = 1.5;

            var endogenous = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Self adapting
                    MutationRate = mutationRate,
                    // Constant, based on gene count
                    Lambda = (int) (5 * Math.Log(geneCount)),
                    // Always 1
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingEndogenousRandomPopulation(mutationRate)
                .UsingHeuristic(SingleEndogenous(learningRate))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());
            endogenous.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

            var stagnation = new BitEvolutionaryAlgorithm<IBitIndividual>()
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
                .UsingHeuristic(StagnationDetection(mutationRate, 1))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());
            stagnation.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

            var asymmetric = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Self adapting, based on learning rate
                    MutationRate = 3,
                    // Always 1
                    Lambda = 1,
                    // Always 1
                    Mu = 1,
                })
                .UsingEndogenousRandomPopulation(mutationRate)
                .UsingStatistics(new AsymmetricBasicEvolutionaryStatistics<IBitIndividual>())
                .UsingHeuristic(Asymmetric(0.05, observationPhase))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());
            asymmetric.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

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
                .UsingStatistics(new LambdaBasicStatistics<IBitIndividual>())
                .UsingEndogenousRandomPopulation(mutationRate)
                .UsingHeuristic(Repair(learningRate, repairRate))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());
            oneLambdaLambda.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

            var heavyTail = new BitEvolutionaryAlgorithm<IBitIndividual>()
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
                .UsingHeuristic(HeavyTail(beta))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());
            heavyTail.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

            var lambdaEndogenous = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Self adapting
                    MutationRate = mutationRate,
                    // Constant, based on gene count
                    Lambda = (int) (3 * Math.Log(geneCount)),
                    // Constant, based on gene count
                    Mu = (int) (3 * Math.Log(geneCount)),
                })
                .UsingStatistics(new EndogenousBasicEvolutionaryStatistics())
                .UsingEndogenousRandomPopulation(mutationRate)
                .UsingHeuristic(MultiEndogenous(learningRate))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());
            lambdaEndogenous.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

            Run(asymmetric);
        }

        private static void Run<T>(IEvolutionaryAlgorithm<T, BitArray, bool> algorithm)
            where T : IBitIndividual
        {
            //await algorithm.Evolve(new TimeoutTermination<T, BitArray, bool>(TimeSpan.FromSeconds(1)));
            algorithm.Evolve(new FitnessTermination<T, BitArray, bool>(algorithm.Parameters.GeneCount));
            Console.WriteLine(algorithm.Statistics);
        }
    }
}
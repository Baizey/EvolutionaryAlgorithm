using System;
using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Terminations;
using EvolutionaryAlgorithm.Template;
using EvolutionaryAlgorithm.Template.Asymmetric;
using EvolutionaryAlgorithm.Template.Basics.Fitness;
using EvolutionaryAlgorithm.Template.Endogenous;
using EvolutionaryAlgorithm.Template.HeavyTail;
using EvolutionaryAlgorithm.Template.MultiEndogenous;
using EvolutionaryAlgorithm.Template.Repair;
using EvolutionaryAlgorithm.Template.Stagnation;

namespace EvolutionaryAlgorithm
{
    internal class Program
    {
        private static async Task Main()
        {
            const int geneCount = 500;
            const int mutationRate = 2;
            const int repairRate = 2;
            const int learningRate = 2;
            const int observationPhase = 10;
            const double beta = 1.5;

            var endogenous = new BitEvolutionaryAlgorithm<IEndogenousBitIndividual>()
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
                .UsingEvaluation(new OneMaxFitness<IEndogenousBitIndividual>());
            stagnation.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

            var asymmetric = new BitEvolutionaryAlgorithm<IEndogenousBitIndividual>()
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
                .UsingStatistics(new AsymmetricBasicEvolutionaryStatistics<IEndogenousBitIndividual>())
                .UsingHeuristic(new AsymmetricGenerationGenerator(0.05, observationPhase))
                .UsingEvaluation(new OneMaxFitness<IEndogenousBitIndividual>());
            asymmetric.OnGenerationProgress = algo => Console.WriteLine(algo.Statistics);

            var oneLambdaLambda = new BitEvolutionaryAlgorithm<IEndogenousBitIndividual>()
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
                .UsingStatistics(new LambdaBasicStatistics<IEndogenousBitIndividual>())
                .UsingEndogenousRandomPopulation(mutationRate)
                .UsingHeuristic(new OneLambdaLambdaGenerationGenerator(learningRate, repairRate))
                .UsingEvaluation(new OneMaxFitness<IEndogenousBitIndividual>());
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

            var lambdaEndogenous = new BitEvolutionaryAlgorithm<IEndogenousBitIndividual>()
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
                .UsingHeuristic(new MultiEndogenousGenerationGenerator(learningRate))
                .UsingEvaluation(new OneMaxFitness<IEndogenousBitIndividual>());
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
using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Template;
using EvolutionaryAlgorithm.Template.Basics.Fitness;
using EvolutionaryAlgorithm.Template.Endogenous;
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
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingRandomPopulation(mutationRate)
                .UsingEndogenousGeneration(learningRate)
                .UsingFitness(new OneMaxFitness<IEndogenousBitIndividual>());

            var stagnation = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    MutationRate = mutationRate,
                    Lambda = (int) (3 * Math.Log(geneCount)),
                    Mu = 1,
                })
                .UsingStagnationStatistics()
                .UsingRandomPopulation()
                .UsingStagnationDetectionGeneration(mutationRate)
                .UsingFitness(new JumpFitness<IBitIndividual>(5));
            
            var asymmetric = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    MutationRate = mutationRate,
                    Lambda = 1,
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingRandomPopulation()
                .UsingAsymmetricGeneration(learningRate, observationPhase)
                .UsingFitness(new OneMaxFitness<IBitIndividual>());

            // TODO: Missing mutation implementation
            var oneLambdaLambda = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    MutationRate = mutationRate,
                    Lambda = (int) (3 * Math.Log(geneCount)),
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingRandomPopulation()
                .UsingOneLambdaLambda(learningRate)
                .UsingFitness(new OneMaxFitness<IBitIndividual>());

            // TODO: Figure out 5'th algorithm
            // TODO: Wide tail

            var algorithm = stagnation;

            algorithm.OnGenerationProgress = algo => Console.WriteLine(algorithm.Statistics);

            await algorithm.Evolve(a => a.Statistics.Best.Fitness >= geneCount);
            Console.WriteLine(algorithm.Statistics);
        }
    }
}
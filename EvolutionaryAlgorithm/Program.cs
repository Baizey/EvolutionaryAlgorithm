using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Algorithm.Parameters;
using EvolutionaryAlgorithm.Core.Algorithm.Statistics;
using EvolutionaryAlgorithm.Core.Algorithm.Terminations;
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
            var geneCount = 500;
            var learningRate = 2;
            var mu = 1;
            var lambda = (int) (Math.Log(geneCount) * 3);
            var mutationRate = 2;
            var observationPhase = 5;

            var parameters = new Parameters
            {
                GeneCount = geneCount,
                Lambda = lambda,
                Mu = mu,
                MutationRate = mutationRate
            };

            var endogenous = new EvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    MutationRate = mutationRate,
                    Lambda = lambda,
                    Mu = 1,
                })
                .UsingParameters(parameters)
                .UsingBasicStatistics()
                .UsingRandomPopulation(learningRate)
                .UsingEndogenousGeneration(learningRate)
                .UsingFitness(new OneMaxFitness<IEndogenousBitIndividual>());

            var stagnation = new EvolutionaryAlgorithm<IBitIndividual, BitArray, bool>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    MutationRate = mutationRate,
                    Lambda = lambda,
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingRandomPopulation()
                .UsingStagnationDetectionGeneration(learningRate)
                .UsingFitness(new OneMaxFitness<IBitIndividual>());

            // TODO: Missing mutation implementation
            var asymmetric = new EvolutionaryAlgorithm<IBitIndividual, BitArray, bool>()
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

            // TODO: Missing generator implementation
            // TODO: Missing mutation implementation
            var oneLambdaLambda = new EvolutionaryAlgorithm<IBitIndividual, BitArray, bool>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    MutationRate = mutationRate,
                    Lambda = lambda,
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingRandomPopulation()
                .UsingFitness(new OneMaxFitness<IBitIndividual>());

            endogenous.OnGenerationProgress = algo => { Console.WriteLine(endogenous.Statistics); };

            await endogenous.Evolve(a => a.Statistics.Best.Fitness >= geneCount);
            Console.WriteLine(endogenous.Statistics);
        }
    }
}
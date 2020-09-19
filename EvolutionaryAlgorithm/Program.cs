using System;
using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions;
using EvolutionaryAlgorithm.Core.Algorithm.Crossover;
using EvolutionaryAlgorithm.Core.Algorithm.Statistics;
using EvolutionaryAlgorithm.Core.Algorithm.Terminations;
using EvolutionaryAlgorithm.Template.Fitness;
using EvolutionaryAlgorithm.Template.Mutation;
using EvolutionaryAlgorithm.Template.ParentSelector;
using EvolutionaryAlgorithm.Template.Selection;

namespace EvolutionaryAlgorithm
{
    internal class Program
    {
        private static async Task Main()
        {
            var random = new Random();
            const int
                geneCount = 50,
                mu = 1,
                lambda = 1,
                jump = 1;

            var classicInstance = new BitEvolutionaryAlgorithm
            {
                Parameters = new BitStaticParameters
                {
                    GeneCount = geneCount,
                    Lambda = lambda,
                    Mu = mu
                },
                Statistics = new BasicEvolutionaryStatistics<IBitIndividual, BitArray, bool>(),
                Population = BitPopulation.From(() => random.NextDouble() >= 0.5),
                Mutator = new BitMutator
                {
                    Mutations =
                    {
                        new CloneParent<IBitIndividual, BitArray, bool>(new BestFitnessParentSelector()),
                        new OneMaxStaticOptimalMutation()
                    }
                },
                Fitness = new OneMaxFitness(),
                GenerationFilter = new ElitismGenerationFilter(),
                Termination = new FitnessTermination<IBitIndividual, BitArray, bool>(geneCount)
            };

            var chainedInstance = BitEvolutionaryAlgorithm.Construct
                .UsingParameters(new BitStaticParameters
                {
                    GeneCount = geneCount,
                    Lambda = lambda,
                    Mu = mu
                })
                .UsingBasicStatistics()
                .UseRandomInitialGenome()
                .UsingMutator(mutator => mutator
                    .CloneGenesFrom(new BestFitnessParentSelector())
                    .ThenOneMaxStaticOptimalMutation())
                .UsingOneMaxFitness()
                .UsingElitismGenerationFilter()
                .UsingTermination(new FitnessTermination<IBitIndividual, BitArray, bool>(geneCount));

            await chainedInstance.Evolve();

            Console.WriteLine(chainedInstance.Statistics.Generations + ": " + chainedInstance.Best);
        }
    }
}
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

            var algo2 = new BitEvolutionaryAlgorithm
            {
                Parameters = new BitStaticParameters
                {
                    GeneCount = geneCount,
                    Lambda = lambda,
                    Mu = mu
                },
                Statistics = new BasicEvolutionaryStatistics<IBitIndividual, BitArray, bool>(),
                Fitness = new OneMaxFitness(),
                Population = BitPopulation.From(() => random.NextDouble() >= 0.5),
                GenerationFilter = new ElitismGenerationFilter(),
                Mutator = new BitMutator()
                    .CloneGenesFrom(new FirstParentSelector())
                    .ThenOneMaxStaticOptimalMutation(),
                Termination = new FitnessTermination<IBitIndividual, BitArray, bool>(geneCount)
            };

            var algo = new BitEvolutionaryAlgorithm()
                .UsingParameters(new BitStaticParameters
                {
                    GeneCount = geneCount,
                    Lambda = lambda,
                    Mu = mu
                })
                .UsingBasicStatistics()
                .UsingOneMaxFitness()
                .UseRandomInitialGenome()
                .UsingMutator(mutator => mutator
                    .CloneGenesFrom(new FirstParentSelector())
                    .ThenOneMaxStaticOptimalMutation())
                .UsingElitismGenerationFilter()
                .UsingTermination(new FitnessTermination<IBitIndividual, BitArray, bool>(geneCount));

            await algo.Evolve();

            Console.WriteLine(algo.Statistics.Generations + ": " + algo.Best);
        }
    }
}
using System;
using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm.Statistics;
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
                geneSize = 50,
                populationSize = 1,
                newIndividuals = 1,
                jump = 1;


            var algo2 = new BitEvolutionaryAlgorithm
            {
                Statistics = new BasicEvolutionaryStatistics<IBitIndividual, BitArray, bool>(),
                Fitness = new OneMaxFitness(),
                Parameters = new BitStaticParameters
                {
                    Lambda = newIndividuals,
                    Mu = populationSize,
                    MutationFactor = 1
                },
                Population = BitPopulation.From(populationSize, geneSize, () => random.NextDouble() >= 0.5),
                GenerationFilter = new ElitismGenerationFilter(),
                Mutator = new BitMutator().ThenOneMaxStaticOptimalMutation(geneSize)
            };

            var algo = new BitEvolutionaryAlgorithm()
                .UsingBasicStatistics()
                .UsingOneMaxFitness()
                .UsingStaticParameters(populationSize, newIndividuals, 1)
                .UsingRandomPopulation(populationSize, geneSize)
                .UsingMutator(new FirstParentSelector(), mutator => mutator
                    .ThenOneMaxStaticOptimalMutation(geneSize))
                .UsingElitismGenerationFilter();

            await algo.EvolveUntil(new FitnessTermination<IBitIndividual, BitArray, bool>(geneSize));

            Console.WriteLine(algo.Statistics.Generations + ": " + algo.Best);
        }
    }
}
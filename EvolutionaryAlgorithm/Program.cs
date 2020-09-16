using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.BitAlgorithm;
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
            const int
                geneSize = 50,
                populationSize = 1,
                newIndividuals = 1,
                jump = 1;

            var random = new Random();
            var algo = BitEvolutionaryAlgorithm.Construct
                .UsingBasicStatistics()
                .UsingPopulation(populationSize, geneSize, () => random.NextDouble() >= 0.5)
                .UsingOneMaxFitness()
                .UsingMutator(newIndividuals, new RandomParentSelector(),
                    mutator => mutator.ThenOneMaxStaticOptimalMutation(geneSize))
                .UsingElitismGenerationFilter();

            do
            {
                await algo.Evolve();
                Console.WriteLine(algo.Statistics.Generations + ": " + algo.Best);
            } while (algo.Best.Fitness < geneSize);
        }
    }
}
using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Template.Algorithm;
using EvolutionaryAlgorithm.Template.Fitness;
using EvolutionaryAlgorithm.Template.Mutation;
using EvolutionaryAlgorithm.Template.ParentSelector;
using EvolutionaryAlgorithm.Template.Population;
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
                newIndividuals = 1;

            var random = new Random();
            var algo = new BitEvolutionaryAlgorithm()
                .UsingOneMaxFitness()
                .UsingPopulation(populationSize, geneSize, () => random.NextDouble() >= 0.5)
                .UsingMutator(newIndividuals, new RandomParentSelector(),
                    mutator => mutator.ThenOneMaxStaticOptimalMutation(geneSize))
                .UsingElitismGenerationFilter();

            do
            {
                await algo.Evolve();
            } while (algo.Best.Fitness < geneSize);

            Console.WriteLine(algo.Population.Generation + ": " + algo.Best);
        }
    }
}
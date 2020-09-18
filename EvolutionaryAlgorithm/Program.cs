using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions;
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
                .UsingRandomPopulation(populationSize, geneSize)
                .UsingOneMaxFitness()
                .UsingMutator(newIndividuals, new FirstParentSelector(), mutator => mutator
                    .ThenOneMaxStaticOptimalMutation(geneSize))
                .UsingStaticElitismGenerationFilter();

            await algo.EvolveUntilFitnessExceed(geneSize);

            Console.WriteLine(algo.Statistics.Generations + ": " + algo.Best);
        }
    }
}
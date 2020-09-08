using System;
using System.Linq;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;

namespace EvolutionaryAlgorithm
{
    internal class Program
    {
        private static void Main()
        {
            var algo = new GeneticAlgorithm(
                new Population(2, 2, new IntegerChromosome(0, 1)),
                new FuncFitness(e => e.Fitness ??= e.GetGenes().Count(g => (bool) g.Value)),
                new EliteSelection(),
                new UniformCrossover(),
                new FlipBitMutation()
            )
            {
                MutationProbability = 1F / 32F, Termination = new FitnessThresholdTermination(32),
            };

            algo.GenerationRan += (_, __) =>
            {
                Console.WriteLine(algo.GenerationsNumber);
                Console.WriteLine(algo.BestChromosome.Fitness);
                Console.WriteLine();
            };
            algo.Start();
        }
    }
}
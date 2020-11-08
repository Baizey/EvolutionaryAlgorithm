using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Statistics;
using EvolutionaryAlgorithm.Template.Statistics;

namespace EvolutionaryAlgorithm.Template
{
    public static class Extensions
    {
        public static IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> UsingRandomPopulation(
            this IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> algorithm)
        {
            var random = new Random();
            return algorithm.UsingPopulation(
                new BitPopulation<IBitIndividual>(g => new BitIndividual(g, () => random.NextDouble() >= 0.5)));
        }

        public static IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> UsingEndogenousRandomPopulation(
            this IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> algorithm,
            int mutationRate) =>
            algorithm.UsingPopulation(BitIndividual.FromRandom(mutationRate));

        public static IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> UsingBasicStatistics(
            this IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> algorithm) =>
            algorithm.UsingStatistics(new BasicEvolutionaryStatistics<IBitIndividual, BitArray, bool>());
    }
}
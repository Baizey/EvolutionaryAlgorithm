using System;
using EvolutionaryAlgorithm.BitImplementation.Abstract;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions
{
    public static class BitPopulationExtensions
    {
        private static readonly Random Random = new Random();

        public static IBitEvolutionaryAlgorithm UsingRandomPopulation(this IBitEvolutionaryAlgorithm algo,
            int populationSize, int geneSize) =>
            algo.UsingPopulation(BitPopulation.From(populationSize, geneSize, Random.NextDouble() >= 0.5));

        public static IBitEvolutionaryAlgorithm UsingPopulation(this IBitEvolutionaryAlgorithm algo, int populationSize,
            int geneSize, Func<bool> geneGenerator) =>
            algo.UsingPopulation(BitPopulation.From(populationSize, geneSize, geneGenerator));

        public static IBitEvolutionaryAlgorithm UsingPopulation(this IBitEvolutionaryAlgorithm algo, int populationSize,
            int geneSize, bool defaultValue) =>
            algo.UsingPopulation(BitPopulation.From(populationSize, geneSize, defaultValue));
    }
}
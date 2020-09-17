using System;
using EvolutionaryAlgorithm.BitImplementation.Abstract;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions
{
    public static class BitPopulationExtensions
    {
        private static readonly Random Random = new Random();

        public static IBitEvolutionaryAlgorithm UsingRandomPopulation(this IBitEvolutionaryAlgorithm algo,
            int populationSize, int geneSize) => (IBitEvolutionaryAlgorithm) algo
            .UsingPopulation(populationSize, geneSize, () => Random.NextDouble() >= 0.5);

        public static IBitEvolutionaryAlgorithm UsingPopulation(this IBitEvolutionaryAlgorithm algo, int populationSize,
            int geneSize, Func<bool> geneGenerator) =>
            (IBitEvolutionaryAlgorithm) algo
                .UsingPopulation(BitPopulation.From(populationSize, geneSize, geneGenerator));

        public static IBitEvolutionaryAlgorithm UsingPopulation(this IBitEvolutionaryAlgorithm algo, int populationSize,
            int geneSize, bool defaultValue) =>
            (IBitEvolutionaryAlgorithm) algo.UsingPopulation(BitPopulation.From(populationSize, geneSize,
                defaultValue));
    }
}
using System;
using EvolutionaryAlgorithm.BitImplementation.Abstract;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions
{
    public static class BitPopulationExtensions
    {
        private static readonly Random Random = new Random();

        public static IBitEvolutionaryAlgorithm UseRandomInitialGenome(this IBitEvolutionaryAlgorithm algo) =>
            algo.UsingInitialGenome(BitPopulation.From(() => Random.NextDouble() >= 0.5));

        public static IBitEvolutionaryAlgorithm UsingPopulation(this IBitEvolutionaryAlgorithm algo,
            Func<bool> geneGenerator) => algo.UsingInitialGenome(BitPopulation.From(geneGenerator));

        public static IBitEvolutionaryAlgorithm
            UsingPopulation(this IBitEvolutionaryAlgorithm algo, bool defaultValue) =>
            algo.UsingInitialGenome(BitPopulation.From(() => defaultValue));
    }
}
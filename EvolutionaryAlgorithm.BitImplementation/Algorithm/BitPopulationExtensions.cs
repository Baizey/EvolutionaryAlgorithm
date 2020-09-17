using System;
using EvolutionaryAlgorithm.BitImplementation.Abstract;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm
{
    public static class BitPopulationExtensions
    {
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
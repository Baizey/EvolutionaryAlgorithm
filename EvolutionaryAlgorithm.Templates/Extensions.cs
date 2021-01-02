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
        public static IEvolutionaryAlgorithm<IBitIndividual, bool[], bool> UsingRandomPopulation(
            this IEvolutionaryAlgorithm<IBitIndividual, bool[], bool> algorithm,
            double mutationRate = 1) =>
            algorithm.UsingPopulation(BitIndividual.FromRandom(mutationRate));

        public static IEvolutionaryAlgorithm<IBitIndividual, bool[], bool> UsingBasicStatistics(
            this IEvolutionaryAlgorithm<IBitIndividual, bool[], bool> algorithm) =>
            algorithm.UsingStatistics(new BasicEvolutionaryStatistics<IBitIndividual, bool[], bool>());
    }
}
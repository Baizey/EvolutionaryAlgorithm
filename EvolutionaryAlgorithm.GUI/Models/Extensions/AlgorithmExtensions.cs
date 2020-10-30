using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.GUI.Models.Extensions
{
    public static class StatisticsExtensions
    {
        public static StatisticsView MapToStatisticsView(
            this IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> algorithm) =>
            new StatisticsView(algorithm);
    }
}
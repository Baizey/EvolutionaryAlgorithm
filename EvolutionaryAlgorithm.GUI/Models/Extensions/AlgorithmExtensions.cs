using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.GUI.Models.Extensions
{
    public static class AlgorithmExtensions
    {
        public static IUiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool> CloneUiStatistics(
            this IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> algorithm) =>
            (IUiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool>) algorithm.Statistics.Clone();
    }

    public static class StatisticsExtensions
    {
        public static StatisticsView MapToView(
            this IEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool> statistics, bool includeHistory) =>
            new StatisticsView((IUiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool>) statistics,
                includeHistory);

        public static StatisticsView MapToView(
            this IUiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool> statistics, bool includeHistory) =>
            new StatisticsView(statistics, includeHistory);
    }
}
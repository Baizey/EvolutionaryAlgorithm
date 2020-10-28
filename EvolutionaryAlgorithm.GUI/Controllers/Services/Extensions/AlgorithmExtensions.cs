using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Statistics;
using EvolutionaryAlgorithm.GUI.Models;

namespace EvolutionaryAlgorithm.GUI.Controllers.Services.Extensions
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
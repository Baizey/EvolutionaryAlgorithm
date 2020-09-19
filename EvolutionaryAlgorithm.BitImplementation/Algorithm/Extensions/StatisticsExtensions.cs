using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm.Statistics;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions
{
    public static class StatisticsExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingBasicStatistics(this IBitEvolutionaryAlgorithm algo) =>
            algo.UsingStatistics(new BasicEvolutionaryStatistics<IBitIndividual, BitArray, bool>());
    }
}
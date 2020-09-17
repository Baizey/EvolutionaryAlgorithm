using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions
{
    public static class StatisticsExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingBasicStatistics(this IBitEvolutionaryAlgorithm algo) =>
            (IBitEvolutionaryAlgorithm) algo.UsingStatistics(new BasicEvolutionaryStatistics<IBitIndividual, BitArray, bool>());
    }
}
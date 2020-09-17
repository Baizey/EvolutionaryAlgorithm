using System.Collections;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Core.BitAlgorithm
{
    public static class StatisticsExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingBasicStatistics(this IBitEvolutionaryAlgorithm algo) =>
            (IBitEvolutionaryAlgorithm) algo.UsingStatistics(new BasicEvolutionaryStatistics<IBitIndividual, BitArray, bool>());
    }
}
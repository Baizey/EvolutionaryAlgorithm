using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.GUI.Controllers.Services.Extensions
{
    public static class AlgorithmExtensions
    {
        public static IUiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool> CloneUiStatistics(
            this IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> algorithm) =>
            (IUiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool>) algorithm.Statistics.Clone();
    }
}
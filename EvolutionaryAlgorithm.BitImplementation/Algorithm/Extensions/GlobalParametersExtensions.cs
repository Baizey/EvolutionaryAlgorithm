using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm.Parameters;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions
{
    public static class GlobalParametersExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingStaticParameters(this IBitEvolutionaryAlgorithm algo,
            int geneCount, int populationSize, int newIndividualsSize)
        {
            algo.Parameters = new StaticParameters<IBitIndividual, BitArray, bool>
            {
                GeneCount = geneCount,
                Lambda = newIndividualsSize,
                Mu = populationSize,
            };
            return algo;
        }
    }
}
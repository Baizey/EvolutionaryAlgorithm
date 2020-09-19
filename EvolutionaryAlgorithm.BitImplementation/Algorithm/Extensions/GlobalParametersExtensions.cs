using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm.GlobalParameters;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions
{
    public static class GlobalParametersExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingStaticParameters(this IBitEvolutionaryAlgorithm algo,
            int populationSize, int newIndividualsSize, double mutationFactor)
        {
            algo.Parameters = new StaticParameters<IBitIndividual, BitArray, bool>()
            {
                Lambda = newIndividualsSize,
                Mu = populationSize,
                MutationFactor = mutationFactor
            };
            return algo;
        }
    }
}
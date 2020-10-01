using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm
{
    public class BitEvolutionaryAlgorithm
        : EvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, IBitEvolutionaryAlgorithm
    {
        public static BitEvolutionaryAlgorithm Construct => new BitEvolutionaryAlgorithm();
    }
}
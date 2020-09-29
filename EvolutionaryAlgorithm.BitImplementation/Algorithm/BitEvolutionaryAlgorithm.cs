using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm
{
    public class BitEvolutionaryAlgorithm
        : EvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, IBitEvolutionaryAlgorithm
    {
        public new static BitEvolutionaryAlgorithm Construct => new BitEvolutionaryAlgorithm();
    }
}
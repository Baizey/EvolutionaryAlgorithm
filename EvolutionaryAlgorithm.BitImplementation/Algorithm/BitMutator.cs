using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm
{
    public class BitMutator : Mutator<IBitIndividual, BitArray, bool>, IBitMutator
    {
        public BitMutator(IBitParentSelector initialSelector = null) : base(initialSelector)
        {
        }
    }
}
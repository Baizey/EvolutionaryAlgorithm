using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm
{
    public class BitMutator : Mutator<IBitIndividual, BitArray, bool>
    {
        public BitMutator(int size, IParentSelector<IBitIndividual, BitArray, bool> initialSelector = null) 
            : base(size, initialSelector)
        {
        }
        public BitMutator(int size) : base(size)
        {
        }
    }
}
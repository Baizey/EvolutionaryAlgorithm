using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Core.BitAlgorithm
{
    public class BitMutator : Mutator<IBitIndividual, BitArray, bool>
    {
        public BitMutator(int size, IParentSelector<IBitIndividual, BitArray, bool> initialSelector) 
            : base(size, initialSelector)
        {
        }
    }
}
using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Core.BitAlgorithm
{
    public class BitMutator : Mutator<BitArray, bool>, IBitMutator
    {
        public BitMutator(int newIndividuals, IParentSelector<BitArray, bool> initialSelector)
            : base(newIndividuals, initialSelector)
        {
        }
    }
}
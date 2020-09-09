using System.Collections;
using EvolutionaryAlgorithm.Core;

namespace EvolutionaryAlgorithm.Template.Algorithm
{
    public class BitMutator : Mutator<BitArray>
    {
        public BitMutator(int λ, IParentSelector<BitArray> initialSelector) : base(λ, initialSelector)
        {
        }
    }
}
using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Bit
{
    public class BitMutationStep : MutationStep<IBitIndividual, BitArray, bool>
    {
        public BitMutationStep(IBitMutation mutation, IBitParentSelector parentSelector)
            : base(mutation, parentSelector)
        {
        }
    }
}
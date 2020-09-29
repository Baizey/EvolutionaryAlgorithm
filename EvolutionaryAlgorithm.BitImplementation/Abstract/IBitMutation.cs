using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;

namespace EvolutionaryAlgorithm.BitImplementation.Abstract
{
    public interface IBitMutation : IMutation<IBitIndividual, BitArray, bool>
    {
    }
}
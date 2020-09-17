using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.BitImplementation.Abstract
{
    public interface IBitMutation : IMutation<IBitIndividual, BitArray, bool>
    {
    }
}
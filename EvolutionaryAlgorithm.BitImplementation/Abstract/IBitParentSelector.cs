using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase.Helpers;

namespace EvolutionaryAlgorithm.BitImplementation.Abstract
{
    public interface IBitSingleParentSelector : ISingleParentSelector<IBitIndividual, BitArray, bool>
    {
    }
}
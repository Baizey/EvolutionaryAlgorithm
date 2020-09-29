using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;

namespace EvolutionaryAlgorithm.BitImplementation.Abstract
{
    public interface IBitHyperHeuristic : IHyperHeuristic<IBitIndividual, BitArray, bool>
    {
    }
}
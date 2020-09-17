using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.BitImplementation.Abstract
{
    public interface IBitParentSelector : IParentSelector<IBitIndividual, BitArray, bool>
    {
    }
}
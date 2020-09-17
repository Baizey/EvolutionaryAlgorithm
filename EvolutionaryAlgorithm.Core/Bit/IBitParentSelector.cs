using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Bit
{
    public interface IBitParentSelector : IParentSelector<IBitIndividual, BitArray, bool>
    {
    }
}
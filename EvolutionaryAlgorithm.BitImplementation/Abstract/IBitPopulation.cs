using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.BitImplementation.Abstract
{
    public interface IBitPopulation : IPopulation<IBitIndividual, BitArray, bool>
    {
    }
}
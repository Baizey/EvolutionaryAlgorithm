using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.BitImplementation.Abstract
{
    public interface IBitGenerationFilter : IGenerationFilter<IBitIndividual, BitArray, bool>
    {
    }
}
using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Bit
{
    public interface IBitGenerationFilter : IGenerationFilter<IBitIndividual, BitArray, bool>
    {
    }
}
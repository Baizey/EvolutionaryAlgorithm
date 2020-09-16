using System.Collections;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Bit
{
    public interface IBitGenerationFilter : IGenerationFilter<BitArray, bool>
    {
        IPopulation<BitArray, bool> IGenerationFilter<BitArray, bool>.Filter(
            IPopulation<BitArray, bool> population,
            List<IIndividual<BitArray, bool>> newcomers) => Filter((IBitPopulation) population, newcomers);

        IBitPopulation Filter(IBitPopulation population, List<IBitIndividual> newcomers);
    }
}
using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Bit
{
    public interface IBitParentSelector : IParentSelector<BitArray, bool>
    {
        IIndividual<BitArray, bool> IParentSelector<BitArray, bool>.Select(IPopulation<BitArray, bool> population) =>
            Select((IBitPopulation) population);

        public IBitIndividual Select(IBitPopulation population);
    }
}
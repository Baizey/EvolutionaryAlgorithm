using System.Collections;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Bit
{
    public interface IBitMutator : IMutator<BitArray, bool>
    {
        IMutator<BitArray, bool> IMutator<BitArray, bool>.Then(IMutation<BitArray, bool> mutation,
            IParentSelector<BitArray, bool> parentSelector) =>
            Then((IBitMutation) mutation, (IBitParentSelector) parentSelector);

        List<IIndividual<BitArray, bool>> IMutator<BitArray, bool>.Create(
            IPopulation<BitArray, bool> population) =>
            Create((IBitPopulation) population);

        IBitMutator Then(IBitMutation mutation, IBitParentSelector parentSelector = null);
        List<IIndividual<BitArray, bool>> Create(IBitPopulation population);
    }
}
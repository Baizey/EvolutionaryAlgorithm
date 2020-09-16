using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Bit
{
    public interface IBitMutation : IMutation<BitArray, bool>
    {
        IIndividual<BitArray, bool> IMutation<BitArray, bool>.Mutate(
            IIndividual<BitArray, bool> child,
            IIndividual<BitArray, bool> parent) => Mutate((IBitIndividual) child, (IBitIndividual) parent);

        IBitIndividual Mutate(IBitIndividual child, IBitIndividual parent = null);
    }
}
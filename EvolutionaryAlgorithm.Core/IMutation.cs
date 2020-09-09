using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Core
{
    public interface IMutation<T>
    {
        IIndividual<T> Mutate(IIndividual<T> individual);
    }
}
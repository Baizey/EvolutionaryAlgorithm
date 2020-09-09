using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Core
{
    public interface IFitness<T>
    {
        double Evaluate(IIndividual<T> individual);
    }
}
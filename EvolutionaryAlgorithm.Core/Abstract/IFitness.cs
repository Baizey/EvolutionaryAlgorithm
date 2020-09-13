namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IFitness<T, TA>
    {
        double Evaluate(IIndividual<T, TA> individual);
    }
}
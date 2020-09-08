namespace GeneticAlgorithm.Interfaces
{
    public interface IBooleanIndividual : IIndividual
    {
        public bool Flip(int i);
    }
}
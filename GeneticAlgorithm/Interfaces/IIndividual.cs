namespace GeneticAlgorithm.Interfaces
{
    public interface IIndividual
    {
        IIndividual Clone();
        public int Count { get; }
        public double? Fitness { get; set; }
    }
}
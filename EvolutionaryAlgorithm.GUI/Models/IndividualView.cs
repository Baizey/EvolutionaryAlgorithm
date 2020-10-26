using EvolutionaryAlgorithm.BitImplementation;

namespace EvolutionaryAlgorithm.GUI.Models
{
    public class IndividualView
    {
        public bool[] Genes { get; set; }
        public double MutationRate { get; set; }
        public double Fitness { get; set; }

        public IndividualView(IEndogenousBitIndividual other)
        {
            Fitness = other.Fitness;
            MutationRate = other.MutationRate;
            Genes = new bool[other.Genes.Count];
            other.Genes.CopyTo(Genes, 0);
        }
    }
}
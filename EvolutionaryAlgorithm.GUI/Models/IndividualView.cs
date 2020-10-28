using EvolutionaryAlgorithm.BitImplementation;

namespace EvolutionaryAlgorithm.GUI.Models
{
    public class CheapIndividualView
    {
        public double Fitness { get; set; }

        public CheapIndividualView(IEndogenousBitIndividual other) => Fitness = other.Fitness;
    }

    public class IndividualViewView : CheapIndividualView
    {
        public bool[] Genes { get; set; }

        public IndividualViewView(IEndogenousBitIndividual other) : base(other)
        {
            Genes = new bool[other.Genes.Count];
            other.Genes.CopyTo(Genes, 0);
        }
    }
}
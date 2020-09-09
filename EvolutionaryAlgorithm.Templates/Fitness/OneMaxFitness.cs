using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.Core;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class OneMaxFitness : IFitness<BitArray>
    {
        private static double Evaluate(BitIndividual individual) =>
            (double) (individual.Fitness = individual.Genes.Cast<bool>().Count(gene => gene));

        public double Evaluate(IIndividual<BitArray> individual) => Evaluate((BitIndividual) individual);
    }
}
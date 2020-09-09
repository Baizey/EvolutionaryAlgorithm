using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.Core;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class LeadingOnesFitness : IFitness<BitArray>
    {
        private static double Evaluate(BooleanIndividual individual) =>
            (double) (individual.Fitness = individual.Genes.Cast<bool>().TakeWhile(gene => gene).Count());

        public double Evaluate(IIndividual<BitArray> individual) => Evaluate((BooleanIndividual) individual);
    }
}
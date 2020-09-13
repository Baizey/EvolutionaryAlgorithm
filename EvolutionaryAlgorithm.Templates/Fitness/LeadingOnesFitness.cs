using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class LeadingOnesFitness : IBitFitness
    {
        public double Evaluate(IIndividual<BitArray, bool> individual) =>
            (double) (individual.Fitness = individual.TakeWhile(e => e).Count());
    }
}
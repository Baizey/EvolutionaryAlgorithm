using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class OneMaxFitness : IBitFitness
    {
        public double Evaluate(IIndividual<BitArray, bool> individual) =>
            (double) (individual.Fitness = individual.Count(e => e));
    }
}
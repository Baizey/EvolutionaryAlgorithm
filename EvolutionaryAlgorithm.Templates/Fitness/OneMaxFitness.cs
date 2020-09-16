using System.Linq;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class OneMaxFitness : IBitFitness
    {
        public double Evaluate(IBitIndividual individual) => individual.Count(e => e);
    }
}
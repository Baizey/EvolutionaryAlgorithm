using System.Linq;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class LeadingOnesFitness : IBitFitness
    {
        public double Evaluate(IBitIndividual individual) => individual.TakeWhile(e => e).Count();
    }
}
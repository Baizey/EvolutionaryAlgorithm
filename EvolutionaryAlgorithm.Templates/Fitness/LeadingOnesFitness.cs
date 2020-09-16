using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class LeadingOnesFitness : IBitFitness
    {
        public IEvolutionaryAlgorithm<BitArray, bool> Algorithm { get; set; }

        public double Evaluate(IBitIndividual individual) => individual.TakeWhile(e => e).Count();
    }
}
using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class LeadingOnesFitness : IBitFitness
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public double Evaluate(IBitIndividual individual) => individual.TakeWhile(e => e).Count();
    }
}
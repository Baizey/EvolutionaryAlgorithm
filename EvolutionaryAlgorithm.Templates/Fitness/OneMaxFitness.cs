using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class OneMaxFitness : IBitFitness
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public double Evaluate(IBitIndividual individual) => individual.Count(e => e);
    }
}
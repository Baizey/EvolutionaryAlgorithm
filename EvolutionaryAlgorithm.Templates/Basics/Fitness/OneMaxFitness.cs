using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Fitness;

namespace EvolutionaryAlgorithm.Template.Basics.Fitness
{
    public class OneMaxFitness<TIndividual> : IFitness<TIndividual, BitArray, bool>
        where TIndividual : IBitIndividual
    {
        public IEvolutionaryAlgorithm<TIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public double Evaluate(TIndividual individual) => individual.Ones;
    }
}
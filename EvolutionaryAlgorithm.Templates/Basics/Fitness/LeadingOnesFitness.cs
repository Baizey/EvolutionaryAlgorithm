using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Fitness;

namespace EvolutionaryAlgorithm.Template.Basics.Fitness
{
    public class LeadingOnesFitness<TIndividual> : IBitFitness<TIndividual>
        where TIndividual : IBitIndividual
    {
        public IEvolutionaryAlgorithm<TIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public double Evaluate(TIndividual individual)
        {
            var i = 0;
            for (; i < individual.Size; i++)
                if (!individual[i])
                    break;
            return i;
        }
    }
}
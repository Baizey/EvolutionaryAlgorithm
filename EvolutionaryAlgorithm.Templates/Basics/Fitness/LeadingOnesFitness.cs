using System.Collections;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Basics.Fitness
{
    public class LeadingOnesFitness<TIndividual> : IFitness<TIndividual, BitArray, bool>
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
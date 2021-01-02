using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.FitnessFunctions
{
    public class LeadingOnesFitness<TIndividual> : IBitFitness<TIndividual>
        where TIndividual : IBitIndividual
    {
        public IEvolutionaryAlgorithm<TIndividual, bool[], bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public long Calls { get; private set; }

        public double Evaluate(TIndividual individual)
        {
            Calls++;
            var i = 0;
            for (; i < individual.Count; i++)
                if (!individual[i])
                    break;
            return i;
        }
    }
}
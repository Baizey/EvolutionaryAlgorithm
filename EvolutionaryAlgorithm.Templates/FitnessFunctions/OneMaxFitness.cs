using System.Collections;
using System.Threading;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.FitnessFunctions
{
    public class OneMaxFitness<TIndividual> : IBitFitness<TIndividual>
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
            return individual.Ones;
        }
    }
}
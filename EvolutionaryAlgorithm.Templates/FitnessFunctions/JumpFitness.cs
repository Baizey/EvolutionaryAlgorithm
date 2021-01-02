using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.FitnessFunctions
{
    public class JumpFitness<TIndividual> : IBitFitness<TIndividual>
        where TIndividual : IBitIndividual
    {
        public IEvolutionaryAlgorithm<TIndividual, bool[], bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public long Calls { get; private set; }
        private readonly int _jump;

        public JumpFitness(int jump)
        {
            _jump = jump;
        }

        public double Evaluate(TIndividual individual)
        {
            Calls++;
            var ones = individual.Ones;
            var zeroes = individual.Count - ones;
            if (zeroes == 0 || zeroes >= _jump)
                return ones;
            else
                return zeroes;
        }
    }
}
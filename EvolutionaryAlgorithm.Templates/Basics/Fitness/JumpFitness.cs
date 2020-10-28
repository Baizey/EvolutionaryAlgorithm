using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.Basics.Fitness
{
    public class JumpFitness<TIndividual> : IBitFitness<TIndividual>
        where TIndividual : IBitIndividual
    {
        public IEvolutionaryAlgorithm<TIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        private readonly int _jump;

        public JumpFitness(int jump)
        {
            _jump = jump;
        }

        public double Evaluate(TIndividual individual)
        {
            if (individual.Zeros == 0 || individual.Zeros > _jump)
                return individual.Ones;
            else
                return individual.Zeros;
        }
    }
}
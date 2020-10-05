using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Fitness;

namespace EvolutionaryAlgorithm.Template.Basics.Fitness
{
    public class JumpFitness<TIndividual> : IBitFitness<TIndividual>
        where TIndividual : IBitIndividual
    {
        public IEvolutionaryAlgorithm<TIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
            _total = Algorithm.Parameters.GeneCount;
            _limit = _total - _jump;
        }

        private int _total, _limit;
        private readonly int _jump;

        public JumpFitness(int jump)
        {
            _jump = jump;
        }

        public double Evaluate(TIndividual individual)
        {
            var flipped = individual.Ones;
            if (flipped == _total) return _total;
            return flipped < _limit ? flipped : _total - flipped;
        }
    }
}
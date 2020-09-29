using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Basics.Fitness
{
    public class JumpFitness : IBitFitness
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
            _total = Algorithm.Parameters.GeneCount;
            _limit = _total - _jump;
        }

        public void Update()
        {
        }

        private int _total, _limit;
        private readonly int _jump;

        public JumpFitness(int jump)
        {
            _jump = jump;
        }

        public double Evaluate(IBitIndividual individual)
        {
            var flipped = individual.Ones;
            if (flipped == _total) return _total;
            return flipped < _limit ? flipped : _total - flipped;
        }
    }
}
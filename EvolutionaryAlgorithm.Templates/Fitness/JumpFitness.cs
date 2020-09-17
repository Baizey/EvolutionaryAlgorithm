using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class JumpFitness : IBitFitness
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        private readonly int _total, _limit;

        public JumpFitness(int total, int jump)
        {
            _total = total;
            _limit = _total - jump;
        }

        public double Evaluate(IBitIndividual individual)
        {
            var flipped = individual.Count(e => e);
            if (flipped == _total) return _total;
            return flipped < _limit ? flipped : _total - flipped;
        }
    }
}
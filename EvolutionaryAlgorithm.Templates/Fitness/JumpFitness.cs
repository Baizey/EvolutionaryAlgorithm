using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class JumpFitness : IBitFitness
    {
        private readonly int _total, _limit;

        public JumpFitness(int total, int jump)
        {
            _total = total;
            _limit = _total - jump;
        }

        public double Evaluate(IIndividual<BitArray, bool> individual)
        {
            var flipped = individual.Count(e => e);
            if (flipped == _total) return _total;
            return flipped < _limit ? flipped : _total - flipped;
        }
    }
}
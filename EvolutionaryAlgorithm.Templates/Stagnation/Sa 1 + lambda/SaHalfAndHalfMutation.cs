using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;

namespace EvolutionaryAlgorithm.Template.Stagnation
{
    public class SaHalfAndHalfMutation : IBitMutation<IEndogenousBitIndividual>
    {
        private readonly MutationApplier _applier = new MutationApplier();
        private IParameters _parameters;
        private int _n;
        public IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _n = _parameters.GeneCount;
        }

        public void Update()
        {
        }

        public void Mutate(int index, IEndogenousBitIndividual child)
        {
            // Create x_i by flipping each bit in a copy of x independently with probability [r / 2n] if [i ≤ λ/2] and with probability [2r / n] otherwise.
            if (index < _parameters.Lambda / 2)
            {
                child.MutationRate = Math.Max(1, _parameters.MutationRate / 2);
                _applier.Mutate(child, _n);
            }
            else
            {
                child.MutationRate = Math.Min(_n / 2, _parameters.MutationRate * 2);
                _applier.Mutate(child, _n);
            }
        }
    }
}
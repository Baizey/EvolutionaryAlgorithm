using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;

namespace EvolutionaryAlgorithm.Template.Mutations
{
    public class HalfAndHalfMutation : IBitMutation<IBitIndividual>
    {
        private readonly MutationApplier _applier = new MutationApplier();
        private IParameters _parameters;
        private int _n;
        private readonly double _learningRate;
        private int _mutationRateCap;
        private Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, int> _calc;
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }


        public HalfAndHalfMutation(double learningRate = 2,
            Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, int> calculateMaxMutationRate = null)
        {
            _learningRate = learningRate;
            _calc = calculateMaxMutationRate ?? (a => a.Parameters.GeneCount / 2);
        }

        public void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _n = _parameters.GeneCount;
            _mutationRateCap = _calc.Invoke(Algorithm);
        }

        public void Update()
        {
        }

        public void Mutate(int index, IBitIndividual child)
        {
            // Create x_i by flipping each bit in a copy of x independently with probability [r / 2n] if [i ≤ λ/2] and with probability [2r / n] otherwise.
            if (index < _parameters.Lambda / 2)
            {
                child.MutationRate = Math.Max(1, _parameters.MutationRate / _learningRate);
                _applier.Mutate(child, _n);
            }
            else
            {
                child.MutationRate = Math.Min(_mutationRateCap, _parameters.MutationRate * _learningRate);
                _applier.Mutate(child, _n);
            }
        }
    }
}
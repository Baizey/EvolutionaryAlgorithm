using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;

namespace EvolutionaryAlgorithm.Template.SelfAdjusters
{
    public class SelfAdjustingMutationRate : IBitParameterAdjuster<IBitIndividual>
    {
        public IEvolutionaryAlgorithm<IBitIndividual, bool[], bool> Algorithm { get; set; }
        private IParameters _parameters;
        private int _minRate, _maxRate;
        private readonly int _learningRate;
        private readonly Func<IEvolutionaryAlgorithm<IBitIndividual, bool[], bool>, int> _calcCap;

        public SelfAdjustingMutationRate(
            int learningRate,
            Func<IEvolutionaryAlgorithm<IBitIndividual, bool[], bool>, int> maxMutationRate)
        {
            _learningRate = learningRate;
            _calcCap = maxMutationRate;
        }

        public void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _minRate = 1;
            _maxRate = _calcCap.Invoke(Algorithm);
        }

        public void Mutate(int index, IBitIndividual child) =>
            child.MutationRate = index < _parameters.Lambda / 2
                ? Math.Max(_minRate, _parameters.MutationRate / _learningRate)
                : Math.Min(_maxRate, _parameters.MutationRate * _learningRate);

        public void Update() => _parameters.MutationRate = Algorithm.Best.MutationRate;
    }
}
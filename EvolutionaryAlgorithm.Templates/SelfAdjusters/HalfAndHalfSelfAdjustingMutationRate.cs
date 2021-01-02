using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;

namespace EvolutionaryAlgorithm.Template.SelfAdjusters
{
    public class HalfAndHalfSelfAdjustingMutationRate : IBitParameterAdjuster<IBitIndividual>
    {
        public IEvolutionaryAlgorithm<IBitIndividual, bool[], bool> Algorithm { get; set; }
        private IParameters _parameters;
        private readonly Random _random;
        private int _maxRate;
        private readonly Func<IEvolutionaryAlgorithm<IBitIndividual, bool[], bool>, int> _calcCap;
        private readonly int _learningRate;
        private int _minRate;

        public HalfAndHalfSelfAdjustingMutationRate(
            int learningRate,
            Func<IEvolutionaryAlgorithm<IBitIndividual, bool[], bool>, int> calcCap)
        {
            _learningRate = learningRate;
            _calcCap = calcCap;
            _random = new Random();
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

        public void Update() =>
            _parameters.MutationRate = _random.NextDouble() >= 0.5
                ? Algorithm.Best.MutationRate
                : _random.NextDouble() >= 0.5
                    ? Math.Min(_maxRate, _parameters.MutationRate * _learningRate)
                    : Math.Max(1, _parameters.MutationRate / _learningRate);
    }
}
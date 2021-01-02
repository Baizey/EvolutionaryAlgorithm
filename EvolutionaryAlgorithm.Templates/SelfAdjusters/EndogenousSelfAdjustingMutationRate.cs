using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;

namespace EvolutionaryAlgorithm.Template.SelfAdjusters
{
    public class EndogenousSelfAdjustingMutationRate : IBitParameterAdjuster<IBitIndividual>
    {
        public IEvolutionaryAlgorithm<IBitIndividual, bool[], bool> Algorithm { get; set; }
        private IParameters _parameters;
        private readonly Random _random = new Random();
        private int _minRate, _maxRate;
        private readonly int _learningRate;
        private readonly Func<IEvolutionaryAlgorithm<IBitIndividual, bool[], bool>, int> _calcCap;

        public EndogenousSelfAdjustingMutationRate(
            int learningRate,
            Func<IEvolutionaryAlgorithm<IBitIndividual, bool[], bool>, int> maxMutationRate = null)
        {
            _learningRate = learningRate;
            _calcCap = maxMutationRate ?? (algo => (int) Math.Pow(
                _learningRate,
                Math.Log(algo.Parameters.GeneCount / (2D * _learningRate), _learningRate)));
        }

        public void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _minRate = 1;
            _maxRate = _calcCap.Invoke(Algorithm);
        }

        public void Mutate(int index, IBitIndividual child) =>
            child.MutationRate = _random.NextDouble() < 0.5
                ? Math.Max(_minRate, child.MutationRate / _learningRate)
                : Math.Min(_maxRate, child.MutationRate * _learningRate);

        public void Update() => _parameters.MutationRate = Algorithm.Best.MutationRate;
    }
}
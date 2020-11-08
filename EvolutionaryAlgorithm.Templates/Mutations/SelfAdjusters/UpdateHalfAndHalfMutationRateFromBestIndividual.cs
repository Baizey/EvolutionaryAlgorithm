using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;

namespace EvolutionaryAlgorithm.Template.Mutations.SelfAdjusters
{
    public class UpdateHalfAndHalfMutationRateFromBestIndividual : IBitParameterAdjuster<IBitIndividual>
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }
        private IParameters _parameters;
        private readonly Random _random;
        private int _maxRate;
        private readonly Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, int> _calcCap;

        public UpdateHalfAndHalfMutationRateFromBestIndividual(
            Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, int> calcCap = null)
        {
            _calcCap = calcCap ?? (a => a.Parameters.GeneCount / 4);
            _random = new Random();
        }

        public void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _maxRate = _calcCap.Invoke(Algorithm);
        }

        public void Update()
        {
            _parameters.MutationRate = Algorithm.Best.MutationRate;
            if (_random.NextDouble() >= 0.5)
                _parameters.MutationRate = _random.NextDouble() >= 0.5
                    ? Math.Min(_maxRate, _parameters.MutationRate * 2)
                    : Math.Max(1, _parameters.MutationRate / 2);
            else
                _parameters.MutationRate = Algorithm.Best.MutationRate;
        }
    }
}
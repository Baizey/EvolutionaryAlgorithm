using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.Template.Mutations
{
    public class AsymmetricMutation : IBitMutation<IBitIndividual>
    {
        private readonly int _observationPhase;
        private readonly double _learningRate, _learningCap;
        public int ObservationResult { get; private set; }
        public int ObservationCounter { get; private set; }
        public double ZeroFocus { get; private set; }
        public double OneFocus { get; private set; }

        private IEvolutionaryStatistics<IBitIndividual, bool[], bool> _statistics;
        private readonly Random _random = new Random();
        private bool _tryFocusZero;
        private readonly MutationApplier _applier = new MutationApplier();
        private IParameters _parameters;

        public IEvolutionaryAlgorithm<IBitIndividual, bool[], bool> Algorithm { get; set; }

        public AsymmetricMutation(double learningRate, int observationPhase)
        {
            _observationPhase = Math.Max(2, observationPhase);
            _learningRate = Math.Max(0.00, Math.Min(0.5, learningRate));
            ObservationCounter = _observationPhase;

            _tryFocusZero = true;
            _learningCap = 2 * _learningRate;
            ZeroFocus = 0.5;
            OneFocus = 1D - ZeroFocus;
            ObservationResult = 0;
        }

        public void Initialize()
        {
            _statistics = Algorithm.Statistics;
            _parameters = Algorithm.Parameters;
        }

        public void Mutate(int index, IBitIndividual child)
        {
            double zeroRate, oneRate;
            if (_tryFocusZero)
            {
                zeroRate = ZeroFocus + _learningRate;
                oneRate = OneFocus - _learningRate;
            }
            else
            {
                zeroRate = ZeroFocus - _learningRate;
                oneRate = OneFocus + _learningRate;
            }

            _applier.MutateAsymmetric(child, _parameters.MutationRate, zeroRate, oneRate);
        }

        public void Update()
        {
            if (_statistics.ImprovedFitness)
                ObservationResult += _tryFocusZero ? 1 : -1;

            if (--ObservationCounter <= 0)
            {
                if (ObservationResult < 0) RaiseOneFocus();
                else if (ObservationResult > 0) RaiseZeroFocus();
                else if (_random.NextDouble() >= 0.5) RaiseOneFocus();
                else RaiseZeroFocus();

                ObservationCounter = _observationPhase;
                ObservationResult = 0;
            }

            _tryFocusZero = !_tryFocusZero;
        }

        private void RaiseOneFocus()
        {
            ZeroFocus = Math.Max(ZeroFocus - _learningRate, _learningCap);
            OneFocus = 1D - ZeroFocus;
        }

        private void RaiseZeroFocus()
        {
            OneFocus = Math.Max(OneFocus - _learningRate, _learningCap);
            ZeroFocus = 1D - OneFocus;
        }
    }
}
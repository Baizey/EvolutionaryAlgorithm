using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Mutation
{
    public class AsymmetricMutation : IBitMutation
    {
        private readonly int _observationPhase;
        private readonly double _learningRate;

        private int _b, _observationCounter;
        private double _r0, _r1;

        private IEvolutionaryStatistics<IBitIndividual, BitArray, bool> _statistics;
        private readonly Random _random;

        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public AsymmetricMutation(double learningRate, int observationPhase)
        {
            _random = new Random();

            _observationPhase = observationPhase;
            _observationCounter = _observationPhase;

            _learningRate = learningRate;
            _r0 = 0.5;
            _r1 = 1D - _r0;
            _b = 0;
        }

        public void Initialize()
        {
            _statistics = Algorithm.Statistics;
        }

        public void Mutate(int index, IBitIndividual child)
        {
            double zeroRate, oneRate;
            if (_statistics.Generations % 2 == 0)
            {
                zeroRate = _r0 + _learningRate;
                oneRate = _r1 - _learningRate;
            }
            else
            {
                zeroRate = _r0 - _learningRate;
                oneRate = _r1 + _learningRate;
            }

            // TODO: mutate individual
            throw new NotImplementedException();
        }


        private void LowerR0()
        {
            _r0 = Math.Max(_r0 - _learningRate, 2 * _learningRate);
            _r1 = 1D - _r0;
        }

        private void RaiseR0()
        {
            _r0 = Math.Min(_r0 + _learningRate, 1D - 2 * _learningRate);
            _r1 = 1D - _r0;
        }

        private void UpdateObservations()
        {
            if (_statistics.StagnantGeneration != 0) return;
            if (_statistics.Generations % 2 == 0)
                _b++;
            else
                _b--;
        }

        private void UpdateRates()
        {
            if (--_observationCounter != 0) return;

            if (_b < 0) LowerR0();
            else if (_b > 0) RaiseR0();
            else if (_random.NextDouble() >= 0.5) LowerR0();
            else RaiseR0();

            _observationCounter = _observationPhase;
            _b = 0;
        }

        public void Update()
        {
            UpdateObservations();
            UpdateRates();
        }
    }
}
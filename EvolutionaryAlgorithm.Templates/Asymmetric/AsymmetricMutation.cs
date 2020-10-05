﻿using System;
using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.Template.Asymmetric
{
    public class AsymmetricMutation : IBitMutation<IBitIndividual>
    {
        private readonly int _observationPhase;
        private readonly double _learningRate, _learningCap;

        private int _b, _observationCounter;
        private double _r0, _r1;

        private IEvolutionaryStatistics<IBitIndividual, BitArray, bool> _statistics;
        private readonly Random _random;
        private bool _oddGeneration;
        private MutationApplier _applier;
        private IParameters _parameters;

        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public AsymmetricMutation(double learningRate, int observationPhase)
        {
            _applier = new MutationApplier();
            _random = new Random();

            _observationPhase = observationPhase;
            _observationCounter = _observationPhase;

            _oddGeneration = true;
            _learningRate = learningRate;
            _learningCap = 2 * _learningRate;
            _r0 = 0.5;
            _r1 = 1D - _r0;
            _b = 0;
        }

        public void Initialize()
        {
            _statistics = Algorithm.Statistics;
            _parameters = Algorithm.Parameters;
        }

        public async Task Mutate(int index, IBitIndividual child)
        {
            double zeroRate, oneRate;
            if (_oddGeneration)
            {
                zeroRate = _r0 + _learningRate;
                oneRate = _r1 - _learningRate;
            }
            else
            {
                zeroRate = _r0 - _learningRate;
                oneRate = _r1 + _learningRate;
            }

            _applier.Mutate(child, _parameters.MutationRate, zeroRate, oneRate);
        }

        public void Update()
        {
            UpdateObservations();
            UpdateRates();
        }

        private void UpdateObservations()
        {
            if (!_statistics.ImprovedFitness) return;
            _b += _oddGeneration ? 1 : -1;
            _oddGeneration = !_oddGeneration;
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

        private void LowerR0()
        {
            _r0 = Math.Max(_r0 - _learningRate, _learningCap);
            _r1 = 1D - _r0;
        }

        private void RaiseR0()
        {
            _r1 = Math.Max(_r1 - _learningRate, _learningCap);
            _r0 = 1D - _r1;
        }
    }
}
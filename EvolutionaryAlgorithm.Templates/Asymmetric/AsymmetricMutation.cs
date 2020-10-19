using System;
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

        public int B { get; private set; }
        public int ObservationCounter { get; private set; }
        public double R0 { get; private set; }
        public double R1 { get; private set; }

        private IEvolutionaryStatistics<IBitIndividual, BitArray, bool> _statistics;
        private readonly Random _random = new Random();
        private bool _oddGeneration;
        private readonly MutationApplier _applier = new MutationApplier();
        private IParameters _parameters;

        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public AsymmetricMutation(double learningRate, int observationPhase)
        {
            _observationPhase = observationPhase;
            ObservationCounter = _observationPhase;

            _oddGeneration = true;
            _learningRate = learningRate;
            _learningCap = 2 * _learningRate;
            R0 = 0.5;
            R1 = 1D - R0;
            B = 0;
        }

        public void Initialize()
        {
            _statistics = Algorithm.Statistics;
            _parameters = Algorithm.Parameters;
        }

        public void Mutate(int index, IBitIndividual child)
        {
            double zeroRate, oneRate;
            if (_oddGeneration)
            {
                zeroRate = R0 + _learningRate;
                oneRate = R1 - _learningRate;
            }
            else
            {
                zeroRate = R0 - _learningRate;
                oneRate = R1 + _learningRate;
            }

            _applier.MutateAsymmetric(child, _parameters.MutationRate, zeroRate, oneRate);
        }

        public void Update()
        {
            UpdateObservations();
            UpdateRates();
        }

        private void UpdateObservations()
        {
            if (!_statistics.ImprovedFitness) return;
            B += _oddGeneration ? 1 : -1;
            _oddGeneration = !_oddGeneration;
        }

        private void UpdateRates()
        {
            if (--ObservationCounter != 0) return;

            if (B < 0) LowerR0();
            else if (B > 0) RaiseR0();
            else if (_random.NextDouble() >= 0.5) LowerR0();
            else RaiseR0();

            ObservationCounter = _observationPhase;
            B = 0;
        }

        private void LowerR0()
        {
            R0 = Math.Max(R0 - _learningRate, _learningCap);
            R1 = 1D - R0;
        }

        private void RaiseR0()
        {
            R1 = Math.Max(R1 - _learningRate, _learningCap);
            R0 = 1D - R1;
        }
    }
}
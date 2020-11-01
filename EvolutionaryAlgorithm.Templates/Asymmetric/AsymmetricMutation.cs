using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.Template.Asymmetric
{
    public class AsymmetricMutation : IBitMutation<IEndogenousBitIndividual>
    {
        private readonly int _observationPhase;
        private readonly double _learningRate, _learningCap;

        public int ShouldFocusZero { get; private set; }
        public int ObservationCounter { get; private set; }
        public double R0 { get; private set; }
        public double R1 { get; private set; }

        private IEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool> _statistics;
        private readonly Random _random = new Random();
        private bool _focusZero;
        private readonly MutationApplier _applier = new MutationApplier();
        private IParameters _parameters;

        public IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> Algorithm { get; set; }

        public AsymmetricMutation(double learningRate, int observationPhase)
        {
            _observationPhase = observationPhase;
            ObservationCounter = _observationPhase;

            _focusZero = true;
            _learningRate = learningRate;
            _learningCap = 2 * _learningRate;
            R0 = 0.5;
            R1 = 1D - R0;
            ShouldFocusZero = 0;
        }

        public void Initialize()
        {
            _statistics = Algorithm.Statistics;
            _parameters = Algorithm.Parameters;
        }

        public void Mutate(int index, IEndogenousBitIndividual child)
        {
            double zeroRate, oneRate;
            if (_focusZero)
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
            if (_statistics.ImprovedFitness)
                ShouldFocusZero += _focusZero ? 1 : -1;

            if (--ObservationCounter <= 0)
            {
                Console.WriteLine($"Observing update B: {ShouldFocusZero}, 0: {R0}, 1: {R1}");
                if (ShouldFocusZero < 0) LowerR0();
                else if (ShouldFocusZero > 0) RaiseR0();
                else if (_random.NextDouble() >= 0.5) LowerR0();
                else RaiseR0();
                Console.WriteLine($"0: {R0}, 1: {R1}");
                ObservationCounter = _observationPhase;
                ShouldFocusZero = 0;
            }

            _focusZero = !_focusZero;
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
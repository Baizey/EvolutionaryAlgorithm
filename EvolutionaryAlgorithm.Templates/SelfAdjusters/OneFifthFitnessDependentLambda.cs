using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.Template.SelfAdjusters
{
    public class OneFifthFitnessDependentLambda : IBitParameterAdjuster<IBitIndividual>
    {
        private IParameters _parameters;
        private IEvolutionaryStatistics<IBitIndividual, BitArray, bool> _statistics;
        private readonly double _shrinkRate, _growthRate;
        private double _actualLambda;

        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public OneFifthFitnessDependentLambda(double learningRate)
        {
            _shrinkRate = learningRate;
            _growthRate = Math.Pow(learningRate, 0.25D);
        }

        public void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _statistics = Algorithm.Statistics;
            _actualLambda = _parameters.Lambda;
        }

        public void Mutate(int index, IBitIndividual child)
        {
            // Only changes lambda between generations
        }

        public void Update()
        {
            _actualLambda = _statistics.ImprovedFitness
                ? Math.Max(_actualLambda / _shrinkRate, 1)
                : Math.Min(_actualLambda * _growthRate, _parameters.GeneCount);
            _parameters.Lambda = (int) _actualLambda;
        }
    }
}
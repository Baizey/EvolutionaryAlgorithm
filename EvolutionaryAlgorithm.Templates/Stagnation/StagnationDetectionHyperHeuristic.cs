using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;

namespace EvolutionaryAlgorithm.Template.Stagnation
{
    public static class StagnationDetectionExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingStagnationDetection(this IBitEvolutionaryAlgorithm algorithm,
            int initialStagnantLearningRate) =>
            algorithm.UsingHyperHeuristic(new StagnationDetectionHyperHeuristic(initialStagnantLearningRate));
    }

    public class StagnationDetectionHyperHeuristic : HyperHeuristicBase<IBitIndividual, BitArray, bool>,
        IBitHyperHeuristic
    {
        private int _u;
        private readonly int _initialStagnantLearningRate;
        private bool _inStagnationDetection;
        private readonly IGenerationGenerator<IBitIndividual, BitArray, bool> _mutationModule;
        private readonly StagnationDetectorGenerationGenerator _sdModule;
        private IParameters _parameters;
        private int _n;

        public StagnationDetectionHyperHeuristic(int initialStagnantLearningRate)
        {
            _sdModule = new StagnationDetectorGenerationGenerator();
            States.Add(_sdModule);

            _mutationModule = new SelfAdjustingOneLambda.SelfAdjustingOneLambdaGenerationGenerator();
            States.Add(_mutationModule);

            _initialStagnantLearningRate = initialStagnantLearningRate;
        }

        public override void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _n = _parameters.GeneCount;

            _u = 0;
            _inStagnationDetection = false;
            _parameters.MutationRate = _initialStagnantLearningRate;
            base.Initialize();
        }

        private bool IsOverLimit() => _u > 2
            * Math.Pow(Math.E * _parameters.GeneCount / _parameters.MutationRate, _parameters.MutationRate)
            * Math.Log(_parameters.GeneCount * _parameters.GeneCount)
            / _parameters.Lambda;

        private void UpdateInStagnation()
        {
            if (Algorithm.Statistics.StagnantGeneration == 0)
            {
                _u = 0;
                _inStagnationDetection = false;
                _parameters.MutationRate = _initialStagnantLearningRate;
            }
            else if (IsOverLimit())
            {
                _parameters.MutationRate = Math.Min(_parameters.MutationRate + 1, _n / 2);
                _u = 0;
            }
        }

        private void UpdateNormal()
        {
            if (Algorithm.Statistics.StagnantGeneration == 0)
                _u = 0;

            var isOverLimit = IsOverLimit();
            _parameters.MutationRate = Math.Min(_n / 4, Math.Max(2, _parameters.MutationRate));

            if (isOverLimit)
            {
                _parameters.MutationRate = 2;
                _inStagnationDetection = true;
                _u = 0;
            }
        }

        public override void Update()
        {
            if (_inStagnationDetection)
            {
                _sdModule.Update();
                UpdateInStagnation();
            }
            else
            {
                _mutationModule.Update();
                UpdateNormal();
            }
        }

        public override Task<List<IBitIndividual>> Generate() => _inStagnationDetection
            ? _sdModule.Generate()
            : _mutationModule.Generate();
    }
}
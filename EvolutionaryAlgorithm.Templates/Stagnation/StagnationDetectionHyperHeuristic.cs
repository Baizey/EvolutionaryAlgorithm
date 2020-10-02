using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;

namespace EvolutionaryAlgorithm.Template.Stagnation
{
    public class StagnationDetectionHyperHeuristic : HyperHeuristicBase<IBitIndividual, BitArray, bool>,
        IBitHyperHeuristic<IBitIndividual>
    {
        private long _u;
        private readonly int _initialMutationRate;
        public bool InStagnationDetection { get; private set; }

        private readonly IGenerationGenerator<IBitIndividual, BitArray, bool> _mutationModule;
        private readonly StagnationDetectorGenerationGenerator _sdModule;
        private IParameters _parameters;
        private int _n;
        private double _staticLimit;

        public StagnationDetectionHyperHeuristic(int initialMutationRate)
        {
            _sdModule = new StagnationDetectorGenerationGenerator();
            States.Add(_sdModule);

            _mutationModule = new SelfAdjustingOneLambda.SelfAdjustingOneLambdaGenerationGenerator();
            States.Add(_mutationModule);

            _initialMutationRate = initialMutationRate;
        }

        public override void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _n = _parameters.GeneCount;

            _u = 0;
            InStagnationDetection = false;
            _parameters.MutationRate = _initialMutationRate;

            base.Initialize();

            _staticLimit = 2D * Math.Log(_parameters.GeneCount) / _parameters.Lambda;
        }

        public double At => _u;

        public double Limit =>
            _staticLimit * Math.Pow(Math.E * _n / _parameters.MutationRate, _parameters.MutationRate);

        private bool IsOverLimit() => _u > Limit;

        private void UpdateInStagnation()
        {
            if (Algorithm.Statistics.ImprovedFitness)
            {
                _u = 0;
                InStagnationDetection = false;
                _parameters.MutationRate = _initialMutationRate;
            }
            else if (IsOverLimit())
            {
                _parameters.MutationRate = Math.Min(_parameters.MutationRate + 1, _parameters.GeneCount / 2);
                _u = 0;
            }
        }

        private void UpdateNormal()
        {
            if (Algorithm.Statistics.ImprovedFitness)
            {
                _u = 0;
                _parameters.MutationRate = Math.Min(_n / 4, Math.Max(2, _parameters.MutationRate));
            }
            else
            {
                var isOverLimit = IsOverLimit();
                _parameters.MutationRate = Math.Min(_n / 4, Math.Max(2, _parameters.MutationRate));

                if (!isOverLimit) return;

                _parameters.MutationRate = 2;
                InStagnationDetection = true;
                _u = 0;
            }
        }

        public override void Update()
        {
            if (InStagnationDetection)
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

        public override Task<List<IBitIndividual>> Generate()
        {
            _u++;
            return InStagnationDetection
                ? _sdModule.Generate()
                : _mutationModule.Generate();
        }
    }
}
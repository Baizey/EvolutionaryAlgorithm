using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Template.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Mutations
{
    public class StagnationDetectionHyperHeuristic : BitHyperHeuristicBase<IBitIndividual>
    {
        private long _u;
        private readonly double _initialMutationRate;
        public bool InStagnationDetection { get; private set; }

        private readonly IBitGenerationGenerator<IBitIndividual> _mutationModule;
        private readonly IBitGenerationGenerator<IBitIndividual> _sdModule;
        private IParameters _parameters;
        private int _n;
        private double _staticLimit;
        private readonly int _limitFactor;

        public StagnationDetectionHyperHeuristic(double initialMutationRate, int limitFactor,
            IBitGenerationGenerator<IBitIndividual> generator)
        {
            _limitFactor = limitFactor;
            _sdModule = new BitGenerationGenerator<IBitIndividual>
            {
                Mutator = new BitMutator<IBitIndividual>()
                    .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                    .ThenApplyMutation(new StagnationDetectionMutation()),
                Filter = new BitElitismGenerationFilter<IBitIndividual>(false)
            };
            _mutationModule = generator;

            States.Add(_sdModule);
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

            _staticLimit = 2D * Math.Log(_limitFactor * _parameters.GeneCount) / _parameters.Lambda;
        }

        public double At => _u;

        public double Limit =>
            _staticLimit * Math.Pow(Math.E * _n / _parameters.MutationRate, _parameters.MutationRate);

        private bool IsOverLimit() => _u > Limit;

        public override void Update()
        {
            var overLimit = IsOverLimit();
            if (InStagnationDetection)
            {
                _sdModule.Update();
                if (Algorithm.Statistics.ImprovedFitness)
                {
                    _u = 0;
                    InStagnationDetection = false;
                    _parameters.MutationRate = _initialMutationRate;
                }
                else if (overLimit)
                {
                    _parameters.MutationRate = Math.Min(_parameters.MutationRate + 1, _n / 2);
                    _u = 0;
                }
            }

            else
            {
                _mutationModule.Update();
                if (Algorithm.Statistics.ImprovedFitness)
                {
                    _u = 0;
                }
                else if (overLimit)
                {
                    _parameters.MutationRate = 2;
                    InStagnationDetection = true;
                    _u = 0;
                }
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
using System;
using System.Collections;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.Bit.Algorithm;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;
using EvolutionaryAlgorithm.Template.Basics.Selection;

namespace EvolutionaryAlgorithm.Template.OneLambdaLambda
{
    public class OneLambdaLambdaGenerationGenerator : GenerationGenerator<IBitIndividual, BitArray, bool>
    {
        private IParameters _parameters;
        private IEvolutionaryStatistics<IBitIndividual, BitArray, bool> _statistics;
        private readonly double _shrinkRate, _growthRate;

        public OneLambdaLambdaGenerationGenerator(int learningRate)
        {
            _shrinkRate = learningRate;
            _growthRate = Math.Pow(learningRate, 0.25D);

            Mutator = new BitMutator()
                .CloneGenesFrom(new FirstParentSelector<IBitIndividual, BitArray, bool>())
                .ThenApply(new CrossoverPhase());
            Filter = new ElitismGenerationFilter<IBitIndividual>(true);
        }

        public override void Initialize()
        {
            // Mu is always 1 for this implementation
            _parameters.Mu = 1;

            // Self adapting parameter, initializes to 1
            _parameters.Lambda = 1;

            _parameters = Algorithm.Parameters;
            _statistics = Algorithm.Statistics;

            base.Initialize();
        }

        public override void Update()
        {
            if (_statistics.ImprovedFitness)
                _parameters.Lambda = (int) Math.Max(_parameters.Lambda / _shrinkRate, 1);
            else
                _parameters.Lambda = (int) Math.Min(_parameters.Lambda * _growthRate, _parameters.GeneCount);
            base.Update();
        }
    }
}
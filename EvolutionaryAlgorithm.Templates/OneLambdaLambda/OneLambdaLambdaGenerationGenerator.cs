using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Statistics;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.OneLambdaLambda
{
    public class OneLambdaLambdaGenerationGenerator : BitGenerationGenerator<IBitIndividual>
    {
        private IParameters _parameters;
        private IEvolutionaryStatistics<IBitIndividual, BitArray, bool> _statistics;
        private readonly double _shrinkRate, _growthRate;

        public OneLambdaLambdaGenerationGenerator(int learningRate, double c)
        {
            _shrinkRate = learningRate;
            _growthRate = Math.Pow(learningRate, 0.25D);

            Mutator = new BitMutator<IBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                .ThenApply(new CrossoverPhase(c));
            Filter = new BitElitismGenerationFilter<IBitIndividual>(true);
        }

        public override void Initialize()
        {
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
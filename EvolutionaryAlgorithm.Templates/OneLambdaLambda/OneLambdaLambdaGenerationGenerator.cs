using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Statistics;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.OneLambdaLambda
{
    public class OneLambdaLambdaGenerationGenerator : BitGenerationGenerator<IEndogenousBitIndividual>
    {
        private IParameters _parameters;
        private IEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool> _statistics;
        private readonly double _shrinkRate, _growthRate;
        
        // Keep a double-lambda so we can keep track of small changes between eventual increases
        private double _actualLambda;

        public OneLambdaLambdaGenerationGenerator(int learningRate, double c)
        {
            _shrinkRate = learningRate;
            _growthRate = Math.Pow(learningRate, 0.25D);

            Mutator = new BitMutator<IEndogenousBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IEndogenousBitIndividual>())
                .ThenApply(new CrossoverPhase(c));
            Filter = new BitElitismGenerationFilter<IEndogenousBitIndividual>(true);
        }

        public override void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _statistics = Algorithm.Statistics;
            _actualLambda = _parameters.Lambda;
            base.Initialize();
        }

        public override void Update()
        {
            _actualLambda = _statistics.ImprovedFitness
                ? Math.Max(_actualLambda / _shrinkRate, 1)
                : Math.Min(_actualLambda * _growthRate, _parameters.GeneCount);
            _parameters.Lambda = (int) _actualLambda;
            base.Update();
        }
    }
}
﻿using System;
using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Algorithm.Mutator;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Endogenous
{
    public class EndogenousGenerationGenerator
        : GenerationGenerator<IEndogenousBitIndividual, BitArray, bool>
    {
        private readonly int _learningRate;
        private int _maxRate;

        public EndogenousGenerationGenerator(int learningRate)
        {
            Mutator = new Mutator<IEndogenousBitIndividual, BitArray, bool>()
                .CloneGenesFrom(new FirstParentSelector<IEndogenousBitIndividual, BitArray, bool>())
                .ThenApply(new EndogenousMutation(learningRate));
            Filter = new EndogenousGenerationFilter();
            _learningRate = learningRate;
        }

        public new void Initialize()
        {
            var g = Algorithm.Parameters.GeneCount;
            _maxRate = (int) Math.Pow(_learningRate, Math.Log(g / (2D * _learningRate), _learningRate));
            base.Initialize();
        }

        public new void Update()
        {
            var individual = Algorithm.Population[0];
            var r = individual.MutationRate;
            individual.MutationRate = Math.Min(Math.Max(_learningRate, r), _maxRate);
        }
    }
}
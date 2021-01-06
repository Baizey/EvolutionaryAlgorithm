using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Crossover;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;

namespace EvolutionaryAlgorithm.BitImplementation.Templates
{
    public class BitMiddlePointCrossover<TIndividual>
        : MultiParentCrossoverBase<TIndividual, BitArray, bool>
        where TIndividual : IBitIndividual
    {
        private BitArray _left, _right;
        private readonly Random _random = new Random();

        public BitMiddlePointCrossover(IMultiParentSelector<TIndividual, BitArray, bool> parentsSelector)
            : base(parentsSelector)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            _left = ((TIndividual) Algorithm.Population[0].Clone()).Genes;

            _left.SetAll(false);
            for (var i = 0; i < _left.Count / 2; i++)
                _left[i] = true;

            _right = new BitArray(_left.Count)
                .And(_left)
                .Not();
        }

        public override void Crossover(int index, TIndividual child, List<TIndividual> parents)
        {
            child.MutationRate = parents[0].MutationRate;

            child.Genes.SetAll(false);

            child.Genes.And(_left);
            child.Genes.And(parents[0].Genes);

            var clone = (BitArray) _right.Clone();
            clone.And(parents[1].Genes);
            child.Genes.Or(clone);
        }
    }
}
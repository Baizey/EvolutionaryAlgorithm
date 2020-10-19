using System.Collections;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Crossover;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;

namespace EvolutionaryAlgorithm.BitImplementation.Templates
{
    public class BitMiddlePointCrossover<TIndividual>
        : MultiParentCrossoverBase<TIndividual, BitArray, bool>
        where TIndividual : IBitIndividual
    {
        private BitArray _left, _right;

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

        public override void Crossover(TIndividual child, List<TIndividual> parents)
        {
            child.Genes.SetAll(false);

            child.Genes.And(_left);
            child.Genes.And(parents[0].Genes);

            var clone = (BitArray) _right.Clone();
            clone.And(parents[1].Genes);
            child.Genes.Or(clone);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Crossover;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;

namespace EvolutionaryAlgorithm.BitImplementation.Templates
{
    public class BitMiddlePointCrossover<TIndividual>
        : MultiParentCrossoverBase<TIndividual, bool[], bool>
        where TIndividual : IBitIndividual
    {
        public BitMiddlePointCrossover(IMultiParentSelector<TIndividual, bool[], bool> parentsSelector)
            : base(parentsSelector)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Crossover(TIndividual child, List<TIndividual> parents)
        {
            child.MutationRate = parents[0].MutationRate;
            var split = child.Count / 2;
            Array.Copy(parents[0].Genes, 0, child.Genes, 0, split);
            Array.Copy(parents[1].Genes, split, child.Genes, 0, child.Count - split);
        }
    }
}
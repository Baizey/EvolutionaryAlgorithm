using System;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Crossover
{
    public class CloneParent<TIndividual, TGeneStructure, TGene>
        : SingleParentCrossoverBase<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public CloneParent(ISingleParentSelector<TIndividual, TGeneStructure, TGene> parentsSelector)
            : base(parentsSelector)
        {
        }

        public override void Crossover(int index, TIndividual child, TIndividual parent) => parent.CopyTo(child);
    }
}
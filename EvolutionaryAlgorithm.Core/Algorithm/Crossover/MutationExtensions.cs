using System;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Crossover
{
    public static class MutationExtensions
    {
        public static IMutator<TIndividual, TGeneStructure, TGene> CloneGenesFrom<TIndividual, TGeneStructure,
            TGene>(
            this IMutator<TIndividual, TGeneStructure, TGene> mutator,
            IParentSelector<TIndividual, TGeneStructure, TGene> parentSelector)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable =>
            mutator.ThenApply(new CloneParent<TIndividual, TGeneStructure, TGene>(parentSelector));
    }
}
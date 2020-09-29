using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase.Helpers;

namespace EvolutionaryAlgorithm.Core.Algorithm.Crossover
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

        public override async Task Crossover(int index, TIndividual child, TIndividual parent) => parent.CloneGenesTo(child);
    }
}
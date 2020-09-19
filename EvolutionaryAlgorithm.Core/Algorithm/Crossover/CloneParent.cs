using System;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Crossover
{
    public class CloneParent<TIndividual, TGeneStructure, TGene>
        : SingleParentCrossoverBase<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public CloneParent(IParentSelector<TIndividual, TGeneStructure, TGene> parentsSelector) : base(parentsSelector)
        {
        }

        public override void Crossover(TIndividual child, TIndividual parent) => parent.CloneGenesTo(child);
    }
}
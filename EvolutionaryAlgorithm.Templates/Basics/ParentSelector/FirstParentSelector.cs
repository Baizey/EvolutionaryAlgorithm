using System;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Template.Basics.ParentSelector
{
    public class
        FirstParentSelector<TIndividual, TGeneStructure, TGene> : ISingleParentSelector<TIndividual, TGeneStructure,
            TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public TIndividual Select(int index) =>
            Algorithm.Population[0];
    }
}
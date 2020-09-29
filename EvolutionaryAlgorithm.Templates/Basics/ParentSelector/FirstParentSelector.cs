using System;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase.Helpers;

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

        public TIndividual Select(int index, IPopulation<TIndividual, TGeneStructure, TGene> population) =>
            population[0];
    }
}
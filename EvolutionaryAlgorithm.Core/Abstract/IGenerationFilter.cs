using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IGenerationFilter<TIndividual, TGeneStructure, TGene> 
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        IPopulation<TIndividual, TGeneStructure, TGene> Filter(
            IPopulation<TIndividual, TGeneStructure, TGene> population,
            List<TIndividual> newcomers);
    }
}
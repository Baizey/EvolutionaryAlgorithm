using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IGenerationFilter<TGeneStructure, TGene> where TGeneStructure : ICloneable
    {
        public IEvolutionaryAlgorithm<TGeneStructure, TGene> Algorithm { get; set; }
        IPopulation<TGeneStructure, TGene> Filter(IPopulation<TGeneStructure, TGene> population, List<IIndividual<TGeneStructure, TGene>> newcomers);
    }
}
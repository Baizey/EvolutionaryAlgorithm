using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IGenerationFilter<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        GenerationFilterResult<TIndividual, TGeneStructure, TGene> Filter(
            IPopulation<TIndividual, TGeneStructure, TGene> oldIndividuals,
            List<TIndividual> newIndividuals);
    }

    public class GenerationFilterResult<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<TIndividual> NextGeneration { get; set; }
        public List<TIndividual> Discarded { get; set; }
    }
}
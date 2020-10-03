using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.GenerationFilter
{
    public class GenerationFilterResult<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<TIndividual> NextGeneration { get; set; }
        public List<TIndividual> Discarded { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IMultiParentSelector<TIndividual, TGeneStructure, TGene> 
        : IInitialization<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<TIndividual> Select(IPopulation<TIndividual, TGeneStructure, TGene> population);
    }
}
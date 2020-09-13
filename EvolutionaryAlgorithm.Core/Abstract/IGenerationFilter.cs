using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IGenerationFilter<TDataStructure, TGene> where TDataStructure : ICloneable
    {
        IPopulation<TDataStructure, TGene> Filter(IPopulation<TDataStructure, TGene> population, List<IIndividual<TDataStructure, TGene>> newcomers);
    }
}
using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IParentSelector<TGeneStructure, TGene> where TGeneStructure : ICloneable
    {
        public IIndividual<TGeneStructure, TGene> Select(IPopulation<TGeneStructure, TGene> population);
    }
}
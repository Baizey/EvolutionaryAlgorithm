using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IPopulation<TGeneStructure, TGene> :
        ICloneable,
        IEnumerable<IIndividual<TGeneStructure, TGene>>
        where TGeneStructure : ICloneable
    {
        public int Count { get; }
        public List<IIndividual<TGeneStructure, TGene>> Individuals { get; set; }
        public IIndividual<TGeneStructure, TGene> Best { get; }

        public IIndividual<TGeneStructure, TGene> this[int i]
        {
            get => Individuals[i];
            set => Individuals[i] = value;
        }
    }
}
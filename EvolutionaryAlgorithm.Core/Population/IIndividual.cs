using System;
using EvolutionaryAlgorithm.Core.Infrastructure;

namespace EvolutionaryAlgorithm.Core.Population
{
    public interface IIndividual<TGeneStructure, TGene> :
        ICopyTo<IIndividual<TGeneStructure, TGene>>,
        ICloneable,
        IComparable<IIndividual<TGeneStructure, TGene>>
        where TGeneStructure : ICloneable
    {
        public double Fitness { get; set; }
        public TGeneStructure Genes { get; set; }
        public int Count { get; }
    }
}
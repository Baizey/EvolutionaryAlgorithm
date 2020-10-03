using System;

namespace EvolutionaryAlgorithm.Core.Population
{
    public interface IIndividual<TGeneStructure, TGene> :
        ICloneable,
        IComparable<IIndividual<TGeneStructure, TGene>>
        where TGeneStructure : ICloneable
    {
        public int Size { get; }
        public double Fitness { get; set; }
        public TGeneStructure Genes { get; set; }
        public void CloneGenesTo(IIndividual<TGeneStructure, TGene> other);
        public void Reset();
    }
}
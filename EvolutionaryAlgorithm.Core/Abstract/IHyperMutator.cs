using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IHyperMutator<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<IMutator<TIndividual, TGeneStructure, TGene>> States { get; set; }
        public abstract Task Mutate(List<TIndividual> newIndividuals);
        
        public IMutator<TIndividual, TGeneStructure, TGene> this[int i]
        {
            get => States[i];
            set => States[i] = value;
        }
    }
}
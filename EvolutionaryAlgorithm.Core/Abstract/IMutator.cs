using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IMutator<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        List<IMutation<TIndividual, TGeneStructure, TGene>> Mutations { get; set; }
        IMutator<TIndividual, TGeneStructure, TGene> ThenApply(IMutation<TIndividual, TGeneStructure, TGene> mutation);
        Task Mutate(IPopulation<TIndividual, TGeneStructure, TGene> oldIndividuals, List<TIndividual> newIndividuals);
    }
}
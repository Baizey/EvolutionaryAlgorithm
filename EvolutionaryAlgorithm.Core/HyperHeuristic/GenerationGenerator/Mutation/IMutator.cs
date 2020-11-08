using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Infrastructure;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation
{
    public interface IMutator<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<IMutation<TIndividual, TGeneStructure, TGene>> Mutations { get; }
        public List<IParameterAdjuster<TIndividual, TGeneStructure, TGene>> ParameterAdjusters { get; }

        public IMutator<TIndividual, TGeneStructure, TGene> ThenApply(
            IMutation<TIndividual, TGeneStructure, TGene> mutation);

        public IMutator<TIndividual, TGeneStructure, TGene> ThenAfterGeneratingApply(
            IParameterAdjuster<TIndividual, TGeneStructure, TGene> mutation);

        public Task Mutate(List<TIndividual> newIndividuals);
    }
}
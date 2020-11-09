using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Crossover;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;
using EvolutionaryAlgorithm.Core.Infrastructure;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation
{
    public interface IMutator<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IMutation<TIndividual, TGeneStructure, TGene> Crossover { get; }
        public List<IMutation<TIndividual, TGeneStructure, TGene>> Mutations { get; }
        public List<IParameterAdjuster<TIndividual, TGeneStructure, TGene>> ParameterAdjusters { get; }


        public IMutator<TIndividual, TGeneStructure, TGene> CloneGenesFrom(
            ISingleParentSelector<TIndividual, TGeneStructure, TGene> parentSelector);

        public IMutator<TIndividual, TGeneStructure, TGene> CloneGenesFrom(
            SingleParentCrossoverBase<TIndividual, TGeneStructure, TGene> parentSelector);

        public IMutator<TIndividual, TGeneStructure, TGene> CrossoverGenesFrom(
            MultiParentCrossoverBase<TIndividual, TGeneStructure, TGene> parentsSelector);

        public IMutator<TIndividual, TGeneStructure, TGene> ThenApplyMutation(
            IMutation<TIndividual, TGeneStructure, TGene> mutation);

        public IMutator<TIndividual, TGeneStructure, TGene> AdjustParameterUsing(
            IParameterAdjuster<TIndividual, TGeneStructure, TGene> adjuster);

        public Task Mutate(List<TIndividual> newIndividuals);
    }
}
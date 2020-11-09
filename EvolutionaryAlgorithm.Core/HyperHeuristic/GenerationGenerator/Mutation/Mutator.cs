using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Crossover;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation
{
    public class Mutator<TIndividual, TGeneStructure, TGene>
        : IMutator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IMutation<TIndividual, TGeneStructure, TGene> Crossover { get; private set; }

        public List<IMutation<TIndividual, TGeneStructure, TGene>> Mutations { get; } =
            new List<IMutation<TIndividual, TGeneStructure, TGene>>();

        public List<IParameterAdjuster<TIndividual, TGeneStructure, TGene>> ParameterAdjusters { get; } =
            new List<IParameterAdjuster<TIndividual, TGeneStructure, TGene>>();

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public void Initialize()
        {
            ParameterAdjusters.ForEach(mutation =>
            {
                mutation.Algorithm = Algorithm;
                mutation.Initialize();
            });
            Crossover.Algorithm = Algorithm;
            Crossover.Initialize();
            Mutations.ForEach(mutation =>
            {
                mutation.Algorithm = Algorithm;
                mutation.Initialize();
            });
        }

        public void Update()
        {
            ParameterAdjusters.ForEach(m => m.Update());
            Crossover.Update();
            Mutations.ForEach(mutation => mutation.Update());
        }

        public IMutator<TIndividual, TGeneStructure, TGene> CloneGenesFrom(
            ISingleParentSelector<TIndividual, TGeneStructure, TGene> parentSelector) =>
            CloneGenesFrom(new CloneParent<TIndividual, TGeneStructure, TGene>(parentSelector));

        public IMutator<TIndividual, TGeneStructure, TGene> CloneGenesFrom(
            SingleParentCrossoverBase<TIndividual, TGeneStructure, TGene> parentSelector)
        {
            Crossover = parentSelector;
            return this;
        }

        public IMutator<TIndividual, TGeneStructure, TGene> CrossoverGenesFrom(
            MultiParentCrossoverBase<TIndividual, TGeneStructure, TGene> parentsSelector)
        {
            Crossover = parentsSelector;
            return this;
        }

        public IMutator<TIndividual, TGeneStructure, TGene> ThenApplyMutation(
            IMutation<TIndividual, TGeneStructure, TGene> mutation)
        {
            Mutations.Add(mutation);
            return this;
        }

        public IMutator<TIndividual, TGeneStructure, TGene> AdjustParameterUsing(
            IParameterAdjuster<TIndividual, TGeneStructure, TGene> adjuster)
        {
            ParameterAdjusters.Add(adjuster);
            return this;
        }

        public virtual Task Mutate(List<TIndividual> newIndividuals)
        {
            for (var i = 0; i < newIndividuals.Count; i++)
            {
                Crossover.Mutate(i, newIndividuals[i]);
                foreach (var adjuster in ParameterAdjusters)
                    adjuster.Mutate(i, newIndividuals[i]);
                foreach (var t in Mutations)
                    t.Mutate(i, newIndividuals[i]);
            }

            return Task.CompletedTask;
        }
    }
}
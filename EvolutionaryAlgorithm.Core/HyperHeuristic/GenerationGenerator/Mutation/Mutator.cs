using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation
{
    public class Mutator<TIndividual, TGeneStructure, TGene>
        : IMutator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
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
            Mutations.ForEach(mutation =>
            {
                mutation.Algorithm = Algorithm;
                mutation.Initialize();
            });
        }

        public void Update()
        {
            ParameterAdjusters.ForEach(m => m.Update());
            Mutations.ForEach(mutation => mutation.Update());
        }

        public IMutator<TIndividual, TGeneStructure, TGene> ThenApply(
            IMutation<TIndividual, TGeneStructure, TGene> mutation)
        {
            Mutations.Add(mutation);
            return this;
        }

        public IMutator<TIndividual, TGeneStructure, TGene> ThenAfterGeneratingApply(
            IParameterAdjuster<TIndividual, TGeneStructure, TGene> mutation)
        {
            ParameterAdjusters.Add(mutation);
            return this;
        }

        public virtual Task Mutate(List<TIndividual> newIndividuals)
        {
            for (var i = 0; i < newIndividuals.Count; i++)
            {
                foreach (var t in Mutations)
                    t.Mutate(i, newIndividuals[i]);
            }

            return Task.CompletedTask;
        }
    }
}
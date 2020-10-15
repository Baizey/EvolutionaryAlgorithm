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
        public List<IMutation<TIndividual, TGeneStructure, TGene>> Mutations { get; set; } =
            new List<IMutation<TIndividual, TGeneStructure, TGene>>();

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public void Initialize() => Mutations.ForEach(mutation =>
        {
            mutation.Algorithm = Algorithm;
            mutation.Initialize();
        });

        public void Update() => Mutations.ForEach(mutation => mutation.Update());

        public IMutator<TIndividual, TGeneStructure, TGene> ThenApply(
            IMutation<TIndividual, TGeneStructure, TGene> mutation)
        {
            Mutations.Add(mutation);
            return this;
        }

        public virtual Task Mutate(List<TIndividual> newIndividuals)
        {
            for (var i = 0; i < newIndividuals.Count; i++)
            {
                newIndividuals[i].Reset();
                foreach (var t in Mutations)
                    t.Mutate(i, newIndividuals[i]);
            }

            return Task.CompletedTask;
        }
    }
}
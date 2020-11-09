using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dasync.Collections;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation
{
    public class MutatorAsync<TIndividual, TGeneStructure, TGene>
        : Mutator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public override async Task Mutate(List<TIndividual> newIndividuals) =>
            await Enumerable.Range(0, newIndividuals.Count).ParallelForEachAsync(async i =>
            {
                foreach (var t in ParameterAdjusters)
                    t.Mutate(i, newIndividuals[i]);
                foreach (var t in Mutations)
                    t.Mutate(i, newIndividuals[i]);
            });
    }
}
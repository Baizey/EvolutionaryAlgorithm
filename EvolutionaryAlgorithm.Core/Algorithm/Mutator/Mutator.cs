using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dasync.Collections;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;

namespace EvolutionaryAlgorithm.Core.Algorithm.Mutator
{
    public class Mutator<TIndividual, TGeneStructure, TGene>
        : MutatorBase<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public override async Task Mutate(List<TIndividual> newIndividuals)
        {
            await Enumerable.Range(0, newIndividuals.Count).ParallelForEachAsync(async i =>
            {
                newIndividuals[i].Reset();
                foreach (var t in Mutations)
                    await t.Mutate(i, newIndividuals[i]);
            });
        }
    }
}
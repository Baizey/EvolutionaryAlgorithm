using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;

namespace EvolutionaryAlgorithm.Core.Abstract.MutationPhase
{
    public interface IMutator<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<IMutation<TIndividual, TGeneStructure, TGene>> Mutations { get; set; }

        public MutatorBase<TIndividual, TGeneStructure, TGene> ThenApply(
            IMutation<TIndividual, TGeneStructure, TGene> mutation);

        public Task Mutate(List<TIndividual> newIndividuals);
    }

    public abstract class MutatorBase<TIndividual, TGeneStructure, TGene>
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

        public MutatorBase<TIndividual, TGeneStructure, TGene> ThenApply(
            IMutation<TIndividual, TGeneStructure, TGene> mutation)
        {
            Mutations.Add(mutation);
            return this;
        }

        public abstract Task Mutate(List<TIndividual> newIndividuals);
    }
}
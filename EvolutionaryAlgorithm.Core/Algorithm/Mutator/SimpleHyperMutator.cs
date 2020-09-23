using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Mutator
{
    public class SimpleHyperMutator<TIndividual, TGeneStructure, TGene>
        : IHyperMutator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        public List<IMutator<TIndividual, TGeneStructure, TGene>> States { get; set; }

        public SimpleHyperMutator(IMutator<TIndividual, TGeneStructure, TGene> mutator) => States.Add(mutator);
        public void Initialize() => States.ForEach(mutation => mutation.Initialize());
        public void Update() => States.ForEach(mutation => mutation.Update());
        public async Task Mutate(List<TIndividual> newIndividuals) => await States[0].Mutate(newIndividuals);
    }
}
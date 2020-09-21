using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class Mutator<TIndividual, TGeneStructure, TGene> : IMutator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> _algorithm;

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm
        {
            get => _algorithm;
            set
            {
                _algorithm = value;
                Mutations.ForEach(step => step.Algorithm = Algorithm);
            }
        }

        public void Initialize() => Mutations.ForEach(mutation => mutation.Initialize());

        public void Update() => Mutations.ForEach(mutation => mutation.Update());

        public List<IMutation<TIndividual, TGeneStructure, TGene>> Mutations { get; set; } =
            new List<IMutation<TIndividual, TGeneStructure, TGene>>();

        public IMutator<TIndividual, TGeneStructure, TGene> ThenApply(
            IMutation<TIndividual, TGeneStructure, TGene> mutation)
        {
            Mutations.Add(mutation);
            mutation.Algorithm = Algorithm;
            return this;
        }

        public async Task Mutate(IPopulation<TIndividual, TGeneStructure, TGene> oldIndividuals,
            List<TIndividual> newIndividuals)
        {
            for (var i = 0; i < newIndividuals.Count; i++)
            {
                newIndividuals[i].Reset();
                foreach (var t in Mutations)
                    t.Mutate(i, newIndividuals[i]);
            }
        }
    }
}
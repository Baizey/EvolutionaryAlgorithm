using System;
using System.Collections.Generic;
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
                if (InitialSelector != null)
                    InitialSelector.Algorithm = Algorithm;
                Mutations.ForEach(step => step.Algorithm = Algorithm);
            }
        }

        public void Initialize()
        {
            Mutations.ForEach(mutation => mutation.Initialize());
        }

        public IParentSelector<TIndividual, TGeneStructure, TGene> InitialSelector { get; set; }

        public List<IMutation<TIndividual, TGeneStructure, TGene>> Mutations { get; set; } =
            new List<IMutation<TIndividual, TGeneStructure, TGene>>();

        public Mutator(IParentSelector<TIndividual, TGeneStructure, TGene> initialSelector = null)
        {
            InitialSelector = initialSelector;
            if (InitialSelector != null)
                InitialSelector.Algorithm = _algorithm;
        }

        public IMutator<TIndividual, TGeneStructure, TGene> ThenApply(
            IMutation<TIndividual, TGeneStructure, TGene> mutation)
        {
            Mutations.Add(mutation);
            mutation.Algorithm = _algorithm;
            return this;
        }

        public void Mutate(IPopulation<TIndividual, TGeneStructure, TGene> oldIndividuals,
            List<TIndividual> newIndividuals)
        {
            newIndividuals.ForEach(body =>
            {
                InitialSelector?.Select(oldIndividuals.Individuals).CloneGenesTo(body);
                Mutations.ForEach(step => step.Mutate(body));
            });
        }
    }
}
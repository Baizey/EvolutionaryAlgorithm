using System;
using System.Collections.Generic;
using System.Linq;
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
                    InitialSelector.Algorithm = _algorithm;
                Mutations.ForEach(step => step.Algorithm = _algorithm);
            }
        }

        public List<TIndividual> Reserves { get; set; }
        public IParentSelector<TIndividual, TGeneStructure, TGene> InitialSelector { get; set; }

        public List<IMutation<TIndividual, TGeneStructure, TGene>> Mutations { get; set; } =
            new List<IMutation<TIndividual, TGeneStructure, TGene>>();

        public Mutator(int size, IParentSelector<TIndividual, TGeneStructure, TGene> initialSelector = null)
        {
            Reserves = Enumerable.Range(0, size)
                .Select(_ => Activator.CreateInstance<TIndividual>())
                .ToList();
            InitialSelector = initialSelector;
            if (initialSelector != null)
                InitialSelector.Algorithm = _algorithm;
        }

        public IMutator<TIndividual, TGeneStructure, TGene> Then(
            IMutation<TIndividual, TGeneStructure, TGene> mutation)
        {
            Mutations.Add(mutation);
            mutation.Algorithm = _algorithm;
            return this;
        }

        public List<TIndividual> GenerateNextGeneration(
            IPopulation<TIndividual, TGeneStructure, TGene> population)
        {
            Reserves.ForEach(reserve =>
            {
                InitialSelector?.Select(population).CloneGenesTo(reserve);
                Mutations.ForEach(step => step.Mutate(reserve));
            });

            return Reserves;
        }
    }
}
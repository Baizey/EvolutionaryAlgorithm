using System;
using System.Collections.Generic;
using System.Drawing;
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
                MutationSteps.ForEach(step =>
                {
                    step.Mutation.Algorithm = _algorithm;
                    if (step.ParentSelector != null)
                        step.ParentSelector.Algorithm = _algorithm;
                });
            }
        }

        public List<TIndividual> Reserves { get; set; }
        public IParentSelector<TIndividual, TGeneStructure, TGene> InitialSelector { get; set; }

        public List<MutationStep<TIndividual, TGeneStructure, TGene>> MutationSteps { get; set; } =
            new List<MutationStep<TIndividual, TGeneStructure, TGene>>();

        public Mutator(int size, IParentSelector<TIndividual, TGeneStructure, TGene> initialSelector)
        {
            Reserves = Enumerable.Range(0, size)
                .Select(_ => Activator.CreateInstance<TIndividual>())
                .ToList();
            InitialSelector = initialSelector;
        }

        public IMutator<TIndividual, TGeneStructure, TGene> Then(
            IMutation<TIndividual, TGeneStructure, TGene> mutation,
            IParentSelector<TIndividual, TGeneStructure, TGene> parentSelector = null)
        {
            MutationSteps.Add(new MutationStep<TIndividual, TGeneStructure, TGene>(mutation, parentSelector));
            mutation.Algorithm = _algorithm;
            if (parentSelector != null)
                parentSelector.Algorithm = _algorithm;
            return this;
        }

        private TIndividual CreateIndividual(
            IPopulation<TIndividual, TGeneStructure, TGene> population)
        {
            var individual = (TIndividual) InitialSelector.Select(population).Clone();

            foreach (var step in MutationSteps)
            {
                var parent = step.ParentSelector.Select(population);
                individual = step.Mutation.Mutate(individual, parent);
            }

            return individual;
        }

        public List<TIndividual> Create(
            IPopulation<TIndividual, TGeneStructure, TGene> population)
        {
            return Enumerable.Range(0, Reserves.Count).Select(_ => CreateIndividual(population)).ToList();
        }
    }
}
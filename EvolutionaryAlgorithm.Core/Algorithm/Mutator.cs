using System;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class Mutator<TGeneStructure, TGene> : IMutator<TGeneStructure, TGene> where TGeneStructure : ICloneable
    {
        public int NewIndividuals { get; set; }
        public IParentSelector<TGeneStructure, TGene> InitialSelector { get; set; }

        public List<MutationStep<TGeneStructure, TGene>> MutationSteps { get; set; } =
            new List<MutationStep<TGeneStructure, TGene>>();

        public Mutator(int newIndividuals, IParentSelector<TGeneStructure, TGene> initialSelector)
        {
            NewIndividuals = newIndividuals;
            InitialSelector = initialSelector;
        }

        public IMutator<TGeneStructure, TGene> Then(
            IMutation<TGeneStructure, TGene> mutation,
            IParentSelector<TGeneStructure, TGene> parentSelector = null)
        {
            MutationSteps.Add(new MutationStep<TGeneStructure, TGene>(mutation, parentSelector));
            return this;
        }

        private IIndividual<TGeneStructure, TGene> CreateIndividual(
            IPopulation<TGeneStructure, TGene> population)
        {
            var individual = (IIndividual<TGeneStructure, TGene>) InitialSelector.Select(population).Clone();

            foreach (var step in MutationSteps)
            {
                var parent = step.ParentSelector?.Select(population);
                individual = step.Mutation.Mutate(individual, parent);
            }
            
            return individual;
        }

        public List<IIndividual<TGeneStructure, TGene>> Create(IPopulation<TGeneStructure, TGene> population) =>
            Enumerable.Range(0, NewIndividuals).Select(_ => CreateIndividual(population)).ToList();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core;
using EvolutionaryAlgorithm.Core.Individual;
using EvolutionaryAlgorithm.Template.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Algorithm
{
    public class Mutator<T> : IMutator<T> where T : ICloneable
    {
        public int Λ { get; set; }
        public IParentSelector<T> InitialSelector { get; set; }
        public List<MutationStep<T>> MutationSteps { get; set; }

        public Mutator(int λ, IParentSelector<T> initialSelector)
        {
            Λ = λ;
            InitialSelector = initialSelector;
        }

        public IMutator<T> Then(
            IMutation<T> mutation,
            IParentSelector<T> parentSelector = null)
        {
            MutationSteps.Add(new MutationStep<T>(mutation, parentSelector));
            return this;
        }

        private IIndividual<T> CreateIndividual(IPopulation<T> population, IFitness<T> fitness)
        {
            var individual = (IIndividual<T>) InitialSelector.Select(population).Clone();

            foreach (var step in MutationSteps)
            {
                var parent = step.ParentSelector?.Select(population);
                individual = step.Mutation.Mutate(individual, parent);
            }

            fitness.Evaluate(individual);
            return individual;
        }

        public List<IIndividual<T>> Create(IPopulation<T> population, IFitness<T> fitness)
        {
            return Enumerable.Range(0, Λ).Select(_ => CreateIndividual(population, fitness)).ToList();
        }
    }
}
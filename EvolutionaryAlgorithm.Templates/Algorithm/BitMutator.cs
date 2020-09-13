using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Algorithm
{
    public class BitMutator : IBitMutator
    {
        public static IBitMutator CloneParent(int newIndividuals,
            IBitParentSelector initialSelector) =>
            new BitMutator(newIndividuals, initialSelector);

        public int NewIndividuals { get; set; }
        public IParentSelector<BitArray, bool> InitialSelector { get; set; }

        public List<MutationStep<BitArray, bool>> MutationSteps { get; set; } =
            new List<MutationStep<BitArray, bool>>();

        public BitMutator(int newIndividuals, IBitParentSelector initialSelector)
        {
            NewIndividuals = newIndividuals;
            InitialSelector = initialSelector;
        }

        public IMutator<BitArray, bool> Then(
            IMutation<BitArray, bool> mutation,
            IParentSelector<BitArray, bool> parentSelector = null)
        {
            MutationSteps.Add(new MutationStep<BitArray, bool>(mutation, parentSelector));
            return this;
        }

        private IIndividual<BitArray, bool> CreateIndividual(IPopulation<BitArray, bool> population,
            IFitness<BitArray, bool> fitness)
        {
            var individual = (IIndividual<BitArray, bool>) InitialSelector.Select(population).Clone();

            foreach (var step in MutationSteps)
            {
                var parent = step.ParentSelector?.Select(population);
                individual = step.Mutation.Mutate(individual, parent);
            }

            fitness.Evaluate(individual);
            return individual;
        }

        public List<IIndividual<BitArray, bool>> Create(IPopulation<BitArray, bool> population,
            IFitness<BitArray, bool> fitness) =>
            Enumerable.Range(0, NewIndividuals).Select(_ => CreateIndividual(population, fitness)).ToList();
    }
}
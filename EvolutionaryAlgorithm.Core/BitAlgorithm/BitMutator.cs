using System.Collections;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Core.BitAlgorithm
{
    public class BitMutator : Mutator<BitArray, bool>, IBitMutator
    {
        public BitMutator(int newIndividuals, IParentSelector<BitArray, bool> initialSelector)
            : base(newIndividuals, initialSelector)
        {
        }

        public IBitMutator Then(IBitMutation mutation, IBitParentSelector parentSelector = null) =>
            (IBitMutator) base.Then(mutation, parentSelector);

        public List<IIndividual<BitArray, bool>> Create(IBitPopulation population) => base.Create(population);
    }
}
using System;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Algorithm
{
    public static class MutatorExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingMutator(
            this IBitEvolutionaryAlgorithm algo,
            int newIndividuals,
            IBitParentSelector initialSelector,
            Action<BitMutator> opt)
        {
            algo.UsingMutator(new BitMutator(newIndividuals, initialSelector));
            opt.Invoke((BitMutator) algo.Mutator);
            return algo;
        }
    }
}
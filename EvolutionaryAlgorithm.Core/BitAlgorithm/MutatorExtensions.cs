using System;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Core.BitAlgorithm
{
    public static class MutatorExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingMutator(
            this IBitEvolutionaryAlgorithm algo,
            int newIndividuals,
            IBitParentSelector initialSelector,
            Action<IBitMutator> opt)
        {
            algo.UsingMutator(new BitMutator(newIndividuals, initialSelector));
            opt.Invoke((IBitMutator) algo.Mutator);
            return algo;
        }
    }
}
using System;
using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Core.BitAlgorithm
{
    public static class MutatorExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingMutator(
            this IBitEvolutionaryAlgorithm algo,
            int newIndividuals,
            IBitParentSelector initialSelector,
            Action<IMutator<IBitIndividual, BitArray, bool>> opt)
        {
            var mutator = new BitMutator(newIndividuals, initialSelector);
            algo.UsingMutator(mutator);
            opt.Invoke(mutator);
            return algo;
        }
        
        public static IBitEvolutionaryAlgorithm UsingMutator(
            this IBitEvolutionaryAlgorithm algo,
            int newIndividuals,
            Action<IMutator<IBitIndividual, BitArray, bool>> opt)
        {
            var mutator = new BitMutator(newIndividuals, null);
            algo.UsingMutator(mutator);
            opt.Invoke(mutator);
            return algo;
        }
    }
}
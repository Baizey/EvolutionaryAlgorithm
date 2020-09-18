using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions
{
    public static class MutatorExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingMutator(
            this IBitEvolutionaryAlgorithm algo,
            int newIndividuals,
            IBitParentSelector initialSelector,
            Action<IMutator<IBitIndividual, BitArray, bool>> opt)
        {
            var mutator = new BitStaticMutator(newIndividuals, initialSelector);
            algo.UsingMutator(mutator);
            opt.Invoke(mutator);
            return algo;
        }
        
        public static IBitEvolutionaryAlgorithm UsingMutator(
            this IBitEvolutionaryAlgorithm algo,
            int newIndividuals,
            Action<IMutator<IBitIndividual, BitArray, bool>> opt)
        {
            var mutator = new BitStaticMutator(newIndividuals, null);
            algo.UsingMutator(mutator);
            opt.Invoke(mutator);
            return algo;
        }
    }
}
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Mutation
{
    public static class MutationExtensions
    {
        public static IMutator<IBitIndividual, BitArray, bool>
            ThenOneMaxStaticOptimalMutation(this IMutator<IBitIndividual, BitArray, bool> mutator, int geneSize) =>
            mutator.ThenApply(new OneMaxStaticOptimalMutation(geneSize));

        public static IMutator<IBitIndividual, BitArray, bool> 
            ThenAllOnes(this IMutator<IBitIndividual, BitArray, bool> mutator) =>
            mutator.ThenApply(new AllOnes());

        public static IMutator<IBitIndividual, BitArray, bool>
            ThenAllZeros(this IMutator<IBitIndividual, BitArray, bool> mutator) =>
            mutator.ThenApply(new AllZeros());
    }
}
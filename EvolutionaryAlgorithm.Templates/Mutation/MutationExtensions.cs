using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Mutation
{
    public static class MutationExtensions
    {
        public static IMutator<IBitIndividual, BitArray, bool>
            ThenOneMaxStaticOptimalMutation(this IMutator<IBitIndividual, BitArray, bool> mutator, int geneSize) =>
            mutator.Then(new OneMaxStaticOptimalMutation(geneSize));
    }
}
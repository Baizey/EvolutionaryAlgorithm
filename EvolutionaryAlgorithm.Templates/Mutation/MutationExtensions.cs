using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Mutation
{
    public static class MutationExtensions
    {
        public static IBitMutator
            ThenOneMaxStaticOptimalMutation(this IBitMutator mutator, int geneSize) =>
            (IBitMutator) mutator.Then(new OneMaxStaticOptimalMutation(geneSize));
    }
}
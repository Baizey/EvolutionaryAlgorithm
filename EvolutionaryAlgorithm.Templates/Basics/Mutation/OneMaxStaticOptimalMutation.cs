using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.Basics.Mutation
{
    public class OneMaxStaticOptimalMutation<T> : IBitMutation<T> where T : IBitIndividual
    {
        public IEvolutionaryAlgorithm<T, BitArray, bool> Algorithm { get; set; }

        private readonly MutationApplier _applier = new MutationApplier();
        private int _geneCount;

        public void Initialize() => _geneCount = Algorithm.Parameters.GeneCount;

        public void Update()
        {
        }

        public void Mutate(int index, T child) => _applier.Mutate(child, 1, _geneCount);
    }
}
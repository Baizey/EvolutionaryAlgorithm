using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.Mutations
{
    public class SelfAdjustingMutation : IBitMutation<IBitIndividual>
    {
        private readonly MutationApplier _applier = new MutationApplier();
        private int _geneCount;
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize() => _geneCount = Algorithm.Parameters.GeneCount;

        public void Mutate(int index, IBitIndividual child) => _applier.Mutate(child, _geneCount);

        public void Update()
        {
        }
    }
}
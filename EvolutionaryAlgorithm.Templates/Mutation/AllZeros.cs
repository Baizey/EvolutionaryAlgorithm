using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Mutation
{
    public class AllZeros : IBitMutation
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }
        public void Mutate(IBitIndividual child) => child.Genes.SetAll(false);
    }
}
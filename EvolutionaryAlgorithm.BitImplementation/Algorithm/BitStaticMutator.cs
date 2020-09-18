using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Algorithm.Mutator;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm
{
    public class BitStaticMutator : StaticMutator<IBitIndividual, BitArray, bool>
    {
        public BitStaticMutator(int nextGenerationSize, IParentSelector<IBitIndividual, BitArray, bool> initialSelector = null) 
            : base(nextGenerationSize, initialSelector)
        {
        }
        public BitStaticMutator(int nextGenerationSize) : base(nextGenerationSize)
        {
        }
    }
}
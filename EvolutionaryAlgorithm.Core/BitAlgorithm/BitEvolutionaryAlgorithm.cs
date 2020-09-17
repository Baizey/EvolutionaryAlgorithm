using System.Collections;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Core.BitAlgorithm
{
    public class BitEvolutionaryAlgorithm : EvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, IBitEvolutionaryAlgorithm
    {
        public new static BitEvolutionaryAlgorithm Construct =>
            new BitEvolutionaryAlgorithm();
    }
}
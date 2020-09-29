using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.BitImplementation.Abstract
{
    public interface IBitIndividual : IIndividual<BitArray, bool>
    {
        public bool this[int i]
        {
            get => Genes[i];
            set => Genes[i] = value;
        }

        bool Flip(int i);

        int Ones { get; set; }
        int Zeros { get; }
    }
}
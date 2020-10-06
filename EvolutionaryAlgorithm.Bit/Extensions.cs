using System.Collections;

namespace EvolutionaryAlgorithm.BitImplementation
{
    public static class Extensions
    {
        public static void CopyTo(this BitArray self, BitArray other)
        {
            other.SetAll(false);
            other.Or(self);
        }
    }
}
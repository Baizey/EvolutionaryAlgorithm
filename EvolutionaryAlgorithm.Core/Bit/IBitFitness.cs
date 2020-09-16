using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Bit
{
    public interface IBitFitness : IFitness<BitArray, bool>
    {
        double IFitness<BitArray, bool>.Evaluate(
            IIndividual<BitArray, bool> individual) => Evaluate((IBitIndividual) individual);

        double Evaluate(IBitIndividual individual);
    }
}
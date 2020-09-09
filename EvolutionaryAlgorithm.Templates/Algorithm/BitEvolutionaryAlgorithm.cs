using System.Collections;
using EvolutionaryAlgorithm.Core;

namespace EvolutionaryAlgorithm.Template.Algorithm
{
    public class BitEvolutionaryAlgorithm : EvolutionaryAlgorithm<BitArray>
    {
        public BitEvolutionaryAlgorithm(IPopulation<BitArray> initialPopulation, IFitness<BitArray> fitnessFunction,
            IMutator<BitArray> mutatorFunction, IGenerationFilter<BitArray> generationFilterFunction) : base(
            initialPopulation, fitnessFunction, mutatorFunction, generationFilterFunction)
        {
        }
    }
}
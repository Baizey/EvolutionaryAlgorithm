using EvolutionaryAlgorithm.Core.Individual;
using EvolutionaryAlgorithm.Template.Population;

namespace EvolutionaryAlgorithm
{
    internal class Program
    {
        private static void Main()
        {
            var population = new Population<BooleanIndividual>(2, () => new BooleanIndividual(100, () => true));
        }
    }
}
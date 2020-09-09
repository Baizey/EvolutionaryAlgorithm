using System.Collections;
using EvolutionaryAlgorithm.Core.Individual;
using EvolutionaryAlgorithm.Template.Algorithm;
using EvolutionaryAlgorithm.Template.Fitness;
using EvolutionaryAlgorithm.Template.Mutation;
using EvolutionaryAlgorithm.Template.ParentSelector;
using EvolutionaryAlgorithm.Template.Population;
using EvolutionaryAlgorithm.Template.Selection;

namespace EvolutionaryAlgorithm
{
    internal class Program
    {
        private static void Main()
        {
            const int
                λ = 10,
                n = 100,
                μ = 1;

            var algo = new EvolutionaryAlgorithm<BitArray>(
                new BitPopulation(μ, () => new BitIndividual(n, () => true)),
                new OneMaxFitness(),
                new Mutator<BitArray>(λ, new FirstParentSelector<BitArray>())
                    .Then(new OneMaxStaticOptimalMutation(λ)),
                new ElitismGenerationFilter<BitArray>(μ));
        }
    }
}
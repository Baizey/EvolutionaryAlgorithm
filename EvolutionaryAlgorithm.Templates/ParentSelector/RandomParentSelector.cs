using System;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class RandomParentSelector : IBitParentSelector
    {
        private readonly Random _random;

        public RandomParentSelector() => _random = new Random();

        public IBitIndividual Select(IBitPopulation population) =>
            (IBitIndividual) population[_random.Next(population.Count)];
    }
}
using System;
using EvolutionaryAlgorithm.Core;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class RandomParentSelector<T> : IParentSelector<T> where T : ICloneable
    {
        private readonly Random _random;

        public RandomParentSelector() => _random = new Random();

        public IIndividual<T> Select(IPopulation<T> population) => population[_random.Next(population.Length)];
    }
}
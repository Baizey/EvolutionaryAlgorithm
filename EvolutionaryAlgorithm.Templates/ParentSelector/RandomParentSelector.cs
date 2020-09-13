using System;
using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class RandomParentSelector : IBitParentSelector
    {
        private readonly Random _random;

        public RandomParentSelector() => _random = new Random();

        public IIndividual<BitArray, bool> Select(IPopulation<BitArray, bool> population) =>
            population[_random.Next(population.Count)];
    }
}
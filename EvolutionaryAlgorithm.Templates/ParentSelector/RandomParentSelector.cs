using System;
using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class RandomParentSelector : IBitParentSelector
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        private readonly Random _random;

        public RandomParentSelector() => _random = new Random();

        public IBitIndividual Select(IPopulation<IBitIndividual, BitArray, bool> population) =>
            population[_random.Next(population.Count)];
    }
}
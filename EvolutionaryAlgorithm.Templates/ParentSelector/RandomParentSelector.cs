using System;
using System.Collections;
using System.Collections.Generic;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class RandomParentSelector : IBitParentSelector
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        private readonly Random _random;

        public RandomParentSelector() => _random = new Random();

        public IBitIndividual Select(IPopulation<IBitIndividual, BitArray, bool> population) =>
            population[_random.Next(population.Count)];
    }
}
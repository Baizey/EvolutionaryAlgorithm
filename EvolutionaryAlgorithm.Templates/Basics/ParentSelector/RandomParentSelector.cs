using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Basics.ParentSelector
{
    public class RandomParentSelector : IBitSingleParentSelector
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        private readonly Random _random;

        public RandomParentSelector() => _random = new Random();

        public IBitIndividual Select(int index, IPopulation<IBitIndividual, BitArray, bool> population) =>
            population[_random.Next(population.Count)];
    }
}
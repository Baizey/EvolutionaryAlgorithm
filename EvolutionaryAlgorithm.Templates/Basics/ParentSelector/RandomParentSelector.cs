using System;
using System.Collections;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Basics.ParentSelector
{
    public class RandomParentSelector<T> : IBitSingleParentSelector<T> where T : IBitIndividual
    {
        public IEvolutionaryAlgorithm<T, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        private readonly Random _random;

        public RandomParentSelector() => _random = new Random();

        public T Select(int index, IPopulation<T, BitArray, bool> population) =>
            population[_random.Next(population.Count)];
    }
}
using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.ParentSelector
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
        
        public T Select(int index) =>
            Algorithm.Population[_random.Next(Algorithm.Population.Count)];
    }
}
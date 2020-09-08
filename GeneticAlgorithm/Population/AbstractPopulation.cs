using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Population
{
    public abstract class AbstractPopulation<T> : IPopulation
    {
        public List<T> Individuals;

        protected AbstractPopulation(List<T> value) => Individuals = value;

        protected AbstractPopulation(Func<List<T>> generator) => Individuals = generator.Invoke();

        protected AbstractPopulation(int size, Func<T> generator) =>
            Individuals = Enumerable.Range(0, size).Select(_ => generator.Invoke()).ToList();

        public override string ToString() => Individuals.ToString();
    }
}
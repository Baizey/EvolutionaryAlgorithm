using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Population
{
    public class Population<T> : IPopulation, IEnumerable<T>
    {
        public List<T> Individuals;

        public Population(List<T> value) => Individuals = value;

        public Population(Func<List<T>> generator) => Individuals = generator.Invoke();

        public Population(int size, Func<T> generator) =>
            Individuals = Enumerable.Range(0, size).Select(_ => generator.Invoke()).ToList();

        public T this[int i]
        {
            get => Individuals[i];
            set => Individuals[i] = value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator() => Individuals.GetEnumerator();

        public override string ToString() => Individuals.ToString();
    }
}
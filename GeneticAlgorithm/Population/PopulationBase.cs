using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Population
{
    public abstract class PopulationBase<T> : IPopulation
    {
        public List<T> Individuals;

        protected PopulationBase(List<T> value) => Individuals = value;

        protected PopulationBase(Func<List<T>> generator) => Individuals = generator.Invoke();

        protected PopulationBase(int size, Func<T> generator) =>
            Individuals = Enumerable.Range(0, size).Select(_ => generator.Invoke()).ToList();


        public T this[int i]
        {
            get => Individuals[i];
            set => Individuals[i] = value;
        }

        public override string ToString() => Individuals.ToString();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core;

namespace EvolutionaryAlgorithm.Template.Population
{
    public class Population<T> : IPopulation<T> where T : ICloneable
    {
        public List<T> Individuals { get; set; }
        public int Generation { get; set; } = 0;

        public Population(List<T> value) => Individuals = value;

        public Population(int size, Func<T> generator) =>
            Individuals = Enumerable.Range(0, size).Select(_ => generator.Invoke()).ToList();

        public IEnumerator<T> GetEnumerator()
        {
            return Individuals.GetEnumerator();
        }

        public override string ToString() => Individuals.ToString();

        public object Clone() => new Population<T>(Individuals.Select(individual => (T) individual.Clone()).ToList());

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Individuals.GetEnumerator();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Template.Population
{
    public class Population<T> : IPopulation<T> where T : ICloneable
    {
        public List<IIndividual<T>> Individuals { get; set; }
        public int Length => Individuals.Count;
        public int Generation { get; set; } = 0;

        public Population(List<IIndividual<T>> value) => Individuals = value;

        public Population(int size, Func<IIndividual<T>> generator) =>
            Individuals = Enumerable.Range(0, size).Select(_ => generator.Invoke()).ToList();

        public IEnumerator<IIndividual<T>> GetEnumerator()
        {
            return Individuals.GetEnumerator();
        }

        public override string ToString() => Individuals.ToString();

        public object Clone() =>
            new Population<T>(Individuals.Select(individual => (IIndividual<T>) individual.Clone()).ToList());

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Individuals.GetEnumerator();
        }
    }
}
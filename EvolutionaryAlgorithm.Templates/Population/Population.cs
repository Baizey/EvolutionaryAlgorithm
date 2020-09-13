using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;
using EvolutionaryAlgorithm.Template.Individual;

namespace EvolutionaryAlgorithm.Template.Population
{
    public class BitPopulation : IBitPopulation
    {
        public static BitPopulation From(int populationCount, int geneCount, bool defaultValue) =>
            From(populationCount, geneCount, () => defaultValue);

        public static BitPopulation From(int populationCount, int geneCount, Func<bool> generator) =>
            new BitPopulation(populationCount, () => new BitIndividual(geneCount, generator));

        public List<IIndividual<BitArray, bool>> Individuals { get; set; }
        public int Count => Individuals.Count;
        public int Generation { get; set; } = 0;

        public IIndividual<BitArray, bool> Best =>
            Individuals.Aggregate((a, b) => a.Fitness > b.Fitness ? a : b);

        public BitPopulation(List<IIndividual<BitArray, bool>> value) => Individuals = value;

        public BitPopulation(int size, Func<IIndividual<BitArray, bool>> generator) =>
            Individuals = Enumerable.Range(0, size).Select(_ => generator.Invoke()).ToList();

        public IEnumerator<IIndividual<BitArray, bool>> GetEnumerator() => Individuals.GetEnumerator();

        public override string ToString() => Individuals.ToString();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public object Clone() =>
            new BitPopulation(Individuals.Select(individual => (IIndividual<BitArray, bool>) individual.Clone())
                .ToList());
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Core.BitAlgorithm
{
    public class BitPopulation : Population<IBitIndividual, BitArray, bool>, IBitPopulation
    {
        public static BitPopulation From(int populationCount, int geneCount, bool defaultValue) =>
            From(populationCount, geneCount, () => defaultValue);

        public static BitPopulation From(int populationCount, int geneCount, Func<bool> generator) =>
            new BitPopulation(populationCount, () => new BitIndividual(geneCount, generator));

        public BitPopulation(List<IBitIndividual> value) : base(value)
        {
        }

        public BitPopulation(int size, Func<IBitIndividual> generator)
            : base(Enumerable.Range(0, size).Select(_ => generator.Invoke()).ToList())
        {
        }
    }
}
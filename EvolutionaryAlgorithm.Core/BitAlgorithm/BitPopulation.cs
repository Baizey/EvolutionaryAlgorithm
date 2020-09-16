﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Core.BitAlgorithm
{
    public class BitPopulation : Population<BitArray, bool>, IBitPopulation
    {
        public static BitPopulation From(int populationCount, int geneCount, bool defaultValue) =>
            From(populationCount, geneCount, () => defaultValue);

        public static BitPopulation From(int populationCount, int geneCount, Func<bool> generator) =>
            new BitPopulation(populationCount, () => new BitIndividual(geneCount, generator));

        public BitPopulation(List<IIndividual<BitArray, bool>> value) : base(value)
        {
        }

        public BitPopulation(int size, Func<IIndividual<BitArray, bool>> generator)
            : base(Enumerable.Range(0, size).Select(_ => generator.Invoke()).ToList())
        {
        }
    }
}
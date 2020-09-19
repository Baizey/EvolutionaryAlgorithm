using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm
{
    public class BitPopulation : Population<IBitIndividual, BitArray, bool>, IBitPopulation
    {
        private BitPopulation(Func<int, BitIndividual> func) : base(func)
        {
        }

        public static BitPopulation From(Func<bool> generator) =>
            new BitPopulation(geneCount => new BitIndividual(geneCount, generator));
    }
}
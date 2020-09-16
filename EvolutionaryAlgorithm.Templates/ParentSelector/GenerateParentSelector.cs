using System;
using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;
using EvolutionaryAlgorithm.Core.BitAlgorithm;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class GenerateParentSelector : IBitParentSelector
    {
        public IEvolutionaryAlgorithm<BitArray, bool> Algorithm { get; set; }

        private readonly Random _random = new Random();

        public IBitIndividual Select(IBitPopulation population) =>
            new BitIndividual(population[0].Size, () => _random.NextDouble() >= 0.5);
    }
}
using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class GenerateParentSelector : IBitParentSelector
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        private readonly Random _random = new Random();

        public IBitIndividual Select(IPopulation<IBitIndividual, BitArray, bool> population) =>
            new BitIndividual(population[0].Size, () => _random.NextDouble() >= 0.5);
    }
}
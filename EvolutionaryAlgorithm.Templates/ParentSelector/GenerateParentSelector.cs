using System;
using System.Collections;
using System.Collections.Generic;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class GenerateParentSelector : IBitParentSelector
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        private readonly Random _random = new Random();

        public IBitIndividual Select(List<IBitIndividual> population) =>
            new BitIndividual(population[0].Size, () => _random.NextDouble() >= 0.5);
    }
}
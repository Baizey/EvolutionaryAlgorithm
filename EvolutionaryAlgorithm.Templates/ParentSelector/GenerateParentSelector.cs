using System;
using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;
using EvolutionaryAlgorithm.Core.BitAlgorithm;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class GenerateParentSelector : IBitParentSelector
    {
        private readonly Random _random = new Random();

        public IIndividual<BitArray, bool> Select(IPopulation<BitArray, bool> population) =>
            new BitIndividual(population[0].Size, () => _random.NextDouble() >= 0.5);
    }
}
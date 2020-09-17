using System.Collections;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Selection
{
    public class ElitismGenerationFilter : IBitGenerationFilter
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public IPopulation<IBitIndividual, BitArray, bool> Filter(
            IPopulation<IBitIndividual, BitArray, bool> population,
            List<IBitIndividual> newcomers)
        {
            var initialSize = population.Count;
            population.Individuals.AddRange(newcomers);
            population.Individuals.Sort((a, b) => b.CompareTo(a));
            population.Individuals = population.Individuals.GetRange(0, initialSize);
            return population;
        }
    }
}
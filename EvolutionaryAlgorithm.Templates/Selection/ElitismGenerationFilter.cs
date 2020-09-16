using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Selection
{
    public class ElitismGenerationFilter : IBitGenerationFilter
    {
        public IBitPopulation Filter(IBitPopulation population, List<IBitIndividual> newcomers)
        {
            var initialSize = population.Count;
            population.Individuals.AddRange(newcomers);
            population.Individuals.Sort((a, b) => b.CompareTo(a));
            population.Individuals = population.Individuals.GetRange(0, initialSize);
            return population;
        }
    }
}
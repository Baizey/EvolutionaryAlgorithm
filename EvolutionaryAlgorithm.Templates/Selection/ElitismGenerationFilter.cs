using System;
using EvolutionaryAlgorithm.Core;

namespace EvolutionaryAlgorithm.Template.Selection
{
    public class ElitismGenerationFilter<T> : IGenerationFilter<T> where T : ICloneable
    {
        private readonly int _keep;

        public ElitismGenerationFilter(int keep)
        {
            _keep = keep;
        }

        public IPopulation<T> Filter(IPopulation<T> population)
        {
            population.Individuals.Sort();
            population.Individuals = population.Individuals.GetRange(0, _keep);
            return population;
        }
    }
}
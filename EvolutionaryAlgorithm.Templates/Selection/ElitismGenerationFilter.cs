using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Template.Selection
{
    public class ElitismGenerationFilter<T> : IGenerationFilter<T> where T : ICloneable
    {
        private readonly int _μ;

        public ElitismGenerationFilter(int μ)
        {
            _μ = μ;
        }

        public IPopulation<T> Filter(IPopulation<T> population, List<IIndividual<T>> newcomers)
        {
            population.Individuals.AddRange(newcomers);
            population.Individuals.Sort();
            population.Individuals = population.Individuals.GetRange(0, _μ);
            return population;
        }
    }
}
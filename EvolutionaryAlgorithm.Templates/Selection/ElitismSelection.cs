using System;
using EvolutionaryAlgorithm.Core;

namespace EvolutionaryAlgorithm.Template.Selection
{
    public class ElitismSelection<T> : ISelection<T> where T : ICloneable
    {
        private readonly int _keep;

        public ElitismSelection(int keep)
        {
            _keep = keep;
        }

        public IPopulation<T> Select(IPopulation<T> population)
        {
            population.Individuals.Sort();
            population.Individuals = population.Individuals.GetRange(0, _keep);
            return population;
        }
    }
}
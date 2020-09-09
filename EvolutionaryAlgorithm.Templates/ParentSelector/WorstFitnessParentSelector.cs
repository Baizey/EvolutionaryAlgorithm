using System;
using System.Linq;
using EvolutionaryAlgorithm.Core;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class WorstFitnessParentSelector<T> : IParentSelector<T> where T : ICloneable
    {
        public IIndividual<T> Select(IPopulation<T> population) =>
            population.Individuals.Aggregate((a, b) => a.Fitness < b.Fitness ? a : b);
    }
}
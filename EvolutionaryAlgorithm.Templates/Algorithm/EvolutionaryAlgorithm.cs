using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core;

namespace EvolutionaryAlgorithm.Template.Algorithm
{
    public class EvolutionaryAlgorithm<T> : IEvolutionaryAlgorithm<T> where T : ICloneable
    {
        public IPopulation<T> Population { get; set; }
        public IFitness<T> Fitness { get; set; }
        public IMutation<T> Mutator { get; set; }
        public ICrossover<T> Crossover { get; set; }
        public ISelection<T> Selector { get; set; }

        public EvolutionaryAlgorithm(
            IPopulation<T> initialPopulation,
            IFitness<T> fitnessFunction,
            IMutation<T> mutationFunction,
            ICrossover<T> crossoverFunction,
            ISelection<T> selectionFunction)
        {
            Population = initialPopulation;
            Fitness = fitnessFunction;
            Mutator = mutationFunction;
            Crossover = crossoverFunction;
            Selector = selectionFunction;
        }

        public Task Evolve()
        {
            throw new NotImplementedException();
        }
    }
}
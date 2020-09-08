using System;
using System.Threading.Tasks;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Algorithm
{
    public class EvolutionaryAlgorithm : IEvolutionaryAlgorithm
    {
        public IPopulation Population;
        public Func<IIndividual, double> Fitness;
        public Func<IIndividual, IIndividual> Mutator;
        public Func<IPopulation, IPopulation> Selector;

        public EvolutionaryAlgorithm(
            IPopulation initialPopulation,
            Func<IIndividual, double> fitnessFunction,
            Func<IIndividual, IIndividual> mutationFunction,
            Func<IPopulation, IPopulation> selectionFunction)
        {
            Population = initialPopulation;
            Fitness = fitnessFunction;
            Mutator = mutationFunction;
            Selector = selectionFunction;
        }

        public Task Evolve()
        {
            throw new System.NotImplementedException();
        }
    }
}
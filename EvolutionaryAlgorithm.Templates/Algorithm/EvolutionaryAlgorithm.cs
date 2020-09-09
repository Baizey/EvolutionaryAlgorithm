using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core;

namespace EvolutionaryAlgorithm.Template.Algorithm
{
    public class EvolutionaryAlgorithm<T> : IEvolutionaryAlgorithm<T> where T : ICloneable
    {
        public IPopulation<T> Population { get; }
        public IFitness<T> Fitness { get; }
        public IMutator<T> Mutator { get; }
        public IGenerationFilter<T> GenerationFilter { get; }

        public EvolutionaryAlgorithm(
            IPopulation<T> initialPopulation,
            IFitness<T> fitnessFunction,
            IMutator<T> mutatorFunction,
            IGenerationFilter<T> generationFilterFunction)
        {
            Population = initialPopulation;
            Fitness = fitnessFunction;
            Mutator = mutatorFunction;
            GenerationFilter = generationFilterFunction;

            foreach (var individual in Population)
                Fitness.Evaluate(individual);
        }

        public Task Evolve()
        {
            throw new NotImplementedException();
        }
    }
}
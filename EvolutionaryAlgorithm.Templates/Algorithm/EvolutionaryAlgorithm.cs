using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core;

namespace EvolutionaryAlgorithm.Template.Algorithm
{
    public class EvolutionaryAlgorithm<T> : IEvolutionaryAlgorithm<T> where T : ICloneable
    {
        public IPopulation<T> Population { get; set; }
        public IFitness<T> Fitness { get; set; }
        public IMutator<T> Mutator { get; set; }
        public IGenerationFilter<T> GenerationFilter { get; set; }

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
        }

        public Task Evolve()
        {
            throw new NotImplementedException();
        }
    }
}
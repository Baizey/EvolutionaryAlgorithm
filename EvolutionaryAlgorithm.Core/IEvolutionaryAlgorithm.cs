using System;
using System.Threading.Tasks;

namespace EvolutionaryAlgorithm.Core
{
    public interface IEvolutionaryAlgorithm<T> where T : ICloneable
    {
        public IPopulation<T> Population { get; }
        public IFitness<T> Fitness { get; }
        public IMutator<T> Mutator { get; }
        public IGenerationFilter<T> GenerationFilter { get; }
        Task Evolve();
    }
}
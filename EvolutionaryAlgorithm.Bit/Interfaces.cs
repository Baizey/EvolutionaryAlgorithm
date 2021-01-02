using System.Collections;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Fitness;
using EvolutionaryAlgorithm.Core.HyperHeuristic;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.GenerationFilter;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.BitImplementation
{
    public interface IBitEvolutionaryAlgorithm<T> : IEvolutionaryAlgorithm<T, bool[], bool> where T : IBitIndividual
    {
    }

    public interface IBitPopulation<T> : IPopulation<T, bool[], bool> where T : IBitIndividual
    {
    }

    public interface IBitFitness<T> : IFitness<T, bool[], bool> where T : IBitIndividual
    {
    }

    public interface IBitHyperHeuristic<T> : IHyperHeuristic<T, bool[], bool> where T : IBitIndividual
    {
    }

    public interface IBitGenerationGenerator<T> : IGenerationGenerator<T, bool[], bool> where T : IBitIndividual
    {
    }

    public interface IBitGenerationFilter<T> : IGenerationFilter<T, bool[], bool> where T : IBitIndividual
    {
    }

    public interface IBitMutator<T> : IMutator<T, bool[], bool> where T : IBitIndividual
    {
    }

    public interface IBitMutation<T> : IMutation<T, bool[], bool> where T : IBitIndividual
    {
    }

    public interface IBitSingleParentSelector<T> : ISingleParentSelector<T, bool[], bool> where T : IBitIndividual
    {
    }

    public interface IBitMultiParentSelector<T> : IMultiParentSelector<T, bool[], bool> where T : IBitIndividual
    {
    }
}
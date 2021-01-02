using System;
using System.Collections;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.GenerationFilter;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.BitImplementation
{
    public class BitEvolutionaryAlgorithm<T> : EvolutionaryAlgorithm<T, bool[], bool>, IBitEvolutionaryAlgorithm<T>
        where T : IBitIndividual
    {
        public static BitEvolutionaryAlgorithm<T> Construct => new BitEvolutionaryAlgorithm<T>();
    }

    public class BitPopulation<T> : Population<T, bool[], bool>, IBitPopulation<T>
        where T : IBitIndividual
    {
        public BitPopulation(Func<int, T> value) : base(value)
        {
        }

        public BitPopulation(IPopulation<T, bool[], bool> other) : base(other)
        {
        }
    }

    public abstract class BitHyperHeuristicBase<T> : HyperHeuristicBase<T, bool[], bool>
        where T : IBitIndividual
    {
    }

    public class BitSimpleHeuristic<T> : SimpleHeuristic<T, bool[], bool>, IBitHyperHeuristic<T>
        where T : IBitIndividual
    {
        public BitSimpleHeuristic(IGenerationGenerator<T, bool[], bool> generationGenerator)
            : base(generationGenerator)
        {
        }
    }

    public class BitElitismGenerationFilterAlwaysNew<T> : ElitismGenerationFilterAlwaysNew<T, bool[], bool>,
        IBitGenerationFilter<T>
        where T : IBitIndividual
    {
    }

    public class BitElitismGenerationFilter<T> : ElitismGenerationFilter<T, bool[], bool>, IBitGenerationFilter<T>
        where T : IBitIndividual
    {
        public BitElitismGenerationFilter(bool preferNew) : base(preferNew)
        {
        }
    }

    public class BitGenerationGenerator<T> : GenerationGenerator<T, bool[], bool>, IBitGenerationGenerator<T>
        where T : IBitIndividual
    {
    }

    public class BitMutator<T> : Mutator<T, bool[], bool>, IBitMutator<T>
        where T : IBitIndividual
    {
    }

    public interface IBitParameterAdjuster<T> : IParameterAdjuster<T, bool[], bool>
        where T : IBitIndividual
    {
    }
}
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
    public class BitEvolutionaryAlgorithm<T> : EvolutionaryAlgorithm<T, BitArray, bool>, IBitEvolutionaryAlgorithm<T>
        where T : IBitIndividual
    {
        public static BitEvolutionaryAlgorithm<T> Construct => new BitEvolutionaryAlgorithm<T>();
    }

    public class BitPopulation<T> : Population<T, BitArray, bool>, IBitPopulation<T>
        where T : IBitIndividual
    {
        public BitPopulation(Func<int, T> value) : base(value)
        {
        }

        public BitPopulation(IPopulation<T, BitArray, bool> other) : base(other)
        {
        }
    }

    public abstract class BitHyperHeuristicBase<T> : HyperHeuristicBase<T, BitArray, bool>
        where T : IBitIndividual
    {
    }

    public class BitSimpleHeuristic<T> : SimpleHeuristic<T, BitArray, bool>, IBitHyperHeuristic<T>
        where T : IBitIndividual
    {
        public BitSimpleHeuristic(IGenerationGenerator<T, BitArray, bool> generationGenerator)
            : base(generationGenerator)
        {
        }
    }

    public class BitElitismGenerationFilter<T> : ElitismGenerationFilter<T, BitArray, bool>, IBitGenerationFilter<T>
        where T : IBitIndividual
    {
        public BitElitismGenerationFilter(bool preferNew) : base(preferNew)
        {
        }
    }

    public class BitGenerationGenerator<T> : GenerationGenerator<T, BitArray, bool>, IBitGenerationGenerator<T>
        where T : IBitIndividual
    {
    }

    public class BitMutator<T> : Mutator<T, BitArray, bool>, IBitMutator<T>
        where T : IBitIndividual
    {
    }
}
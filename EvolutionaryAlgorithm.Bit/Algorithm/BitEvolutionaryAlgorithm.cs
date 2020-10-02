using System;
using System.Collections;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Algorithm.Mutator;

namespace EvolutionaryAlgorithm.Bit.Algorithm
{
    public class BitEvolutionaryAlgorithm<T> : EvolutionaryAlgorithm<T, BitArray, bool>,
        IBitEvolutionaryAlgorithm<T>
        where T : IBitIndividual
    {
        public static BitEvolutionaryAlgorithm<T> Construct => new BitEvolutionaryAlgorithm<T>();
    }

    public class BitPopulation<T> : Population<T, BitArray, bool>, IBitPopulation<T> where T : IBitIndividual
    {
        public BitPopulation(Func<int, T> value) : base(value)
        {
        }

        public BitPopulation(IPopulation<T, BitArray, bool> other) : base(other)
        {
        }
    }

    public class BitSimpleHeuristic<T> : SimpleHeuristic<T, BitArray, bool>, IBitHyperHeuristic<T>
        where T : IBitIndividual
    {
        public BitSimpleHeuristic(IGenerationGenerator<T, BitArray, bool> generationGenerator)
            : base(generationGenerator)
        {
        }
    }

    public class BitGenerationGenerator<T> : GenerationGenerator<T, BitArray, bool>,
        IBitGenerationGenerator<T> where T : IBitIndividual
    {
    }

    public class BitMutator : Mutator<IBitIndividual, BitArray, bool>, IBitMutator<IBitIndividual>
    {
    }
}
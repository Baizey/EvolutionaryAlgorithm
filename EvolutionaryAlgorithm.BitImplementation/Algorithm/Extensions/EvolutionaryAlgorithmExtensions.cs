using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions
{
    public static class BitEvolutionaryAlgorithmExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingParameters(this IBitEvolutionaryAlgorithm algo,
            IParameters<IBitIndividual, BitArray, bool> parameters)
        {
            algo.Parameters = parameters;
            return algo;
        }

        public static IBitEvolutionaryAlgorithm UsingTermination(this IBitEvolutionaryAlgorithm algo,
            ITermination<IBitIndividual, BitArray, bool> termination)
        {
            algo.Termination = termination;
            return algo;
        }
        
        public static IBitEvolutionaryAlgorithm UsingInitialGenome(this IBitEvolutionaryAlgorithm algo,
            IBitPopulation initialPopulation)
        {
            algo.Population = initialPopulation;
            return algo;
        }

        public static IBitEvolutionaryAlgorithm UsingStatistics(this IBitEvolutionaryAlgorithm algo,
            IEvolutionaryStatistics<IBitIndividual, BitArray, bool> statistics)
        {
            algo.Statistics = statistics;
            return algo;
        }

        public static IBitEvolutionaryAlgorithm UsingFitness(this IBitEvolutionaryAlgorithm algo, IBitFitness fitness)
        {
            algo.Fitness = fitness;
            return algo;
        }

        public static IBitEvolutionaryAlgorithm UsingMutator(this IBitEvolutionaryAlgorithm algo,
            Action<IBitMutator> usingMutations)
        {
            var mutator = new BitMutator();
            algo.Mutator = mutator;
            usingMutations.Invoke(mutator);
            return algo;
        }

        public static IBitEvolutionaryAlgorithm UsingMutator(this IBitEvolutionaryAlgorithm algo,
            IBitParentSelector parentSelector,
            Action<IBitMutator> usingMutations)
        {
            var mutator = new BitMutator(parentSelector);
            algo.Mutator = mutator;
            usingMutations.Invoke(mutator);
            return algo;
        }

        public static IBitEvolutionaryAlgorithm UsingGenerationFilter(this IBitEvolutionaryAlgorithm algo,
            IBitGenerationFilter generationFilter)
        {
            algo.GenerationFilter = generationFilter;
            return algo;
        }
    }
}
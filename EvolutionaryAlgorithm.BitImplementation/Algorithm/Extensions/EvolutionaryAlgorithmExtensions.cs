using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm.Mutator;

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

        public static IBitEvolutionaryAlgorithm UsingHyperHeuristic(this IBitEvolutionaryAlgorithm algo,
            IBitHyperHeuristic hyperHeuristic)
        {
            algo.HyperHeuristic = hyperHeuristic;
            return algo;
        }
        
        public static IBitEvolutionaryAlgorithm UsingGenerationGenerator(this IBitEvolutionaryAlgorithm algo,
            IGenerationGenerator<IBitIndividual, BitArray, bool> generationGenerator)
        {
            algo.HyperHeuristic = new SingleHeuristic<IBitIndividual, BitArray, bool>(generationGenerator);
            return algo;
        }
    }
}
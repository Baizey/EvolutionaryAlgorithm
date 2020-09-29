using System;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm.Mutator;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public static class EvolutionaryAlgorithmExtensions
    {
        public static IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
            UsingTermination<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                ITermination<TIndividual, TGeneStructure, TGene> termination)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable
        {
            algo.Termination = termination;
            return algo;
        }

        public static IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
            UsingParameters<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                IParameters<TIndividual, TGeneStructure, TGene> parameters)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable
        {
            algo.Parameters = parameters;
            return algo;
        }

        public static IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
            UsingPopulation<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                IPopulation<TIndividual, TGeneStructure, TGene> initialPopulation)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable
        {
            algo.Population = initialPopulation;
            return algo;
        }

        public static IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
            UsingStatistics<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> statistics)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable
        {
            algo.Statistics = statistics;
            return algo;
        }

        public static IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
            UsingFitness<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                IFitness<TIndividual, TGeneStructure, TGene> fitness)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable
        {
            algo.Fitness = fitness;
            return algo;
        }

        public static IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
            UsingHyperHeuristic<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                IHyperHeuristic<TIndividual, TGeneStructure, TGene> heuristic)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable
        {
            algo.HyperHeuristic = heuristic;
            return algo;
        }

        public static IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
            UsingGenerationGenerator<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                IGenerationGenerator<TIndividual, TGeneStructure, TGene> generator)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable
        {
            algo.HyperHeuristic = new SingleHeuristic<TIndividual, TGeneStructure, TGene>(generator);
            return algo;
        }
    }
}
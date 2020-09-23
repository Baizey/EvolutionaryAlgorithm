﻿using System;
using EvolutionaryAlgorithm.Core.Abstract;
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
            UsingGlobalParameters<TIndividual, TGeneStructure, TGene>(
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
            UsingMutator<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                Action<IMutator<TIndividual, TGeneStructure, TGene>> usingMutations)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable
        {
            algo.HyperMutator = new SimpleHyperMutator<TIndividual, TGeneStructure, TGene>(new Mutator<TIndividual, TGeneStructure, TGene>());
            usingMutations.Invoke(algo.HyperMutator.States[0]);
            return algo;
        }

        public static IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
            UsingGenerationFilter<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                IGenerationFilter<TIndividual, TGeneStructure, TGene> generationFilter)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable
        {
            algo.GenerationFilter = generationFilter;
            return algo;
        }
    }
}
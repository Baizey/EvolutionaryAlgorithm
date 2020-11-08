using System;
using EvolutionaryAlgorithm.Core.Fitness;
using EvolutionaryAlgorithm.Core.HyperHeuristic;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.GenerationFilter;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Crossover;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Population;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public static class EvolutionaryAlgorithmExtensions
    {
        public static IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
            UsingParameters<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                IParameters parameters)
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
            UsingEvaluation<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                IFitness<TIndividual, TGeneStructure, TGene> fitness)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable
        {
            algo.Fitness = fitness;
            return algo;
        }

        public static IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
            UsingHeuristic<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                Func<IMutator<TIndividual, TGeneStructure, TGene>, IMutator<TIndividual, TGeneStructure, TGene>>
                    applyMutations,
                IGenerationFilter<TIndividual, TGeneStructure, TGene> filter)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable =>
            algo.UsingHeuristic(new GenerationGenerator<TIndividual, TGeneStructure, TGene>
            {
                Mutator = applyMutations(new Mutator<TIndividual, TGeneStructure, TGene>()),
                Filter = filter
            });

        public static IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
            UsingHeuristic<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                IGenerationGenerator<TIndividual, TGeneStructure, TGene> generator)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable =>
            algo.UsingHeuristic(new SimpleHeuristic<TIndividual, TGeneStructure, TGene>(generator));

        public static IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
            UsingHeuristic<TIndividual, TGeneStructure, TGene>(
                this IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo,
                IHyperHeuristic<TIndividual, TGeneStructure, TGene> heuristic)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable
        {
            algo.HyperHeuristic = heuristic;
            return algo;
        }

        public static IMutator<TIndividual, TGeneStructure, TGene>
            CrossoverGenesFrom<TIndividual, TGeneStructure, TGene>(
                this IMutator<TIndividual, TGeneStructure, TGene> mutator,
                MultiParentCrossoverBase<TIndividual, TGeneStructure, TGene> crossover)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable =>
            mutator.ThenApply(crossover);

        public static IMutator<TIndividual, TGeneStructure, TGene>
            Crossover<TIndividual, TGeneStructure, TGene>(
                this IMutator<TIndividual, TGeneStructure, TGene> mutator,
                SingleParentCrossoverBase<TIndividual, TGeneStructure, TGene> crossover)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable =>
            mutator.ThenApply(crossover);

        public static IMutator<TIndividual, TGeneStructure, TGene>
            CloneGenesFrom<TIndividual, TGeneStructure, TGene>(
                this IMutator<TIndividual, TGeneStructure, TGene> mutator,
                ISingleParentSelector<TIndividual, TGeneStructure, TGene> parentSelector)
            where TIndividual : IIndividual<TGeneStructure, TGene>
            where TGeneStructure : ICloneable =>
            mutator.ThenApply(new CloneParent<TIndividual, TGeneStructure, TGene>(parentSelector));
    }
}
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.BitImplementation.Templates;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;
using EvolutionaryAlgorithm.Template.Mutations;
using EvolutionaryAlgorithm.Template.ParentSelector;
using EvolutionaryAlgorithm.Template.SelfAdjusters;

namespace EvolutionaryAlgorithm.Template
{
    public static class PresetGenerator
    {
        public static BitGenerationGenerator<IBitIndividual> Asymmetric(double learningRate, int observationPhase) =>
            new BitGenerationGenerator<IBitIndividual>
            {
                Mutator = new BitMutator<IBitIndividual>()
                    .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                    .ThenApplyMutation(new AsymmetricMutation(learningRate, observationPhase)),
                Filter = new BitElitismGenerationFilter<IBitIndividual>(true)
            };

        public static BitGenerationGenerator<IBitIndividual> SingleEndogenous(int learningRate) =>
            new BitGenerationGenerator<IBitIndividual>
            {
                Mutator = new BitMutator<IBitIndividual>()
                    .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                    .ThenApplyMutation(new SelfAdjustingMutation())
                    .AdjustParameterUsing(new EndogenousSelfAdjustingMutationRate(learningRate)),
                Filter = new BitElitismGenerationFilterAlwaysNew<IBitIndividual>()
            };

        public static BitGenerationGenerator<IBitIndividual> MultiEndogenous(int learningRate) =>
            new BitGenerationGenerator<IBitIndividual>
            {
                Mutator = new BitMutator<IBitIndividual>()
                    .CrossoverGenesFrom(new BitMiddlePointCrossover<IBitIndividual>(
                        new MultiTournamentSelection<IBitIndividual, BitArray, bool>(2)))
                    .ThenApplyMutation(new SelfAdjustingMutation())
                    .AdjustParameterUsing(new EndogenousSelfAdjustingMutationRate(learningRate)),
                Filter = new BitElitismGenerationFilterAlwaysNew<IBitIndividual>()
            };

        public static BitGenerationGenerator<IBitIndividual> HeavyTail(
            int learningRate,
            double beta) => new BitGenerationGenerator<IBitIndividual>
        {
            Mutator = new BitMutator<IBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                .ThenApplyMutation(new SelfAdaptingHeavyTailMutation(beta))
                .AdjustParameterUsing(new SelfAdjustingMutationRate(learningRate, a => a.Parameters.GeneCount / 4)),
            Filter = new BitElitismGenerationFilter<IBitIndividual>(true)
        };

        public static IHyperHeuristic<IBitIndividual, BitArray, bool> StagnationDetection(
            int initialMutationRate,
            int limitFactor,
            int learningRate) =>
            StagnationDetection(initialMutationRate, limitFactor, HalfAndHalf(learningRate));

        public static IHyperHeuristic<IBitIndividual, BitArray, bool> StagnationDetection(
            int initialMutationRate,
            int limitFactor,
            BitGenerationGenerator<IBitIndividual> generator) =>
            new StagnationDetectionHyperHeuristic(initialMutationRate, limitFactor, generator);

        public static BitGenerationGenerator<IBitIndividual> HalfAndHalf(int learningRate) =>
            new BitGenerationGenerator<IBitIndividual>
            {
                Mutator = new BitMutator<IBitIndividual>()
                    .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                    .ThenApplyMutation(new SelfAdjustingMutation())
                    .AdjustParameterUsing(
                        new HalfAndHalfSelfAdjustingMutationRate(learningRate, a => a.Parameters.GeneCount / 4)),
                Filter = new BitElitismGenerationFilter<IBitIndividual>(true),
            };

        public static BitGenerationGenerator<IBitIndividual> Repair(int learningRate, double repairChance) =>
            new BitGenerationGenerator<IBitIndividual>
            {
                Mutator = new BitMutator<IBitIndividual>()
                    .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                    .ThenApplyMutation(new MutateAndRepairMutation(repairChance))
                    .AdjustParameterUsing(new OneFifthFitnessDependentLambda(learningRate)),
                Filter = new BitElitismGenerationFilter<IBitIndividual>(true)
            };
    }
}
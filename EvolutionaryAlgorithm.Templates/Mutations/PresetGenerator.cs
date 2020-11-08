using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.BitImplementation.Templates;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;
using EvolutionaryAlgorithm.Template.Mutations.SelfAdjusters;

namespace EvolutionaryAlgorithm.Template.Mutations
{
    public static class PresetGenerator
    {
        public static BitGenerationGenerator<IBitIndividual> Asymmetric(
            double learningRate,
            int observationPhase) => new BitGenerationGenerator<IBitIndividual>
        {
            Mutator = new BitMutator<IBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                .ThenApply(new AsymmetricMutation(learningRate, observationPhase)),
            Filter = new BitElitismGenerationFilter<IBitIndividual>(true)
        };

        public static BitGenerationGenerator<IBitIndividual> SingleEndogenous(
            int learningRate) => new BitGenerationGenerator<IBitIndividual>
        {
            Mutator = new BitMutator<IBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                .ThenApply(new EndogenousMutation(learningRate))
                .ThenAfterGeneratingApply(new UpdateMutationRateFromBestIndividual()),
            Filter = new BitElitismGenerationFilter<IBitIndividual>(true)
        };

        public static BitGenerationGenerator<IBitIndividual> MultiEndogenous(
            int learningRate) => new BitGenerationGenerator<IBitIndividual>
        {
            Mutator = new BitMutator<IBitIndividual>()
                .CrossoverGenesFrom(new BitMiddlePointCrossover<IBitIndividual>(
                    new MultiTournamentSelection<IBitIndividual, BitArray, bool>(2)))
                .ThenApply(new EndogenousMutation(learningRate))
                .ThenAfterGeneratingApply(new UpdateMutationRateFromBestIndividual()),
            Filter = new BitElitismGenerationFilter<IBitIndividual>(true)
        };

        public static BitGenerationGenerator<IBitIndividual> HeavyTail(
            double beta) => new BitGenerationGenerator<IBitIndividual>
        {
            Mutator = new BitMutator<IBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                .ThenApply(new HeavyTailHalfAndHalfMutation(beta))
                .ThenAfterGeneratingApply(new UpdateMutationRateFromBestIndividual()),
            Filter = new BitElitismGenerationFilter<IBitIndividual>(true)
        };

        public static IHyperHeuristic<IBitIndividual, BitArray, bool> StagnationDetection(
            int initialMutationRate,
            int limitFactor,
            BitGenerationGenerator<IBitIndividual> generator = null) =>
            new StagnationDetectionHyperHeuristic(
                initialMutationRate, 
                limitFactor,
                generator ?? HalfAndHalf(2, a => a.Parameters.GeneCount / 4));

        public static BitGenerationGenerator<IBitIndividual> HalfAndHalf(
            int learningRate,
            Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, int> capCalc = null) =>
            new BitGenerationGenerator<IBitIndividual>
            {
                Mutator = new BitMutator<IBitIndividual>()
                    .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                    .ThenApply(new HalfAndHalfMutation(learningRate, capCalc))
                    .ThenAfterGeneratingApply(new UpdateHalfAndHalfMutationRateFromBestIndividual(capCalc)),
                Filter = new BitElitismGenerationFilter<IBitIndividual>(true),
            };

        public static BitGenerationGenerator<IBitIndividual> Repair(
            int learningRate,
            double repairChance) => new BitGenerationGenerator<IBitIndividual>
        {
            Mutator = new BitMutator<IBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                .ThenApply(new MutateAndRepairMutation(repairChance))
                .ThenAfterGeneratingApply(new OneFifthFitnessDependentLambda(learningRate)),
            Filter = new BitElitismGenerationFilter<IBitIndividual>(true)
        };
    }
}
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Algorithm.Statistics;
using EvolutionaryAlgorithm.Template.Asymmetric;
using EvolutionaryAlgorithm.Template.Endogenous;
using EvolutionaryAlgorithm.Template.Stagnation;

namespace EvolutionaryAlgorithm.Template
{
    public static class Extensions
    {
        public static IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> UsingAsymmetricGeneration(
            this IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> algorithm,
            int learningRate,
            int observationPhase) =>
            algorithm.UsingGenerationGenerator(new AsymmetricGenerationGenerator(learningRate, observationPhase));

        public static IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> UsingStagnationDetectionGeneration(
            this IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> algorithm,
            int learningRate) =>
            algorithm.UsingHyperHeuristic(new StagnationDetectionHyperHeuristic(learningRate));

        public static IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> UsingEndogenousGeneration(
            this IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> algorithm,
            int learningRate) =>
            algorithm.UsingGenerationGenerator(new EndogenousGenerationGenerator(learningRate));

        public
            public static IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> UsingRandomPopulation(
                this IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> algorithm) =>
            algorithm.UsingPopulation(BitPopulation.FromRandom());

        public static IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> UsingRandomPopulation(
            this IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> algorithm,
            int learningRate) =>
            algorithm.UsingPopulation(EndogenousBitIndividual.FromRandom(learningRate));

        public static IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> UsingBasicStatistics(
            this IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> algorithm) =>
            algorithm.UsingStatistics(new EndogenousBasicEvolutionaryStatistics());

        public static IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> UsingBasicStatistics(
            this IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> algorithm) =>
            algorithm.UsingStatistics(new BasicEvolutionaryStatistics<IBitIndividual, BitArray, bool>());
    }
}
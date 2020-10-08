using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Statistics;
using EvolutionaryAlgorithm.Template.Asymmetric;
using EvolutionaryAlgorithm.Template.Endogenous;
using EvolutionaryAlgorithm.Template.OneLambdaLambda;
using EvolutionaryAlgorithm.Template.Stagnation;

namespace EvolutionaryAlgorithm.Template
{
    public static class Extensions
    {
        public static IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> UsingRandomPopulation(
            this IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> algorithm)
        {
            var random = new Random();
            return algorithm.UsingPopulation(
                new BitPopulation<IBitIndividual>(g => new BitIndividual(g, () => random.NextDouble() >= 0.5)));
        }

        public static IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> UsingEndogenousRandomPopulation(
            this IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> algorithm,
            int mutationRate) =>
            algorithm.UsingPopulation(EndogenousBitIndividual.FromRandom(mutationRate));

        public static IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> UsingBasicStatistics(
            this IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> algorithm) =>
            algorithm.UsingStatistics(new EndogenousBasicEvolutionaryStatistics());

        public static IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> UsingBasicStatistics(
            this IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> algorithm) =>
            algorithm.UsingStatistics(new BasicEvolutionaryStatistics<IBitIndividual, BitArray, bool>());
    }
}
﻿using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.Template.Stagnation
{
    public static class StagnationExtensions
    {
        public static IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> UsingStagnationStatistics(
            this IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> algorithm) =>
            algorithm.UsingStatistics(new StagnationStatistics());
    }

    public class StagnationStatistics : BasicEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool>
    {
        public override string ToString()
        {
            var a = (StagnationDetectionHyperHeuristic) Algorithm.HyperHeuristic;
            return
                $"Runtime: {(DateTime.Now - StartTime).TotalSeconds} seconds, Generations: {Generations}, Fitness: {Algorithm.Best.Fitness}, MutationRate: {Algorithm.Parameters.MutationRate}, In stagnation: {a.InStagnationDetection}, Progress: {100 * a.At / a.Limit:N2}% ({a.At} of {a.Limit})";
        }
    }
}
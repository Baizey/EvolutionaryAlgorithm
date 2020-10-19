using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.Template.Asymmetric
{
    public class AsymmetricBasicEvolutionaryStatistics<T>
        : BasicEvolutionaryStatistics<T, BitArray, bool>
        where T : IBitIndividual
    {
        public override string ToString()
        {
            var mutation = (AsymmetricMutation) Algorithm.HyperHeuristic[0].Mutator.Mutations[1];
            return
                $"Runtime: {(DateTime.Now - StartTime).TotalSeconds} seconds, Generations: {Generations}, Fitness: {Algorithm.Best.Fitness}, MutationRate: {Algorithm.Parameters.MutationRate}, B: {mutation.B}, Counter: {mutation.ObservationCounter} Zero: {mutation.R0}, One: {mutation.R1}";
        }
    }
}
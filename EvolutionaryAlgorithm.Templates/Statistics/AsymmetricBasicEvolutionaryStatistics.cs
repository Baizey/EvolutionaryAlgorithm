using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Statistics;
using EvolutionaryAlgorithm.Template.Mutations;

namespace EvolutionaryAlgorithm.Template.Statistics
{
    public class AsymmetricBasicEvolutionaryStatistics<T>
        : BasicEvolutionaryStatistics<T, BitArray, bool>
        where T : IBitIndividual
    {
        public override string ToString()
        {
            var mutation = (AsymmetricMutation) Algorithm.HyperHeuristic[0].Mutator.Mutations[1];
            return
                $"Runtime: {(DateTime.Now - StartTime).TotalSeconds} seconds, Generations: {Generations}, Fitness: {Algorithm.Best.Fitness}, MutationRate: {Algorithm.Parameters.MutationRate}, B: {mutation.ObservationResult}, Counter: {mutation.ObservationCounter} Zero: {mutation.ZeroFocus}, One: {mutation.OneFocus}";
        }
    }
}
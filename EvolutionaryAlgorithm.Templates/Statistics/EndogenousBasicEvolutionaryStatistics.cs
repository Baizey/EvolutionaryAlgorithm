using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.Template.Statistics
{
    public class EndogenousBasicEvolutionaryStatistics
        : BasicEvolutionaryStatistics<IBitIndividual, bool[], bool>
    {
        public override string ToString() =>
            $"Runtime: {(DateTime.Now - StartTime).TotalSeconds} seconds, Generations: {Generations}, Fitness: {Algorithm.Best.Fitness}, MutationRate: {Algorithm.Best.MutationRate}";
    }
}
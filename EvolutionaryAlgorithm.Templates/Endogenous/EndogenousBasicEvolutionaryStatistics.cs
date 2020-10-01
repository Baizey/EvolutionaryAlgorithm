using System;
using System.Collections;
using EvolutionaryAlgorithm.Core.Algorithm.Statistics;

namespace EvolutionaryAlgorithm.Template.Endogenous
{
    public class EndogenousBasicEvolutionaryStatistics
        : BasicEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool>
    {
        public override string ToString() =>
            $"Runtime: {(DateTime.Now - StartTime).TotalSeconds} seconds, Generations: {Generations}, Fitness: {Algorithm.Best.Fitness}, MutationRate: {Algorithm.Best.MutationRate}";
    }
}
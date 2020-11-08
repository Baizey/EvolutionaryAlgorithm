using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.FitnessFunctions.Graph;

namespace EvolutionaryAlgorithm.GUI.Models.Extensions
{
    public static class StatisticsExtensions
    {
        public static StatisticsView MapToStatisticsView(
            this IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> algorithm, SimpleGraph graph) =>
            new StatisticsView(algorithm, graph);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Template.FitnessFunctions.Graph;
using EvolutionaryAlgorithm.Template.Mutations;

namespace EvolutionaryAlgorithm.GUI.Models
{
    public class StatisticsView
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IParameters Parameters { get; set; }
        public IndividualViewView Best { get; set; }
        public IndividualViewView Current { get; set; }
        public IndividualViewView Previous { get; set; }
        public long StagnantGeneration { get; set; }
        public long Generations { get; set; }
        public bool InStagnation { get; set; }
        public double StagnationProgress { get; set; }
        public double R0 { get; set; }
        public double R1 { get; set; }
        public IEnumerable<ViewEdge> Edges { get; set; }

        public StatisticsView(IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> algorithm,
            SimpleGraph graph)
        {
            var statistics = algorithm.Statistics;
            Parameters = (IParameters) algorithm.Parameters.Clone();
            StartTime = statistics.StartTime;
            EndTime = statistics.EndTime ?? DateTime.Now;
            Best = statistics.Best == null ? null : new IndividualViewView(statistics.Best);
            Current = statistics.Best == null ? null : new IndividualViewView(statistics.Current);
            Previous = statistics.Best == null ? null : new IndividualViewView(statistics.Previous);
            StagnantGeneration = statistics.StagnantGeneration;
            Generations = statistics.Generations;

            foreach (var m in algorithm.HyperHeuristic.States.SelectMany(g => g.Mutator.Mutations))
                if (m is AsymmetricMutation mutation)
                {
                    R0 = mutation.ZeroFocus;
                    R1 = mutation.OneFocus;
                }

            if (algorithm.HyperHeuristic is StagnationDetectionHyperHeuristic heuristic)
            {
                InStagnation = heuristic.InStagnationDetection;
                StagnationProgress = heuristic.At / heuristic.Limit;
            }

            if (graph != null && Current != null)
                Edges = graph.Edges
                    .Where(t => Current.Genes[t.Id])
                    .Select(t => new ViewEdge(t))
                    .ToList();
        }
    }
}
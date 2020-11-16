using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Terminations;
using EvolutionaryAlgorithm.GUI.Models.Enums;
using EvolutionaryAlgorithm.Template;
using EvolutionaryAlgorithm.Template.FitnessFunctions;

namespace EvolutionaryAlgorithm
{
    public static class Benchmark
    {
        public static async Task RunBenchmark(
            FitnessFunctions? fitness = null,
            Heuristics? heuristic = null,
            int geneStep = 100,
            int mutationRate = 2,
            double learningRate = 2,
            int? mu = null,
            int? lambda = null,
            int? observationPhase = null,
            double? repairChance = null,
            double? beta = null,
            int? jump = null,
            int? limitFactor = null)
        {
            var filename = $"{heuristic}_{fitness}_{mutationRate}_{learningRate}";
            if (lambda != null) filename += $"_{lambda}";
            if (observationPhase != null) filename += $"_{observationPhase}";
            if (beta != null) filename += $"_{beta}";
            if (repairChance != null) filename += $"_{repairChance}";
            if (limitFactor != null) filename += $"_{limitFactor}";
            if (jump != null) filename += $"_{jump}";

            var range = new List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>>();
            var toCords = new List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, Point>>();
            for (var i = 0; i < 20; i++)
            {
                var g = (i + 1) * geneStep;
                range.Add(() => new BitEvolutionaryAlgorithm<IBitIndividual>()
                    .UsingBasicStatistics()
                    .UsingRandomPopulation(mutationRate)
                    .UsingEvaluation(CreateFitness(fitness, jump ?? 5))
                    .UsingParameters(new Parameters
                    {
                        Mu = mu ?? 1,
                        Lambda = lambda ?? 1,
                        GeneCount = g,
                        MutationRate = mutationRate
                    })
                    .UsingHeuristic(CreateHeuristic(
                        heuristic, learningRate, mutationRate, observationPhase ?? 10,
                        repairChance ?? 1, beta ?? 1.5D, limitFactor ?? 1)));

                toCords.Add(algo => new Point(g, (int) algo.Statistics.Generations));
            }

            await Benchmark.Range(filename, range, toCords);
        }

        private static IHyperHeuristic<IBitIndividual, BitArray, bool> CreateHeuristic(Heuristics? heuristic,
            double learningRate = 0.05D,
            int mutationRate = 2,
            int observationPhase = 10,
            double repairChance = 1,
            double beta = 1.5,
            int limitFactor = 1)
        {
            return heuristic switch
            {
                Heuristics.Asymmetric => new SimpleHeuristic<IBitIndividual, BitArray, bool>(
                    PresetGenerator.Asymmetric(learningRate, observationPhase)),
                Heuristics.Repair => new SimpleHeuristic<IBitIndividual, BitArray, bool>(
                    PresetGenerator.Repair((int) learningRate, repairChance)),
                Heuristics.SingleEndogenous => new SimpleHeuristic<IBitIndividual, BitArray, bool>(
                    PresetGenerator.SingleEndogenous((int) learningRate)),
                Heuristics.MultiEndogenous => new SimpleHeuristic<IBitIndividual, BitArray, bool>(
                    PresetGenerator.MultiEndogenous((int) learningRate)),
                Heuristics.HeavyTail => new SimpleHeuristic<IBitIndividual, BitArray, bool>(
                    PresetGenerator.HeavyTail((int) learningRate, beta)),
                Heuristics.StagnationDetection => PresetGenerator.StagnationDetection(mutationRate, limitFactor,
                    (int) learningRate),
                _ => throw new InvalidEnumArgumentException()
            };
        }

        private static IBitFitness<IBitIndividual> CreateFitness(
            FitnessFunctions? fitnessFunction,
            int jump = 0)
        {
            return fitnessFunction switch
            {
                FitnessFunctions.OneMax => new OneMaxFitness<IBitIndividual>(),
                FitnessFunctions.Jump => new JumpFitness<IBitIndividual>(jump),
                FitnessFunctions.LeadingOnes => new LeadingOnesFitness<IBitIndividual>(),
                FitnessFunctions.MinimumSpanningTree => throw new NotImplementedException(),
                FitnessFunctions.Satisfiability => throw new NotImplementedException(),
                _ => throw new InvalidEnumArgumentException()
            };
        }

        private static async Task Range(
            string filename,
            List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>> generator,
            List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, Point>> toCords,
            int rounds = 1000)
        {
            await using var file = new StreamWriter($"{filename}.txt");
            var counter = 0;
            var start = DateTime.Now;
            var total = rounds * generator.Count;
            Console.WriteLine($"Progress: 0 / {total} (0%)");
            for (var j = 0; j < generator.Count; j++)
            {
                var tasks = new List<Task>();
                var algorithms = new List<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>();
                var toCordsUnrolled = new List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, Point>>();

                for (var i = 0; i < rounds; i++)
                {
                    var algo = generator[j].Invoke();
                    algorithms.Add(algo);
                    toCordsUnrolled.Add(toCords[j]);
                    var gene = algo.Parameters.GeneCount;
                    tasks.Add(algo.EvolveAsync(new FitnessTermination<IBitIndividual, BitArray, bool>(gene))
                        .ContinueWith(
                            _ =>
                            {
                                var c = Interlocked.Increment(ref counter);
                                var used = (DateTime.Now - start).TotalMilliseconds / c;
                                var remaining = TimeSpan.FromMilliseconds((total - c) * used);
                                var progress = 100 * c / total;
                                if (c % 10 == 0)
                                    Console.WriteLine($"Progress: {c} / {total} ({progress}%) ~{remaining} remaining");
                            }));
                }

                await Task.WhenAll(tasks);
                for (var i = 0; i < algorithms.Count; i++)
                {
                    var point = toCordsUnrolled[i].Invoke(algorithms[i]);
                    await file.WriteLineAsync($"{point.X} {point.Y}");
                }

                await file.FlushAsync();
            }

            Console.WriteLine("Done...");
        }
    }
}
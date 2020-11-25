﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        public const int MaxThreads = 16;

        public static async Task RunBenchmark(
            int mode,
            FitnessFunctions? fitness = null,
            Heuristics? heuristic = null,
            int stepSize = 100,
            int steps = 20,
            int mutationRate = 2,
            Func<int, int> mutationRateFunc = null,
            string mutationRateString = null,
            double learningRate = 2,
            int mu = 1,
            Func<int, int> muFunc = null,
            string muString = null,
            int lambda = 2,
            Func<int, int> lambdaFunc = null,
            string lambdaString = null,
            int? observationPhase = null,
            int repairChance = 1,
            Func<int, int> repairChanceFunc = null,
            string repairChanceString = null,
            double? beta = null,
            int? jump = null,
            int? limitFactor = null)
        {
            var filename = $"{mode}_{heuristic}_{fitness}_{learningRate}";
            Console.WriteLine(filename);
            if (mutationRateFunc == null) filename += $"_{mutationRate}";
            if (lambdaFunc == null) filename += $"_{lambda}";
            if (observationPhase != null) filename += $"_{observationPhase}";
            if (beta != null) filename += $"_{beta}";
            if (repairChanceFunc == null) filename += $"_{repairChance}";
            if (limitFactor != null) filename += $"_{limitFactor}";
            if (jump != null) filename += $"_{jump}";
            lambdaFunc ??= _ => lambda;
            mutationRateFunc ??= _ => mutationRate;
            repairChanceFunc ??= _ => repairChance;

            muString ??= mu.ToString();
            muFunc ??= _ => mu;

            lambdaString ??= lambda.ToString();
            lambdaFunc ??= _ => lambda;

            mutationRateString ??= mutationRate.ToString();
            mutationRateFunc ??= _ => mutationRate;

            repairChanceString ??= repairChance.ToString();
            repairChanceFunc ??= _ => repairChance;

            await using (var file = new StreamWriter($"{filename}.txt"))
            {
                await file.WriteLineAsync($"{heuristic} {fitness}");
                await file.WriteLineAsync($"lambda {lambdaString}");
                await file.WriteLineAsync($"mu {muString}");
                await file.WriteLineAsync($"mutationRate {mutationRateString}");
                await file.WriteLineAsync($"learningRate {learningRate}");
                if (heuristic == Heuristics.HeavyTail)
                    await file.WriteLineAsync($"beta {beta}");
                if (heuristic == Heuristics.Asymmetric)
                    await file.WriteLineAsync($"observationPhase {observationPhase}");
                if (heuristic == Heuristics.Repair)
                    await file.WriteLineAsync($"repairChance {repairChanceString}");
                if (heuristic == Heuristics.StagnationDetection)
                    await file.WriteLineAsync($"limitFactor {limitFactor}");
                if (fitness == FitnessFunctions.Jump)
                    await file.WriteLineAsync($"jump {jump}");
                await file.WriteLineAsync();
                await file.FlushAsync();
            }

            var range = new List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>>();
            for (var i = 0; i < steps; i++)
            {
                var g = (i + 1) * stepSize;
                var mr = mutationRateFunc.Invoke(g);
                var rr = repairChanceFunc.Invoke(g);
                range.Add(() => new BitEvolutionaryAlgorithm<IBitIndividual>()
                    .UsingBasicStatistics()
                    .UsingRandomPopulation(mr)
                    .UsingEvaluation(CreateFitness(fitness, jump ?? 5))
                    .UsingParameters(new Parameters
                    {
                        Mu = muFunc.Invoke(g),
                        Lambda = lambdaFunc.Invoke(g),
                        GeneCount = g,
                        MutationRate = mr
                    })
                    .UsingHeuristic(CreateHeuristic(
                        heuristic, learningRate, mutationRate, observationPhase ?? 10,
                        rr, beta ?? 1.5D, limitFactor ?? 1)));
            }

            await Benchmark.Range(filename, range);
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
            IReadOnlyList<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>> generators)
        {
            const int rounds = 1024;
            await using var file = new StreamWriter($"{filename}.txt", true);
            await file.WriteLineAsync("GeneCount Generations FitnessCalls");
            var start = DateTime.Now;
            var total = rounds * generators.Count;
            Console.WriteLine($"Progress: 0 / {total} (0%)");
            for (var i = 0; i < generators.Count; i++)
            {
                var algorithms = await ParallelRun(
                    start,
                    i,
                    generators.Count,
                    rounds,
                    generators[i]);
                foreach (var algo in algorithms)
                    await file.WriteLineAsync(
                        $"{algo.Parameters.GeneCount} {algo.Statistics.Generations} {algo.Fitness.Calls}");

                await file.FlushAsync();
            }

            Console.WriteLine("Done...");
        }

        private static async Task<List<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>> ParallelRun(
            DateTime start,
            int generation,
            int generations,
            int rounds,
            Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>> generator)
        {
            var workload = rounds / MaxThreads;

            var algorithms = new List<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>();
            for (var i = 0; i < workload * MaxThreads; i++) algorithms.Add(generator.Invoke());
            var tasks = new List<Task>();

            var total = generations * rounds;
            var count = rounds * generation;
            for (var thread = 0; thread < MaxThreads; thread++)
            {
                var s = thread * workload;
                var e = s + workload;
                tasks.Add(Task.Run(async () =>
                {
                    for (var i = s; i < e; i++)
                    {
                        await algorithms[i]
                            .Evolve(new FitnessTermination<IBitIndividual, BitArray, bool>(
                                algorithms[i].Parameters.GeneCount));
                        Interlocked.Increment(ref count);
                        
                        if (count % 64 != 0) continue;
                        
                        var used = (DateTime.Now - start).TotalMilliseconds / count;
                        var remaining = TimeSpan.FromMilliseconds((total - count) * used);
                        Console.WriteLine(
                            $"Progress: {count} / {total} ({100 * count / total}%), Taken: {DateTime.Now - start}; Remaining: ~{remaining}");
                    }
                }));
            }

            await Task.WhenAll(tasks);
            return algorithms;
        }

        private static async Task<List<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>> ParallelQueue(
            DateTime start,
            int generation,
            int generations,
            int rounds,
            Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>> generator)
        {
            var previousDone = generation * rounds;
            var total = generations * rounds;
            var pending = rounds;
            var working = new List<Task>();
            var algorithms = new List<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>();
            while (pending + working.Count > 0)
            {
                if (working.Count < MaxThreads && pending > 0)
                {
                    var algo = generator.Invoke();
                    algorithms.Add(algo);
                    var gene = algo.Parameters.GeneCount;
                    working.Add(algo.EvolveAsync(new FitnessTermination<IBitIndividual, BitArray, bool>(gene)));
                    pending--;
                }
                else
                {
                    await Task.WhenAny(working);
                    working.RemoveAll(x => x.IsCompleted);
                    var done = rounds - (pending + working.Count);
                    var used = (DateTime.Now - start).TotalMilliseconds / done;
                    var remaining = TimeSpan.FromMilliseconds((total - done) * used);
                    var progress = 100 * (previousDone + done) / total;
                    if (done % 100 == 0)
                        Console.WriteLine(
                            $"Progress: {previousDone + done} / {total} ({progress}%) ~{DateTime.Now - start} taken");
                }
            }

            return algorithms;
        }
    }
}
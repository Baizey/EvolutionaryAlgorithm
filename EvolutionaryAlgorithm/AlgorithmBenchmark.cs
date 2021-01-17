using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Terminations;
using EvolutionaryAlgorithm.GUI.Models.Enums;
using EvolutionaryAlgorithm.Template;
using EvolutionaryAlgorithm.Template.FitnessFunctions;
using EvolutionaryAlgorithm.Template.FitnessFunctions.Graph;
using EvolutionaryAlgorithm.Template.FitnessFunctions.SatisfiabilityProblem;

namespace EvolutionaryAlgorithm
{
    public static class AlgorithmBenchmark
    {
        public const int MaxThreads = 16;

        public static async Task RunBenchmark(
            int mode,
            FitnessFunctions? fitness = null,
            Heuristics? heuristic = null,
            int stepSize = 100,
            int steps = 10,
            double mutationRate = 2,
            Func<double, double> mutationRateFunc = null,
            string mutationRateString = null,
            double learningRate = 2,
            int mu = 1,
            Func<int, int> muFunc = null,
            string muString = null,
            int lambda = 2,
            Func<int, int> lambdaFunc = null,
            string lambdaString = null,
            int? observationPhase = null,
            double repairChance = 1,
            Func<double, double> repairChanceFunc = null,
            string repairChanceString = null,
            double? beta = null,
            int rounds = 100,
            int? jump = null,
            int? limitFactor = null,
            int? seed = null,
            double? formulaRatio = null,
            double? edgeChance = null,
            long? budget = null,
            Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, ITermination<IBitIndividual, BitArray, bool>>
                termination = null)
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
            if (seed != null) filename += $"_{seed}";
            if (formulaRatio != null) filename += $"_{formulaRatio}";
            if (edgeChance != null) filename += $"_{edgeChance}";
            if (budget != null) filename += $"_{budget}";

            muString ??= mu.ToString();
            muFunc ??= _ => mu;

            lambdaString ??= lambda.ToString();
            lambdaFunc ??= _ => lambda;

            mutationRateString ??= mutationRate.ToString(CultureInfo.InvariantCulture);
            mutationRateFunc ??= _ => mutationRate;

            repairChanceString ??= repairChance.ToString(CultureInfo.InvariantCulture);
            repairChanceFunc ??= _ => repairChance;

            var file = new StreamWriter($"{filename}.txt");
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
            if (fitness == FitnessFunctions.Satisfiability || fitness == FitnessFunctions.MinimumSpanningTree)
                await file.WriteLineAsync($"seed {seed}");
            if (fitness == FitnessFunctions.Satisfiability)
                await file.WriteLineAsync($"formula_ratio {formulaRatio}");
            if (fitness == FitnessFunctions.MinimumSpanningTree)
                await file.WriteLineAsync($"edge_chance {edgeChance}");
            await file.WriteLineAsync();
            if (fitness == FitnessFunctions.MinimumSpanningTree)
                await file.WriteLineAsync("Nodes GeneCount Generations FitnessCalls Fitness Runtime");
            else
                await file.WriteLineAsync("GeneCount Generations FitnessCalls Fitness Runtime");
            await file.FlushAsync();

            for (var i = 0; i < steps; i++)
            {
                var done = i * rounds;
                var geneCount = (i + 1) * stepSize;
                var nodes = geneCount;
                SimpleGraph graph = null;
                if (fitness == FitnessFunctions.MinimumSpanningTree)
                {
                    graph = new SimpleGraph(nodes, (double) edgeChance, seed: seed, randomDistanceCost: true);
                    geneCount = graph.Edges.Count;
                }

                var mr = mutationRateFunc.Invoke(geneCount);
                var rr = repairChanceFunc.Invoke(geneCount);
                await RunBenchmarks(termination, file, done, steps, rounds, graph, () =>
                    new BitEvolutionaryAlgorithm<IBitIndividual>()
                        .UsingBasicStatistics()
                        .UsingRandomPopulation(mr)
                        .UsingEvaluation(CreateFitness(fitness,
                            geneCount: geneCount,
                            jump: jump ?? 5,
                            formulaRatio: formulaRatio ?? 1,
                            graph: graph,
                            seed: seed ?? 0))
                        .UsingParameters(new Parameters
                        {
                            Mu = muFunc.Invoke(geneCount),
                            Lambda = lambdaFunc.Invoke(geneCount),
                            GeneCount = geneCount,
                            MutationRate = mr
                        })
                        .UsingHeuristic(CreateHeuristic(
                            heuristic, learningRate, mutationRate, observationPhase ?? 10,
                            rr, beta ?? 1.5D, limitFactor ?? 1)));
            }
        }

        private static async Task RunBenchmarks(
            Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, ITermination<IBitIndividual, BitArray, bool>>
                termination,
            TextWriter file,
            int oldDone,
            int steps,
            int rounds,
            SimpleGraph graph,
            Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>> generator)
        {
            var completed = 0;
            var working = new List<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>();
            var start = DateTime.Now;
            while (completed < rounds || working.Count > 0)
            {
                while (working.Count < MaxThreads && completed < rounds)
                {
                    completed++;
                    var algo = generator.Invoke();
                    working.Add(algo);
                    algo.EvolveAsync(termination.Invoke(algo));
                }

                await Task.WhenAny(working.Select(e => e.AsyncRunner));
                var tasksCompleted = working.Where(e => e.AsyncRunner.IsCompleted).ToList();
                tasksCompleted.ForEach(algo =>
                {
                    working.Remove(algo);
                    file.WriteLine(
                        graph == null
                            ? $"{algo.Parameters.GeneCount} {algo.Statistics.Generations} {algo.Fitness.Calls} {algo.Statistics.Best.Fitness} {algo.Statistics.RunTime}"
                            : $"{graph.Nodes.Count} {algo.Parameters.GeneCount} {algo.Statistics.Generations} {algo.Fitness.Calls} {algo.Statistics.Best.Fitness} {algo.Statistics.RunTime}");
                });
                await file.FlushAsync();
                
                var total = rounds * steps;
                var done = oldDone + completed - working.Count;
                var progress = 100 * done / total;
                if (done % (rounds / 10) == 0 || working.Count < 10)
                    Console.WriteLine(
                        $"Progress: {done} / {total} ({progress}%) {DateTime.Now} | {DateTime.Now - start} taken");
            }
        }

        private static IHyperHeuristic<IBitIndividual, BitArray, bool> CreateHeuristic(Heuristics? heuristic,
            double learningRate = 0.05D,
            double mutationRate = 2,
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
            int jump = 0,
            SimpleGraph graph = null,
            int geneCount = 0,
            double formulaRatio = 1,
            int? seed = null)
        {
            return fitnessFunction switch
            {
                FitnessFunctions.OneMax => new OneMaxFitness<IBitIndividual>(),
                FitnessFunctions.Jump => new JumpFitness<IBitIndividual>(jump),
                FitnessFunctions.LeadingOnes => new LeadingOnesFitness<IBitIndividual>(),
                FitnessFunctions.MinimumSpanningTree => new MinimumSpanningTreeFitness<IBitIndividual>(graph),
                FitnessFunctions.Satisfiability => new SatisfiabilityProblemFitness<IBitIndividual>(
                    seed == null
                        ? SatisfiabilityProblem.Generate(geneCount, formulaRatio)
                        : SatisfiabilityProblem.Generate(new Random((int) seed), geneCount, formulaRatio)),
                _ => throw new InvalidEnumArgumentException()
            };
        }
    }
}
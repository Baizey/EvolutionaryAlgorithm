﻿using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.GUI.Models.Enums;
using static EvolutionaryAlgorithm.Benchmark;
using static EvolutionaryAlgorithm.GUI.Models.Enums.FitnessFunctions;
using static EvolutionaryAlgorithm.GUI.Models.Enums.Heuristics;

namespace EvolutionaryAlgorithm
{
    public static class Program
    {
        // mr = mutation rate
        // lr = learning rate
        // rr = repair rate
        // op = observation phase

        // Asymmetric was tested on OneMax with mr: 1, lr: 0.1, op: 50 (https://arxiv.org/pdf/2006.09126.pdf)
        // S-endogenous was theorised on OneMax, λ: (ln n)^(1+ϵ), lr: 32 (https://dl.acm.org/doi/10.1145/3205455.3205569) O(n/log λ + (n logn)/λ)
        // M-endogenous - none
        // Repair was theorized on jump, with jump: k <= n/16, mr: sqrt(k/n), rr: sqrt(k/n), λ: sqrt(n/k)^k (https://arxiv.org/pdf/2004.06702.pdf)
        // Stagnation was tested on Jump with jump: 4, n: 40-160, λ: ln(n)
        // Stagnation was tested on Jump with jump: 3, n: 200-1000, λ: ln(n)
        // HeavyTail was tested on Jump with n: 20-160, jump: 8, λ: 1, beta: 1.5, 2, 3, 4 lr: 1 (i.e no learning) (https://arxiv.org/pdf/1703.03334.pdf)

        public static async Task Main(string[] args)
        {
            const double formulaRatio = 4.5D;
            var mode = int.Parse(args[0]);
            var benchmarks = new Func<int, Task>[]
            {
                i => RunBenchmark(i,
                    heuristic: Asymmetric, fitness: Satisfiability,
                    stepSize: 100, steps: 10, seed: 0,
                    mu: 1, lambda: 1,
                    formulaRatio: formulaRatio,
                    mutationRate: 1, learningRate: 0.1, observationPhase: 50
                ),

                // TODO: correct mr and rr
                i => RunBenchmark(i,
                    heuristic: Repair, fitness: Satisfiability,
                    stepSize: 100, steps: 10, seed: 0, formulaRatio: formulaRatio,
                    mu: 1, lambda: 2,
                    mutationRateString: "",
                    mutationRateFunc: n => 50,
                    repairChanceString: "",
                    repairChanceFunc: n => 50,
                    learningRate: 2
                ),

                i => RunBenchmark(i,
                    heuristic: Heuristics.HeavyTail, fitness: Satisfiability,
                    stepSize: 100, steps: 10, seed: 0, formulaRatio: formulaRatio,
                    mu: 1, lambda: 2,
                    learningRate: 2, mutationRate: 1
                ),

                // TODO: correct lambda and mu
                i => RunBenchmark(i,
                    heuristic: Heuristics.MultiEndogenous, fitness: Satisfiability,
                    stepSize: 100, steps: 10, seed: 0, formulaRatio: formulaRatio,
                    mu: 1, lambda: 1,
                    mutationRate: 2, learningRate: 2
                ),

                // TODO: correct lambda
                i => RunBenchmark(i,
                    heuristic: Heuristics.SingleEndogenous, fitness: Satisfiability,
                    stepSize: 100, steps: 10, seed: 0, formulaRatio: formulaRatio,
                    mu: 1, lambda: 1,
                    mutationRate: 2, learningRate: 2
                ),

                // TODO: correct lambda
                i => RunBenchmark(i,
                    heuristic: Heuristics.StagnationDetection, fitness: Satisfiability,
                    stepSize: 100, steps: 10, seed: 0, formulaRatio: formulaRatio,
                    mu: 1, lambda: 1,
                    mutationRate: 2, learningRate: 2
                ),
            };
            if (0 > mode) Console.WriteLine($"Benchmarks: {benchmarks.Length} (0...{benchmarks.Length - 1})");
            else await benchmarks[mode].Invoke(mode);
        }
    }
}
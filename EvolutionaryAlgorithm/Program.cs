using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Terminations;
using EvolutionaryAlgorithm.GUI.Models.Enums;
using EvolutionaryAlgorithm.Template.Mutations;
using static EvolutionaryAlgorithm.AlgorithmBenchmark;
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
        // Repair was theorized on jump, with jump: k <= n/16, mr: sqrt(k/n), rr: sqrt(k/n), λ: (n/k)^(1/k)*(2^k) (https://arxiv.org/pdf/2004.06702.pdf)

        // Repair was theorized on jump, with jump: k <= n/16, mr: sqrt(k/n), rr: sqrt(k/n), λ: sqrt(n/k)^k * (1/sqrt(n)) (https://arxiv.org/pdf/2004.06702.pdf)

        // Stagnation was tested on Jump with jump: 4, n: 40-160, λ: ln(n)
        // Stagnation was tested on Jump with jump: 3, n: 200-1000, λ: ln(n)
        // HeavyTail was tested on Jump with n: 20-160, jump: 8, λ: 1, beta: 1.5, 2, 3, 4 lr: 1 (i.e no learning) (https://arxiv.org/pdf/1703.03334.pdf)

        public static async Task Main(string[] args)
        {
            const long fitnessCallsBudget = 300000;
            const int seed1 = 1;
            const double formulaRatio1 = 4.5D;

            const int seed2 = 0;
            const double formulaRatio2 = 4.5D;
            var lookup2 = new Dictionary<int, double>
            {
                {20, 89},
                {40, 180},
                {60, 269},
                {80, 360},
                {100, 450},
                {120, 540},
                {140, 629},
                {160, 719},
                {180, 808},
                {200, 899}
            };
            var timeBudget = TimeSpan.FromSeconds(4);
            const int seed = 0;
            const double chance = 0.25D;
            const double chance2 = 0.5D;
            const double chance3 = 0.75D;
            const int stepSize = 1000;
            const int steps = 10;
            const int k = 2;
            var mode = int.Parse(args[0]);
            var benchmarks = new Func<int, Task>[]
            {
                i =>
                {
                    return RunBenchmark(i,
                        heuristic: SingleEndogenous, fitness: LeadingOnes,
                        rounds: 100, stepSize: 100, steps: 20,
                        termination: a =>
                            new FitnessTermination<IBitIndividual, BitArray, bool>(a.Parameters.GeneCount),
                        mu: 1,
                        mutationRate: 2,
                        learningRate: 2,
                        lambdaFunc: n => (int) (2 + 2 * Math.Log(n)),
                        lambdaString: "2+2*ln(n)"
                    );
                },
                i =>
                {
                    return RunBenchmark(i,
                        heuristic: SingleEndogenous, fitness: OneMax,
                        rounds: 100, stepSize: 100, steps: 20,
                        termination: a =>
                            new FitnessTermination<IBitIndividual, BitArray, bool>(a.Parameters.GeneCount),
                        mu: 1,
                        mutationRate: 2,
                        learningRate: 2,
                        lambdaFunc: n => (int) (2 + 2 * Math.Log(n)),
                        lambdaString: "2+2*ln(n)"
                    );
                },

                i =>
                {
                    return RunBenchmark(i,
                        heuristic: Repair, fitness: LeadingOnes,
                        rounds: 100, stepSize: 100, steps: 10,
                        termination: a =>
                            new FitnessTermination<IBitIndividual, BitArray, bool>(a.Parameters.GeneCount),
                        mu: 1, lambda: 1,
                        repairChanceString: "sqrt(1/n)",
                        mutationRateString: "sqrt(1/n)",
                        repairChanceFunc: Math.Sqrt, // rewriting sqrt(k/n) = x/n
                        mutationRateFunc: Math.Sqrt, // rewriting sqrt(k/n) = x/n
                        learningRate: 2
                    );
                },
                i =>
                {
                    var k = 2;
                    return RunBenchmark(i,
                        heuristic: Repair, fitness: Jump,
                        rounds: 100, stepSize: 200, steps: 10, jump: k,
                        termination: a =>
                            new FitnessTermination<IBitIndividual, BitArray, bool>(a.Parameters.GeneCount),
                        mu: 1,
                        learningRate: 1,
                        repairChanceString: "sqrt(k/n)",
                        mutationRateString: "sqrt(k/n)",
                        repairChanceFunc: n => Math.Sqrt(n * k), // rewriting sqrt(k/n) = x/n
                        mutationRateFunc: n => Math.Sqrt(n * k), // rewriting sqrt(k/n) = x/n
                        lambdaFunc: n => (int) ((1D / Math.Sqrt(n)) * Math.Pow(Math.Sqrt((double) n / k), k)),
                        lambdaString: "(1/sqrt(n))*sqrt(n/k)^k"
                    );
                },
                i =>
                {
                    var k = 3;
                    return RunBenchmark(i,
                        heuristic: Repair, fitness: Jump,
                        rounds: 100, stepSize: 200, steps: 10, jump: k,
                        termination: a =>
                            new FitnessTermination<IBitIndividual, BitArray, bool>(a.Parameters.GeneCount),
                        mu: 1,
                        learningRate: 1,
                        repairChanceString: "sqrt(k/n)",
                        mutationRateString: "sqrt(k/n)",
                        repairChanceFunc: n => Math.Sqrt(n * k), // rewriting sqrt(k/n) = x/n
                        mutationRateFunc: n => Math.Sqrt(n * k), // rewriting sqrt(k/n) = x/n
                        lambdaFunc: n => (int) ((1D / Math.Sqrt(n)) * Math.Pow(Math.Sqrt((double) n / k), k)),
                        lambdaString: "(1/sqrt(n))*sqrt(n/k)^k"
                    );
                },
                i =>
                {
                    var k = 4;
                    return RunBenchmark(i,
                        heuristic: Asymmetric, fitness: Jump,
                        rounds: 1000, stepSize: 1000, steps: 20, jump: k,
                        termination: a =>
                            new FitnessTermination<IBitIndividual, BitArray, bool>(a.Parameters.GeneCount),
                        mu: 1, lambda: 1,
                        mutationRate: 1, learningRate: 0.1, observationPhase: 50
                    );
                },


                i => RunBenchmark(i,
                    heuristic: Asymmetric, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance,
                    termination: a => new TimeoutTermination<IBitIndividual, BitArray, bool>(timeBudget),
                    mu: 1, lambda: 1,
                    mutationRate: 1, learningRate: 0.1, observationPhase: 50
                ),
                i => RunBenchmark(i,
                    heuristic: Asymmetric, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed1, formulaRatio: formulaRatio1,
                    termination: a => new TimeoutTermination<IBitIndividual, BitArray, bool>(timeBudget),
                    mu: 1, lambda: 1,
                    mutationRate: 1, learningRate: 0.1, observationPhase: 50
                ),
                i => RunBenchmark(i,
                    heuristic: Asymmetric, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance,
                    termination: a => new TimeoutTermination<IBitIndividual, BitArray, bool>(timeBudget),
                    mu: 1, lambda: 1,
                    mutationRate: 1, learningRate: 0, observationPhase: 50
                ),
                i => RunBenchmark(i,
                    heuristic: Asymmetric, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed1, formulaRatio: formulaRatio1,
                    termination: a => new TimeoutTermination<IBitIndividual, BitArray, bool>(timeBudget),
                    mu: 1, lambda: 1,
                    mutationRate: 1, learningRate: 0, observationPhase: 50
                ),
                i => MutationBenchmark.Test(
                    () => new AsymmetricMutation(0.1, 50),
                    mode: i,
                    name: "Asymmetric",
                    timeBudget: timeBudget),
                i => MutationBenchmark.Test(
                    () => new SelfAdjustingMutation(),
                    mode: i,
                    name: "Normal",
                    timeBudget: timeBudget),
            };
            if (0 > mode) Console.WriteLine($"Benchmarks: {benchmarks.Length} (0...{benchmarks.Length - 1})");
            else await benchmarks[mode].Invoke(mode);
        }

        public static async Task MainMutation(string[] args)
        {
            const long fitnessCallsBudget = 300000;
            var timeBudget = TimeSpan.FromSeconds(5);
            const int seed = 0;
            const double chance = 0.25D;
            const double chance2 = 0.5D;
            const double chance3 = 0.75D;
            const int stepSize = 10;
            const int steps = 10;
            const int k = 2;
            var mode = int.Parse(args[0]);
            var benchmarks = new Func<int, Task>[]
            {
                i => MutationBenchmark.Test(
                    () => new AsymmetricMutation(0.1, 50),
                    timeBudget: timeBudget),
                i => MutationBenchmark.Test(
                    () => new SelfAdjustingMutation(),
                    timeBudget: timeBudget),
            };
            if (0 > mode) Console.WriteLine($"Benchmarks: {benchmarks.Length} (0...{benchmarks.Length - 1})");
            else await benchmarks[mode].Invoke(mode);
        }

        public static async Task MainMST(string[] args)
        {
            const long fitnessCallsBudget = 300000;
            var timeBudget = TimeSpan.FromMinutes(2);
            const int seed = 0;
            const double chance = 0.25D;
            const double chance2 = 0.5D;
            const double chance3 = 0.75D;
            const int stepSize = 10;
            const int steps = 10;
            const int k = 2;
            var mode = int.Parse(args[0]);
            var benchmarks = new Func<int, Task>[]
            {
                i => RunBenchmark(i,
                    heuristic: Asymmetric, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance,
                    termination: a => new TimeoutTermination<IBitIndividual, BitArray, bool>(timeBudget),
                    mu: 1, lambda: 1,
                    mutationRate: 1, learningRate: 0.1, observationPhase: 50,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: Repair, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance,
                    termination: a => new TimeoutTermination<IBitIndividual, BitArray, bool>(timeBudget),
                    mu: 1, lambda: 2,
                    mutationRateString: "sqrt(k/n)",
                    mutationRateFunc: n => Math.Sqrt(k * n),
                    repairChanceString: "sqrt(k/n)",
                    repairChanceFunc: n => Math.Sqrt(k * n),
                    learningRate: 2,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: HeavyTail, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance,
                    termination: a => new TimeoutTermination<IBitIndividual, BitArray, bool>(timeBudget),
                    mu: 1, lambda: 2,
                    learningRate: 2, mutationRate: 1,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: MultiEndogenous, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance,
                    termination: a => new TimeoutTermination<IBitIndividual, BitArray, bool>(timeBudget),
                    muString: "sqrt(n)",
                    muFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    mutationRate: 2, learningRate: 2,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: SingleEndogenous, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance,
                    termination: a => new TimeoutTermination<IBitIndividual, BitArray, bool>(timeBudget),
                    mu: 1,
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    mutationRate: 2, learningRate: 2,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: StagnationDetection, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance,
                    termination: a => new TimeoutTermination<IBitIndividual, BitArray, bool>(timeBudget),
                    mu: 1,
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => Math.Max(2, (int) Math.Log(n)),
                    mutationRate: 2, learningRate: 2,
                    budget: fitnessCallsBudget
                ),


                i => RunBenchmark(i,
                    heuristic: Asymmetric, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance2,
                    termination: a => new FitnessCallsTermination<IBitIndividual, BitArray, bool>(fitnessCallsBudget),
                    mu: 1, lambda: 1,
                    mutationRate: 1, learningRate: 0.1, observationPhase: 50,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: Repair, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance2,
                    termination: a => new FitnessCallsTermination<IBitIndividual, BitArray, bool>(fitnessCallsBudget),
                    mu: 1, lambda: 2,
                    mutationRateString: "sqrt(k/n)",
                    mutationRateFunc: n => Math.Sqrt(k * n),
                    repairChanceString: "sqrt(k/n)",
                    repairChanceFunc: n => Math.Sqrt(k * n),
                    learningRate: 2,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: HeavyTail, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance2,
                    termination: a => new FitnessCallsTermination<IBitIndividual, BitArray, bool>(fitnessCallsBudget),
                    mu: 1, lambda: 2,
                    learningRate: 2, mutationRate: 1,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: MultiEndogenous, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance2,
                    termination: a => new FitnessCallsTermination<IBitIndividual, BitArray, bool>(fitnessCallsBudget),
                    muString: "sqrt(n)",
                    muFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    mutationRate: 2, learningRate: 2,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: SingleEndogenous, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance2,
                    termination: a => new FitnessCallsTermination<IBitIndividual, BitArray, bool>(fitnessCallsBudget),
                    mu: 1,
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    mutationRate: 2, learningRate: 2,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: StagnationDetection, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance2,
                    termination: a => new FitnessCallsTermination<IBitIndividual, BitArray, bool>(fitnessCallsBudget),
                    mu: 1,
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => Math.Max(2, (int) Math.Log(n)),
                    mutationRate: 2, learningRate: 2,
                    budget: fitnessCallsBudget
                ),


                i => RunBenchmark(i,
                    heuristic: Asymmetric, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance3,
                    termination: a => new FitnessCallsTermination<IBitIndividual, BitArray, bool>(fitnessCallsBudget),
                    mu: 1, lambda: 1,
                    mutationRate: 1, learningRate: 0.1, observationPhase: 50,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: Repair, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance3,
                    termination: a => new FitnessCallsTermination<IBitIndividual, BitArray, bool>(fitnessCallsBudget),
                    mu: 1, lambda: 2,
                    mutationRateString: "sqrt(k/n)",
                    mutationRateFunc: n => Math.Sqrt(k * n),
                    repairChanceString: "sqrt(k/n)",
                    repairChanceFunc: n => Math.Sqrt(k * n),
                    learningRate: 2,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: HeavyTail, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance3,
                    termination: a => new FitnessCallsTermination<IBitIndividual, BitArray, bool>(fitnessCallsBudget),
                    mu: 1, lambda: 2,
                    learningRate: 2, mutationRate: 1,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: MultiEndogenous, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance3,
                    termination: a => new FitnessCallsTermination<IBitIndividual, BitArray, bool>(fitnessCallsBudget),
                    muString: "sqrt(n)",
                    muFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    mutationRate: 2, learningRate: 2,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: SingleEndogenous, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance3,
                    termination: a => new FitnessCallsTermination<IBitIndividual, BitArray, bool>(fitnessCallsBudget),
                    mu: 1,
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    mutationRate: 2, learningRate: 2,
                    budget: fitnessCallsBudget
                ),

                i => RunBenchmark(i,
                    heuristic: StagnationDetection, fitness: MinimumSpanningTree,
                    stepSize: stepSize, steps: steps, seed: seed,
                    edgeChance: chance3,
                    termination: a => new FitnessCallsTermination<IBitIndividual, BitArray, bool>(fitnessCallsBudget),
                    mu: 1,
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => Math.Max(2, (int) Math.Log(n)),
                    mutationRate: 2, learningRate: 2,
                    budget: fitnessCallsBudget
                ),
            };
            if (0 > mode) Console.WriteLine($"Benchmarks: {benchmarks.Length} (0...{benchmarks.Length - 1})");
            else await benchmarks[mode].Invoke(mode);
        }

        public static async Task MainSatisfiability(string[] args)
        {
            const long budget = 30000000;

            const int seed1 = 1;
            const double formulaRatio1 = 3.5D;

            const int seed2 = 0;
            const double formulaRatio2 = 4.5D;
            var lookup2 = new Dictionary<int, double>
            {
                {20, 89},
                {40, 180},
                {60, 269},
                {80, 360},
                {100, 450},
                {120, 540},
                {140, 629},
                {160, 719},
                {180, 808},
                {200, 899}
            };

            const int stepSize = 20;
            const int steps = 10;
            const int k = 2;
            var mode = int.Parse(args[0]);
            var benchmarks = new Func<int, Task>[]
            {
                i => RunBenchmark(i,
                    heuristic: Asymmetric, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed1, formulaRatio: formulaRatio1,
                    termination: a =>
                        new BudgetOrBeforeTermination<IBitIndividual, BitArray, bool>(budget,
                            formulaRatio1 * a.Parameters.GeneCount),
                    mu: 1, lambda: 1,
                    mutationRate: 1, learningRate: 0.1, observationPhase: 50,
                    budget: budget
                ),

                i => RunBenchmark(i,
                    heuristic: Repair, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed1, formulaRatio: formulaRatio1,
                    termination: a =>
                        new BudgetOrBeforeTermination<IBitIndividual, BitArray, bool>(budget,
                            formulaRatio1 * a.Parameters.GeneCount),
                    mu: 1, lambda: 2,
                    mutationRateString: "sqrt(k/n)",
                    mutationRateFunc: n => Math.Sqrt(k * n),
                    repairChanceString: "sqrt(k/n)",
                    repairChanceFunc: n => Math.Sqrt(k * n),
                    learningRate: 2,
                    budget: budget
                ),

                i => RunBenchmark(i,
                    heuristic: HeavyTail, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed1, formulaRatio: formulaRatio1,
                    termination: a =>
                        new BudgetOrBeforeTermination<IBitIndividual, BitArray, bool>(budget,
                            formulaRatio1 * a.Parameters.GeneCount),
                    mu: 1, lambda: 2,
                    learningRate: 2, mutationRate: 1,
                    budget: budget
                ),

                i => RunBenchmark(i,
                    heuristic: MultiEndogenous, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed1, formulaRatio: formulaRatio1,
                    termination: a =>
                        new BudgetOrBeforeTermination<IBitIndividual, BitArray, bool>(budget,
                            formulaRatio1 * a.Parameters.GeneCount),
                    muString: "sqrt(n)",
                    muFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    mutationRate: 2, learningRate: 2,
                    budget: budget
                ),

                i => RunBenchmark(i,
                    heuristic: SingleEndogenous, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed1, formulaRatio: formulaRatio1,
                    termination: a =>
                        new BudgetOrBeforeTermination<IBitIndividual, BitArray, bool>(budget,
                            formulaRatio1 * a.Parameters.GeneCount),
                    mu: 1,
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    mutationRate: 2, learningRate: 2,
                    budget: budget
                ),

                i => RunBenchmark(i,
                    heuristic: StagnationDetection, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed1, formulaRatio: formulaRatio1,
                    termination: a =>
                        new BudgetOrBeforeTermination<IBitIndividual, BitArray, bool>(budget,
                            formulaRatio1 * a.Parameters.GeneCount),
                    mu: 1,
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => Math.Max(2, (int) Math.Log(n)),
                    mutationRate: 2, learningRate: 2,
                    budget: budget
                ),


                i => RunBenchmark(i,
                    heuristic: Asymmetric, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed2, formulaRatio: formulaRatio2,
                    termination: a =>
                        new BudgetOrBeforeTermination<IBitIndividual, BitArray, bool>(budget,
                            lookup2[a.Parameters.GeneCount]),
                    mu: 1, lambda: 1,
                    mutationRate: 1, learningRate: 0.1, observationPhase: 50,
                    budget: budget
                ),

                i => RunBenchmark(i,
                    heuristic: Repair, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed2, formulaRatio: formulaRatio2,
                    termination: a =>
                        new BudgetOrBeforeTermination<IBitIndividual, BitArray, bool>(budget,
                            lookup2[a.Parameters.GeneCount]),
                    mu: 1, lambda: 2,
                    mutationRateString: "sqrt(k/n)",
                    mutationRateFunc: n => Math.Sqrt(k * n),
                    repairChanceString: "sqrt(k/n)",
                    repairChanceFunc: n => Math.Sqrt(k * n),
                    learningRate: 2,
                    budget: budget
                ),

                i => RunBenchmark(i,
                    heuristic: HeavyTail, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed2, formulaRatio: formulaRatio2,
                    termination: a =>
                        new BudgetOrBeforeTermination<IBitIndividual, BitArray, bool>(budget,
                            lookup2[a.Parameters.GeneCount]),
                    mu: 1, lambda: 2,
                    learningRate: 2, mutationRate: 1,
                    budget: budget
                ),

                i => RunBenchmark(i,
                    heuristic: MultiEndogenous, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed2, formulaRatio: formulaRatio2,
                    termination: a =>
                        new BudgetOrBeforeTermination<IBitIndividual, BitArray, bool>(budget,
                            lookup2[a.Parameters.GeneCount]),
                    muString: "sqrt(n)",
                    muFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    mutationRate: 2, learningRate: 2,
                    budget: budget
                ),

                i => RunBenchmark(i,
                    heuristic: SingleEndogenous, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed2, formulaRatio: formulaRatio2,
                    termination: a =>
                        new BudgetOrBeforeTermination<IBitIndividual, BitArray, bool>(budget,
                            lookup2[a.Parameters.GeneCount]),
                    mu: 1,
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => (int) Math.Max(2, Math.Pow(Math.Log(n), 2)),
                    mutationRate: 2, learningRate: 2,
                    budget: budget
                ),

                i => RunBenchmark(i,
                    heuristic: StagnationDetection, fitness: Satisfiability,
                    stepSize: stepSize, steps: steps, seed: seed2, formulaRatio: formulaRatio2,
                    termination: a =>
                        new BudgetOrBeforeTermination<IBitIndividual, BitArray, bool>(budget,
                            lookup2[a.Parameters.GeneCount]),
                    mu: 1,
                    lambdaString: "sqrt(n)",
                    lambdaFunc: n => Math.Max(2, (int) Math.Log(n)),
                    mutationRate: 2, learningRate: 2,
                    budget: budget
                ),
            };
            if (0 > mode) Console.WriteLine($"Benchmarks: {benchmarks.Length} (0...{benchmarks.Length - 1})");
            else await benchmarks[mode].Invoke(mode);
        }
    }
}
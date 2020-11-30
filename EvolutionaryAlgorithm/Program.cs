using System;
using System.Threading.Tasks;
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
        // Repair was theorized on jump, with jump: k <= n/16, mr: p >= 2k/n, rr: sqrt(k/n) (https://arxiv.org/pdf/2004.06702.pdf)
        // Stagnation was tested on Jump with jump: 4, n: 40-160, λ: ln(n)
        // Stagnation was tested on Jump with jump: 3, n: 200-1000, λ: ln(n)
        // HeavyTail was tested on Jump with n: 20-160, jump: 8, λ: 1, beta: 1.5, 2, 3, 4 lr: 1 (i.e no learning) (https://arxiv.org/pdf/1703.03334.pdf)

        public static async Task Main(string[] args)
        {
            var mode = int.Parse(args[0]);
            var benchmarks = new Func<int, Task>[]
            {
                i => RunBenchmark(i, heuristic: Repair, fitness: OneMax, mu: 1, lambda: 2, stepSize: 100,
                    mutationRateString: "sqrt(log(n))",
                    mutationRateFunc: n =>  Math.Sqrt(Math.Log(n)),
                    repairChanceString: "1/(p*n)",
                    repairChanceFunc: n => 1D / Math.Sqrt(Math.Log(n)),
                    learningRate: 2
                ),
                i => RunBenchmark(i, heuristic: Repair, fitness: OneMax, mu: 1, lambda: 2, stepSize: 100,
                    mutationRateString: "sqrt(k/n)",
                    mutationRateFunc: Math.Sqrt,
                    repairChanceString: "sqrt(k/n)",
                    repairChanceFunc: Math.Sqrt,
                    learningRate: 2
                ),

                i => RunBenchmark(i, heuristic: Repair, fitness: LeadingOnes, mu: 1, lambda: 2, stepSize: 100,
                    mutationRateString: "sqrt(log(n))",
                    mutationRateFunc: n =>  Math.Sqrt(Math.Log(n)),
                    repairChanceString: "1/(p*n)",
                    repairChanceFunc: n => 1D / Math.Sqrt(Math.Log(n)),
                    learningRate: 2
                ),
                i => RunBenchmark(i, heuristic: Repair, fitness: LeadingOnes, mu: 1, lambda: 2, stepSize: 100,
                    mutationRateString: "sqrt(k/n)",
                    mutationRateFunc: Math.Sqrt,
                    repairChanceString: "sqrt(k/n)",
                    repairChanceFunc: Math.Sqrt,
                    learningRate: 2
                ),

                i => RunBenchmark(i, heuristic: Repair, fitness: Jump, mu: 1, lambda: 2, stepSize: 100,
                    jump: 2,
                    mutationRateString: "sqrt(log(n))",
                    mutationRateFunc: n =>  Math.Sqrt(Math.Log(n)),
                    repairChanceString: "k/(p*n)",
                    repairChanceFunc: n => 2D / Math.Sqrt(Math.Log(n)),
                    learningRate: 2
                ),
                i => RunBenchmark(i, heuristic: Repair, fitness: Jump, mu: 1, lambda: 2, stepSize: 100,
                    jump: 4,
                    mutationRateString: "sqrt(log(n))",
                    mutationRateFunc: n =>  Math.Sqrt(Math.Log(n)),
                    repairChanceString: "k/(p*n)",
                    repairChanceFunc: n => 2D / Math.Sqrt(Math.Log(n)),
                    learningRate: 2
                ),

                i => RunBenchmark(i, heuristic: Repair, fitness: Jump, mu: 1, stepSize: 100,
                    jump: 2,
                    lambdaString: "sqrt(n/k)",
                    lambdaFunc: n => (int) Math.Max(2, Math.Sqrt(n / 2D)),
                    mutationRateString: "sqrt(k/n)",
                    mutationRateFunc: n => Math.Sqrt(2 * n),
                    repairChanceString: "sqrt(k/n)",
                    repairChanceFunc: n => Math.Sqrt(2 * n),
                    learningRate: 1
                ),
                i => RunBenchmark(i, heuristic: Repair, fitness: Jump, mu: 1, stepSize: 100,
                    jump: 4,
                    lambdaString: "sqrt(n/k)",
                    lambdaFunc: n => (int) Math.Max(2, Math.Sqrt(n / 4D)),
                    mutationRateString: "sqrt(k/n)",
                    mutationRateFunc: n => Math.Sqrt(4 * n),
                    repairChanceString: "sqrt(k/n)",
                    repairChanceFunc: n => Math.Sqrt(4 * n),
                    learningRate: 1
                ),

                i => RunBenchmark(i, heuristic: Repair, fitness: Jump, mu: 1, lambda: 2, stepSize: 100,
                    jump: 2,
                    mutationRateString: "sqrt(k/n)",
                    mutationRateFunc: n => Math.Sqrt(2 * n),
                    repairChanceString: "sqrt(k/n)",
                    repairChanceFunc: n => Math.Sqrt(2 * n),
                    learningRate: 2
                ),
                i => RunBenchmark(i, heuristic: Repair, fitness: Jump, mu: 1, lambda: 2, stepSize: 100,
                    jump: 4,
                    mutationRateString: "sqrt(k/n)",
                    mutationRateFunc: n => Math.Sqrt(4 * n),
                    repairChanceString: "sqrt(k/n)",
                    repairChanceFunc: n => Math.Sqrt(4 * n),
                    learningRate: 2
                ),
            };
            if (0 > mode) Console.WriteLine($"Benchmarks: {benchmarks.Length} (0...{benchmarks.Length - 1})");
            else await benchmarks[mode].Invoke(mode);

            /*
            switch (mode)
            {
                case 1:
                    await RunBenchmark(heuristic: MultiEndogenous, fitness: OneMax, geneStep: 1000, mutationRate: 2,
                        mu: 10,
                        lambda: 10,
                        learningRate: 2);
                    break;
                case 2:
                    await RunBenchmark(heuristic: MultiEndogenous, fitness: OneMax, geneStep: 1000, mutationRate: 2,
                        mu: 100,
                        lambda: 100,
                        learningRate: 2);
                    break;
                case 3:
                    await RunBenchmark(heuristic: MultiEndogenous, fitness: OneMax, geneStep: 1000, mutationRate: 2,
                        mu: 50,
                        lambda: 100,
                        learningRate: 2);
                    break;
                case 4:
                    await RunBenchmark(heuristic: MultiEndogenous, fitness: LeadingOnes, geneStep: 100, mutationRate: 2,
                        mu: 10,
                        lambda: 10,
                        learningRate: 2);
                    break;
                case 5:
                    await RunBenchmark(heuristic: MultiEndogenous, fitness: LeadingOnes, geneStep: 100, mutationRate: 2,
                        mu: 100,
                        lambda: 100,
                        learningRate: 2);
                    break;
                case 6:
                    await RunBenchmark(heuristic: MultiEndogenous, fitness: Jump, geneStep: 10, mutationRate: 2,
                        mu: 100,
                        lambda: 100,
                        learningRate: 4,
                        jump: 5);
                    break;
                case 7:
                    await RunBenchmark(heuristic: MultiEndogenous, fitness: Jump, geneStep: 10, mutationRate: 2,
                        mu: 100,
                        lambda: 100,
                        learningRate: 5,
                        jump: 5);
                    break;
                case 8:
                    await RunBenchmark(heuristic: MultiEndogenous, fitness: Jump, geneStep: 10, mutationRate: 2,
                        mu: 100,
                        lambda: 100,
                        learningRate: 6,
                        jump: 5);
                    break;
                case 9:
                    await RunBenchmark(heuristic: SingleEndogenous, fitness: OneMax, mu: 1, geneStep: 1000,
                        mutationRate: 2,
                        lambda: 10,
                        learningRate: 2);
                    break;
                case 10:
                    await RunBenchmark(heuristic: SingleEndogenous, fitness: OneMax, mu: 1, geneStep: 1000,
                        mutationRate: 2,
                        lambda: 50,
                        learningRate: 2);
                    break;
                case 11:
                    await RunBenchmark(heuristic: SingleEndogenous, fitness: OneMax, mu: 1, geneStep: 1000,
                        mutationRate: 2,
                        lambda: 100,
                        learningRate: 2);
                    break;
                case 12:
                    await RunBenchmark(heuristic: SingleEndogenous, fitness: LeadingOnes, mu: 1, geneStep: 100,
                        mutationRate: 2,
                        lambda: 100,
                        learningRate: 2);
                    break;
                case 13:
                    await RunBenchmark(heuristic: SingleEndogenous, fitness: LeadingOnes, mu: 1, geneStep: 100,
                        mutationRate: 2,
                        lambda: 1000,
                        learningRate: 2);
                    break;
                case 14:
                    await RunBenchmark(heuristic: SingleEndogenous, fitness: Jump, mu: 1, geneStep: 100,
                        mutationRate: 2,
                        lambda: 100,
                        learningRate: 5,
                        jump: 5);
                    break;
                case 15:
                    await RunBenchmark(heuristic: SingleEndogenous, fitness: Jump, mu: 1, geneStep: 100,
                        mutationRate: 2,
                        lambda: 100,
                        learningRate: 4,
                        jump: 5);
                    break;
                case 16:
                    await RunBenchmark(heuristic: SingleEndogenous, fitness: Jump, mu: 1, geneStep: 100,
                        mutationRate: 2,
                        lambda: 100,
                        learningRate: 6,
                        jump: 5);
                    break;
                case 17:
                    await RunBenchmark(heuristic: Asymmetric, fitness: OneMax, mu: 1, lambda: 1, geneStep: 1000,
                        mutationRate: 1,
                        learningRate: 0.1,
                        observationPhase: 50);
                    break;
                case 18:
                    await RunBenchmark(heuristic: Asymmetric, fitness: Jump, mu: 1, lambda: 1, geneStep: 1000,
                        jump: 4,
                        mutationRate: 2,
                        learningRate: 0.02,
                        observationPhase: 10);
                    break;
                case 19:
                    await RunBenchmark(heuristic: Asymmetric, fitness: Jump, mu: 1, lambda: 1, geneStep: 1000,
                        jump: 4,
                        mutationRate: 4,
                        learningRate: 0.02,
                        observationPhase: 10);
                    break;
                case 20:
                    await RunBenchmark(heuristic: Asymmetric, fitness: Jump, mu: 1, lambda: 1, geneStep: 1000,
                        jump: 4,
                        mutationRate: 8,
                        learningRate: 0.02,
                        observationPhase: 10);
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
            */
        }
    }
}
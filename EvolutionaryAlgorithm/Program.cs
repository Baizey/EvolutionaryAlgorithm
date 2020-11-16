using System.ComponentModel;
using System.Threading.Tasks;
using static EvolutionaryAlgorithm.Benchmark;
using static EvolutionaryAlgorithm.GUI.Models.Enums.FitnessFunctions;
using static EvolutionaryAlgorithm.GUI.Models.Enums.Heuristics;

namespace EvolutionaryAlgorithm
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var mode = int.Parse(args[0]);
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
        }
    }
}
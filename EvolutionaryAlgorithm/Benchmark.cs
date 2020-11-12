using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Terminations;

namespace EvolutionaryAlgorithm
{
    public static class Benchmark
    {
        public static async Task Range(
            string filename,
            List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>> generator,
            List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, Point>> toCords,
            int rounds = 1000)
        {
            await using var file = new StreamWriter($"{filename}.txt");
            await file.WriteAsync("");
            var algorithms = new List<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>();
            var toCordsUnrolled = new List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, Point>>();
            var tasks = new List<Task>();
            var counter = 0;
            Console.WriteLine($"Progress: 0 / {tasks.Count} (0%)");
            for (var j = 0; j < generator.Count; j++)
            {
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
                                Console.WriteLine(
                                    $"Progress: {c} / {tasks.Count} ({100 * c / tasks.Count}%)");
                            }));
                }
            }

            await Task.WhenAll(tasks);

            for (var i = 0; i < algorithms.Count; i++)
            {
                var point = toCordsUnrolled[i].Invoke(algorithms[i]);
                await file.WriteLineAsync($"{point.X} {point.Y}");
            }

            await file.FlushAsync();
            Console.WriteLine("Done...");
        }
    }
}
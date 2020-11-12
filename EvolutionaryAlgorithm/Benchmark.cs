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
            for (var j = 0; j < generator.Count; j++)
            {
                for (var i = 0; i < rounds; i++)
                {
                    var algo = generator[j].Invoke();
                    algorithms.Add(algo);
                    toCordsUnrolled.Add(toCords[j]);
                    var gene = algo.Parameters.GeneCount;
                    tasks.Add(algo.EvolveAsync(new FitnessTermination<IBitIndividual, BitArray, bool>(gene)));
                }
            }

            for (var i = 0; i < tasks.Count; i++)
            {
                await tasks[i];
                var point = toCordsUnrolled[i].Invoke(algorithms[i]);
                await file.WriteLineAsync($"{point.X} {point.Y}");
                await file.FlushAsync();
                Console.WriteLine($"Progress: {i + 1} / {tasks.Count} ({100 * (i + 1) / tasks.Count}%)");
            }

            Console.WriteLine("Done...");
        }
    }
}
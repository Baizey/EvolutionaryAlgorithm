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
    public class SimpleTester
    {
        private readonly IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> _algorithm;
        private readonly string _filename;
        private readonly Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, Point> _toCords;

        public SimpleTester(IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> algorithm, string filename,
            Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, Point> toCords)
        {
            _filename = filename;
            _toCords = toCords;
            _algorithm = algorithm;
        }

        public static async Task Range(
            string filename,
            int rounds,
            List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>> generator,
            List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, Point>> toCords)
        {
            await using var file = new StreamWriter($"{filename}.txt");
            await file.WriteAsync("");

            for (var j = 0; j < generator.Count; j++)
            {
                var step = j + 1;
                var algorithms = new List<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>();
                var toCordsUnrolled = new List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, Point>>();
                var tasks = new List<Task>();
                var count = 0;
                for (var i = 0; i < rounds; i++)
                {
                    var algo = generator[j].Invoke();
                    algorithms.Add(algo);
                    toCordsUnrolled.Add(toCords[j]);
                    var gene = algo.Parameters.GeneCount;
                    tasks.Add(
                        algo.EvolveAsync(new FitnessTermination<IBitIndividual, BitArray, bool>(gene))
                            .ContinueWith(_ =>
                            {
                                Interlocked.Increment(ref count);
                                Console.WriteLine(
                                    $"{step} / {generator.Count}: {count} / {algorithms.Count}");
                            })
                    );
                }
                await Task.WhenAll(tasks);

                for (var i = 0; i < algorithms.Count; i++)
                {
                    var point = toCordsUnrolled[i].Invoke(algorithms[i]);
                    await file.WriteLineAsync($"{point.X} {point.Y}");
                }
                await file.FlushAsync();
            }

            Console.WriteLine($"Generators done...");
        }
    }
}
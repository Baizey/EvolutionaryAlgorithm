using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Template;
using EvolutionaryAlgorithm.Template.FitnessFunctions;
using static EvolutionaryAlgorithm.Template.PresetGenerator;

namespace EvolutionaryAlgorithm
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var filename = args[0];
            var algorithm = args[1];
            switch (algorithm)
            {
                case "AsymmetricStatic":
                    await AsymmetricStatic(filename);
                    break;
                default: throw new ArgumentException(algorithm);
            }
        }

        public static async Task AsymmetricStatic(string filename)
        {
            var range = new List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>>();
            var toCords = new List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, Point>>();
            for (var i = 0; i < 200; i++)
            {
                var g = (i + 1) * 100;
                range.Add(() => new BitEvolutionaryAlgorithm<IBitIndividual>()
                    .UsingBasicStatistics()
                    .UsingRandomPopulation()
                    .UsingEvaluation(new LeadingOnesFitness<IBitIndividual>())
                    .UsingHeuristic(Asymmetric(0.02, 10))
                    .UsingParameters(new Parameters
                    {
                        GeneCount = g,
                        Lambda = 1,
                        Mu = 1,
                        MutationRate = 2
                    }));
                toCords.Add(algo => new Point(g, (int) algo.Statistics.Generations));
            }

            await Benchmark.Range(filename, range, toCords);
        }
    }
}
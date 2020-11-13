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
            var algorithm = args[0];
            var mutationRate = double.Parse(args[1]);
            var learningRate = double.Parse(args[2]);
            var extra = double.Parse(args[3]);
            switch (algorithm)
            {
                case "Asymmetric":
                    await AsymmetricStatic(
                        $"Asymmetric_{mutationRate}_{learningRate}_{extra}",
                        (int) mutationRate,
                        learningRate,
                        (int) extra);
                    break;
                default: throw new ArgumentException(algorithm);
            }
        }

        public static async Task AsymmetricStatic(string filename,
            int mutationRate,
            double learningRate,
            int observationPhase)
        {
            var range = new List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>>>();
            var toCords = new List<Func<IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool>, Point>>();
            for (var i = 0; i < 20; i++)
            {
                var g = (i + 1) * 1000;
                range.Add(() => new BitEvolutionaryAlgorithm<IBitIndividual>()
                    .UsingBasicStatistics()
                    .UsingRandomPopulation()
                    .UsingEvaluation(new LeadingOnesFitness<IBitIndividual>())
                    .UsingHeuristic(Asymmetric(learningRate, observationPhase))
                    .UsingParameters(new Parameters
                    {
                        GeneCount = g,
                        Lambda = 1,
                        Mu = 1,
                        MutationRate = mutationRate
                    }));
                toCords.Add(algo => new Point(g, (int) algo.Statistics.Generations));
            }

            await Benchmark.Range(filename, range, toCords);
        }
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Template;
using EvolutionaryAlgorithm.Template.FitnessFunctions;
using EvolutionaryAlgorithm.Template.ParentSelector;

namespace EvolutionaryAlgorithm
{
    public static class MutationBenchmark
    {
        public static async Task Test(Func<IBitMutation<IBitIndividual>> mutation,
            long? fitnessCallBudget = null,
            TimeSpan? timeBudget = null)
        {
            var filename = $"{nameof(mutation)}_{fitnessCallBudget}";
            var file = new StreamWriter($"{filename}.txt");
            await file.WriteLineAsync($"{nameof(mutation)}");
            await file.WriteLineAsync($"Fitness_call_budget {fitnessCallBudget}");
            await file.WriteLineAsync($"Time_budget {timeBudget}");
            await file.WriteLineAsync($"Rounds_per_data_point {100}");
            await file.WriteLineAsync();
            await file.WriteLineAsync("GeneCount FitnessCalls Runtime");
            await file.FlushAsync();

            if (timeBudget != null)
            {
                BenchmarkTime(file, mutation, (TimeSpan) timeBudget);
            }
            else if (fitnessCallBudget != null)
            {
                BenchmarkFitnessCall(file, mutation, (long) fitnessCallBudget);
            }
            else
            {
                throw new Exception();
            }
        }

        private static void BenchmarkTime(
            TextWriter file,
            Func<IBitMutation<IBitIndividual>> mutationGenerator,
            TimeSpan budget)
        {
            for (var n = 100; n <= 1000; n += 100)
            {
                for (var rounds = 0; rounds < 100; rounds++)
                {
                    var mutation = CreateAlgo(mutationGenerator, n);
                    var individual = BitIndividual.Generate(2).Invoke(n);

                    var start = DateTime.Now;
                    TimeSpan runtime;

                    do
                    {
                        mutation.Mutate(0, individual);
                        runtime = DateTime.Now - start;
                    } while (runtime < budget);

                    file.WriteLine($"{n} {budget} {runtime}");
                }
            }
        }

        private static void BenchmarkFitnessCall(
            TextWriter file,
            Func<IBitMutation<IBitIndividual>> mutationGenerator,
            long budget)
        {
            for (var n = 100; n <= 1000; n += 100)
            {
                for (var rounds = 0; rounds < 100; rounds++)
                {
                    var mutation = CreateAlgo(mutationGenerator, n);
                    var individual = BitIndividual.Generate(2).Invoke(n);

                    var start = DateTime.Now;

                    for (var i = 0; i < budget; i++)
                        mutation.Mutate(0, individual);

                    var end = DateTime.Now;

                    file.WriteLine($"{n} {budget} {end - start}");
                }
            }
        }

        private static IBitMutation<IBitIndividual> CreateAlgo(Func<IBitMutation<IBitIndividual>> mutation,
            int geneCount)
        {
            var m = mutation.Invoke();
            var algo = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingBasicStatistics()
                .UsingRandomPopulation()
                .UsingParameters(new Parameters
                {
                    Mu = 1,
                    Lambda = 2,
                    GeneCount = geneCount,
                    MutationRate = 2
                }).UsingEvaluation(new OneMaxFitness<IBitIndividual>())
                .UsingHeuristic(
                    e => e
                        .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                        .ThenApplyMutation(m),
                    new BitElitismGenerationFilter<IBitIndividual>(true));
            algo.Initialize();
            return m;
        }
    }
}
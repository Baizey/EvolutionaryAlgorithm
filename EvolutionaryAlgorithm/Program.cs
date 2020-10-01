using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Algorithm.Parameters;
using EvolutionaryAlgorithm.Core.Algorithm.Statistics;
using EvolutionaryAlgorithm.Core.Algorithm.Terminations;
using EvolutionaryAlgorithm.Template.Basics.Fitness;
using EvolutionaryAlgorithm.Template.Endogenous;

namespace EvolutionaryAlgorithm
{
    internal class Program
    {
        private static async Task Main()
        {
            var geneCount = 50;
            var learningRate = 2;
            var mu = 1;
            var lambda = (int) Math.Log(geneCount);


            var endogenous = new EvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool>()
                .UsingParameters(new StaticParameters<IEndogenousBitIndividual, BitArray, bool>
                {
                    GeneCount = geneCount,
                    Lambda = lambda,
                    Mu = mu,
                    MutationRate = 2
                })
                .UsingStatistics(new BasicEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool>())
                .UsingPopulation(
                    new Population<IEndogenousBitIndividual, BitArray, bool>(
                        EndogenousBitIndividual.Generate(learningRate)))
                .UsingGenerationGenerator(new EndogenousGenerationGenerator(learningRate))
                .UsingTermination(new FitnessTermination<IEndogenousBitIndividual, BitArray, bool>(geneCount))
                .UsingFitness(new OneMaxFitness<IEndogenousBitIndividual>());

            endogenous.OnGenerationProgress = algo => Console.WriteLine(endogenous.Statistics);

            await endogenous.Evolve();
            Console.WriteLine(endogenous.Statistics);
        }


        static void Print(double[] arr)
        {
            Console.WriteLine("{0}: {1}", arr.Length, arr.Sum() * 100);
        }
    }
}
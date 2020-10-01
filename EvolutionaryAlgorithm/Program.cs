using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Algorithm.Parameters;
using EvolutionaryAlgorithm.Core.Algorithm.Terminations;
using EvolutionaryAlgorithm.Template.Basics.Fitness;
using EvolutionaryAlgorithm.Template.Endogenous;

namespace EvolutionaryAlgorithm
{
    internal class Program
    {
        private static async Task Main()
        {
            var geneCount = 500;
            var learningRate = 2;
            var mu = 1;
            var lambda = (int) (Math.Log(geneCount) * 3);


            var endogenous = new EvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool>()
                .UsingParameters(new StaticParameters
                {
                    GeneCount = geneCount,
                    Lambda = lambda,
                    Mu = mu,
                    MutationRate = 2
                })
                .UsingStatistics(new EndogenousBasicEvolutionaryStatistics())
                .UsingPopulation(
                    new Population<IEndogenousBitIndividual, BitArray, bool>(
                        EndogenousBitIndividual.Generate(learningRate)))
                .UsingGenerationGenerator(new EndogenousGenerationGenerator(learningRate))
                .UsingFitness(new OneMaxFitness<IEndogenousBitIndividual>());

            endogenous.OnGenerationProgress = algo => { Console.WriteLine(endogenous.Statistics); };

            await endogenous.Evolve(a => a.Statistics.Best.Fitness >= geneCount);
            Console.WriteLine(endogenous.Statistics);
        }
    }
}
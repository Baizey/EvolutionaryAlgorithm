using System;
using System.Collections;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Algorithm.Crossover;
using EvolutionaryAlgorithm.Core.Algorithm.Mutator;
using EvolutionaryAlgorithm.Core.Algorithm.Parameters;
using EvolutionaryAlgorithm.Core.Algorithm.Statistics;
using EvolutionaryAlgorithm.Core.Algorithm.Terminations;
using EvolutionaryAlgorithm.Template.Basics.Fitness;
using EvolutionaryAlgorithm.Template.Basics.Mutation;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;
using EvolutionaryAlgorithm.Template.Basics.Selection;
using EvolutionaryAlgorithm.Template.Endogenous;
using EvolutionaryAlgorithm.Template.Stagnation;

namespace EvolutionaryAlgorithm
{
    internal class Program
    {
        private static async Task Main()
        {
            const int
                geneCount = 5000,
                populationSize = 1,
                newIndividualsPerGeneration = 1,
                learningRate = 32,
                jump = 1;

            var endogenous = new EvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool>()
                .UsingParameters(new StaticParameters<IEndogenousBitIndividual, BitArray, bool>
                {
                    GeneCount = geneCount,
                    Lambda = newIndividualsPerGeneration,
                    Mu = populationSize
                })
                .UsingStatistics(new BasicEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool>())
                .UsingPopulation(new Population<IEndogenousBitIndividual, BitArray, bool>(
                    EndogenousBitIndividual.Generate(learningRate)))
                .UsingGenerationGenerator(new EndogenousGenerationGenerator(learningRate))
                .UsingTermination(new FitnessTermination<IEndogenousBitIndividual, BitArray, bool>(geneCount));

            var applier = new MutationApplier();

            var a = 1000;
            for (var i = 1; i < a / 2; i++)
                Print(applier.GetOdds(i, a));
        }


        static void Print(double[] arr)
        {
            Console.WriteLine("{0}", arr.Length, string.Join(", ", arr));
        }
    }
}
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions;
using EvolutionaryAlgorithm.Core.Algorithm.Crossover;
using EvolutionaryAlgorithm.Core.Algorithm.Mutator;
using EvolutionaryAlgorithm.Core.Algorithm.Statistics;
using EvolutionaryAlgorithm.Core.Algorithm.Terminations;
using EvolutionaryAlgorithm.Template.Basics;
using EvolutionaryAlgorithm.Template.Basics.Mutation;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;
using EvolutionaryAlgorithm.Template.Basics.Selection;
using EvolutionaryAlgorithm.Template.Fitness;

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
                jump = 1;

            var classicInstance = new BitEvolutionaryAlgorithm
            {
                Parameters = new BitStaticParameters
                {
                    GeneCount = geneCount,
                    Mu = populationSize,
                    Lambda = newIndividualsPerGeneration,
                },
                Statistics = new BasicEvolutionaryStatistics<IBitIndividual, BitArray, bool>(),
                HyperMutator = new SimpleHyperMutator<IBitIndividual, BitArray, bool>(new BitMutator
                {
                    Mutations =
                    {
                        new CloneParent<IBitIndividual, BitArray, bool>(new BestFitnessParentSelector()),
                        new OneMaxStaticOptimalMutation()
                    }
                }),
                Fitness = new OneMaxFitness(),
                GenerationFilter = new ElitismGenerationFilter(),
                Termination = new FitnessTermination<IBitIndividual, BitArray, bool>(geneCount)
            };

            var chainedInstance = BitEvolutionaryAlgorithm.Construct
                .UsingStaticParameters(geneCount, populationSize, newIndividualsPerGeneration)
                .UsingBasicStatistics()
                .UsingPopulation(BitPopulation.FromRandom())
                .UsingMutator(mutator => mutator
                    .CloneGenesFrom(new BestFitnessParentSelector())
                    .ThenApply(new OneMaxStaticOptimalMutation()))
                .UsingFitness(new OneMaxFitness())
                .UsingGenerationFilter(new ElitismGenerationFilter())
                .UsingTermination(new FitnessTermination<IBitIndividual, BitArray, bool>(geneCount));

            await chainedInstance.Evolve();

            chainedInstance.Cancel();

            Console.WriteLine(chainedInstance.Statistics);
        }
    }
}
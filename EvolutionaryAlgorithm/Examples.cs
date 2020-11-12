using System;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Template;
using EvolutionaryAlgorithm.Template.FitnessFunctions;
using EvolutionaryAlgorithm.Template.Statistics;
using static EvolutionaryAlgorithm.Template.PresetGenerator;

namespace EvolutionaryAlgorithm
{
    public class Examples
    {
        public void Func()
        {
            const int geneCount = 500;
            const int mutationRate = 2;
            const int repairRate = 2;
            const int learningRate = 2;
            const int observationPhase = 10;
            const double beta = 1.5;

            var endogenous = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Self adapting
                    MutationRate = mutationRate,
                    // Constant, based on gene count
                    Lambda = (int) (5 * Math.Log(geneCount)),
                    // Always 1
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingRandomPopulation(mutationRate)
                .UsingHeuristic(SingleEndogenous(learningRate))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());

            var stagnation = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Self adapting
                    MutationRate = mutationRate,
                    // Constant, based on gene count
                    Lambda = (int) (3 * Math.Log(geneCount)),
                    // Always 1
                    Mu = 1,
                })
                .UsingStagnationStatistics()
                .UsingRandomPopulation(learningRate)
                .UsingHeuristic(StagnationDetection(learningRate, mutationRate, 1))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());

            var asymmetric = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Self adapting, based on learning rate
                    MutationRate = 3,
                    // Always 1
                    Lambda = 1,
                    // Always 1
                    Mu = 1,
                })
                .UsingRandomPopulation(mutationRate)
                .UsingStatistics(new AsymmetricBasicEvolutionaryStatistics<IBitIndividual>())
                .UsingHeuristic(Asymmetric(0.05, observationPhase))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());

            var oneLambdaLambda = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Constant
                    MutationRate = mutationRate,
                    // Self-adapting, initial 1
                    Lambda = 1,
                    // Always 1
                    Mu = 1,
                })
                .UsingStatistics(new LambdaBasicStatistics<IBitIndividual>())
                .UsingRandomPopulation(mutationRate)
                .UsingHeuristic(Repair(learningRate, repairRate))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());

            var heavyTail = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Self adapting
                    MutationRate = mutationRate,
                    // Constant
                    Lambda = (int) (3 * Math.Log(geneCount)),
                    // Always 1
                    Mu = 1,
                })
                .UsingBasicStatistics()
                .UsingRandomPopulation(mutationRate)
                .UsingHeuristic(HeavyTail(learningRate, beta))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());

            var lambdaEndogenous = new BitEvolutionaryAlgorithm<IBitIndividual>()
                .UsingParameters(new Parameters
                {
                    GeneCount = geneCount,
                    // Self adapting
                    MutationRate = mutationRate,
                    // Constant, based on gene count
                    Lambda = (int) (3 * Math.Log(geneCount)),
                    // Constant, based on gene count
                    Mu = (int) (3 * Math.Log(geneCount)),
                })
                .UsingStatistics(new EndogenousBasicEvolutionaryStatistics())
                .UsingRandomPopulation(mutationRate)
                .UsingHeuristic(MultiEndogenous(learningRate))
                .UsingEvaluation(new OneMaxFitness<IBitIndividual>());
        }
    }
}
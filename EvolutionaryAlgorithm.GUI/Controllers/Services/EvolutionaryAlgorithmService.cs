using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Statistics;
using EvolutionaryAlgorithm.Core.Terminations;
using EvolutionaryAlgorithm.GUI.Controllers.Services.Extensions;
using EvolutionaryAlgorithm.GUI.Services.Enums;
using EvolutionaryAlgorithm.Template;
using EvolutionaryAlgorithm.Template.Asymmetric;
using EvolutionaryAlgorithm.Template.Basics.Fitness;
using EvolutionaryAlgorithm.Template.Endogenous;
using EvolutionaryAlgorithm.Template.LambdaLambdaEndogenous;
using EvolutionaryAlgorithm.Template.OneLambdaLambda;
using EvolutionaryAlgorithm.Template.Stagnation;
using static EvolutionaryAlgorithm.GUI.Services.Enums.FitnessFunctions;
using static EvolutionaryAlgorithm.GUI.Services.Enums.Heuristics;

namespace EvolutionaryAlgorithm.GUI.Controllers.Services
{
    public interface IEvolutionaryAlgorithmService
    {
        public bool IsRunning { get; }
        public IBitEvolutionaryAlgorithm<IEndogenousBitIndividual> Algorithm { get; }
        public IUiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool> Statistics { get; }
        public void Run(ITermination<IEndogenousBitIndividual, BitArray, bool> termination);
        public void Terminate();

        public void Initialize(
            FitnessFunctions fitness, Heuristics heuristic,
            int geneCount = 0,
            int mu = 1,
            int lambda = 1,
            int datapoints = 1000,
            double learningRate = 0.05D,
            int mutationRate = 2,
            int observationPhase = 10,
            double c = 1,
            int jump = 1);
    }

    public class EvolutionaryAlgorithmService : IEvolutionaryAlgorithmService
    {
        private IUiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool> _statistics;
        public IBitEvolutionaryAlgorithm<IEndogenousBitIndividual> Algorithm { get; private set; }
        private bool _requestStatistics;

        public bool IsRunning => Algorithm?.IsRunning ?? false;

        public IUiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool> Statistics
        {
            get
            {
                _requestStatistics = true;
                while (_requestStatistics && IsRunning)
                    Task.Delay(25).GetAwaiter().GetResult();
                _requestStatistics = false;
                return _statistics;
            }
            private set => _statistics = value;
        }

        public void Initialize(
            FitnessFunctions fitness, Heuristics heuristic,
            int geneCount = 0,
            int mu = 1,
            int lambda = 1,
            int datapoints = 1000,
            double learningRate = 0.05D,
            int mutationRate = 2,
            int observationPhase = 10,
            double c = 1,
            int jump = 1)
        {
            Terminate();
            Algorithm = new BitEvolutionaryAlgorithm<IEndogenousBitIndividual>
            {
                OnGenerationProgress = algorithm =>
                {
                    if (!_requestStatistics) return;
                    _requestStatistics = false;
                    Statistics = Algorithm.CloneUiStatistics();
                },
                OnTermination = algorithm => Statistics = Algorithm.CloneUiStatistics()
            };
            Algorithm.UsingParameters(new Parameters
            {
                Mu = mu,
                Lambda = lambda,
                GeneCount = geneCount,
                MutationRate = mutationRate
            });
            Algorithm.UsingStatistics(
                new UiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool>(datapoints));
            Algorithm.UsingEvaluation(CreateFitness(fitness, jump));
            Algorithm.UsingEndogenousRandomPopulation(mutationRate);
            Algorithm.UsingHeuristic(CreateHeuristic(heuristic, learningRate, mutationRate, observationPhase, c));
            Statistics = Algorithm.CloneUiStatistics();
        }

        public void Run(ITermination<IEndogenousBitIndividual, BitArray, bool> termination)
        {
            Terminate();
            Algorithm.EvolveAsync(termination);
        }

        public void Terminate()
        {
            if (!IsRunning) return;
            Algorithm.Terminate();

            while (Algorithm.IsRunning) Task.Delay(25).GetAwaiter().GetResult();

            Statistics = Algorithm.CloneUiStatistics();
        }

        private static IHyperHeuristic<IEndogenousBitIndividual, BitArray, bool> CreateHeuristic(Heuristics heuristic,
            double learningRate = 0.05D,
            int mutationRate = 2,
            int observationPhase = 10,
            double c = 1
        ) => heuristic switch
        {
            Asymmetric => new SimpleHeuristic<IEndogenousBitIndividual, BitArray, bool>(
                new AsymmetricGenerationGenerator(learningRate, observationPhase)),
            Repair => new SimpleHeuristic<IEndogenousBitIndividual, BitArray, bool>(
                new OneLambdaLambdaGenerationGenerator((int) learningRate, c)),
            SingleEndogenous => new SimpleHeuristic<IEndogenousBitIndividual, BitArray, bool>(
                new EndogenousGenerationGenerator((int) learningRate)),
            MultiEndogenous => new SimpleHeuristic<IEndogenousBitIndividual, BitArray, bool>(
                new LambdaLambdaEndogenousGenerationGenerator((int) learningRate)),
            StagnationDetection => new StagnationDetectionHyperHeuristic(mutationRate),
            _ => throw new ArgumentOutOfRangeException(nameof(heuristic), heuristic, null)
        };

        private static IBitFitness<IEndogenousBitIndividual> CreateFitness(FitnessFunctions fitnessFunction,
            int jump = 0) => fitnessFunction switch
        {
            OneMax => new OneMaxFitness<IEndogenousBitIndividual>(),
            Jump => new JumpFitness<IEndogenousBitIndividual>(jump),
            LeadingOnes => new LeadingOnesFitness<IEndogenousBitIndividual>(),
            _ => throw new ArgumentOutOfRangeException(nameof(fitnessFunction), fitnessFunction, null)
        };
    }
}
using System;
using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Statistics;
using EvolutionaryAlgorithm.Core.Terminations;
using EvolutionaryAlgorithm.GUI.Models.Enums;
using EvolutionaryAlgorithm.GUI.Models.Extensions;
using EvolutionaryAlgorithm.Template;
using EvolutionaryAlgorithm.Template.Asymmetric;
using EvolutionaryAlgorithm.Template.Basics.Fitness;
using EvolutionaryAlgorithm.Template.Endogenous;
using EvolutionaryAlgorithm.Template.HeavyTail;
using EvolutionaryAlgorithm.Template.MultiEndogenous;
using EvolutionaryAlgorithm.Template.Repair;
using EvolutionaryAlgorithm.Template.Stagnation;
using static EvolutionaryAlgorithm.GUI.Models.Enums.FitnessFunctions;
using static EvolutionaryAlgorithm.GUI.Models.Enums.Heuristics;

namespace EvolutionaryAlgorithm.GUI.Models.Services
{
    public interface IEvolutionaryAlgorithmService
    {
        public bool IsRunning { get; }
        public IBitEvolutionaryAlgorithm<IEndogenousBitIndividual> Algorithm { get; }
        public StatisticsView Statistics { get; }
        public bool Run(ITermination<IEndogenousBitIndividual, BitArray, bool> termination);
        public void Pause();

        public void Initialize(
            FitnessFunctions fitness, Heuristics heuristic,
            int geneCount = 0,
            int mu = 1,
            int lambda = 1,
            double learningRate = 0.05D,
            int mutationRate = 2,
            int observationPhase = 10,
            double repairChance = 1,
            double beta = 1.5,
            int jump = 1);
    }

    public class EvolutionaryAlgorithmService : IEvolutionaryAlgorithmService
    {
        private StatisticsView _statistics;
        private bool _requestStatistics;
        public IBitEvolutionaryAlgorithm<IEndogenousBitIndividual> Algorithm { get; private set; }

        public bool IsRunning => Algorithm?.IsRunning ?? false;

        public StatisticsView Statistics
        {
            get
            {
                _requestStatistics = true;
                while (_requestStatistics && IsRunning)
                    Task.Delay(1).GetAwaiter().GetResult();
                _requestStatistics = false;
                if (!IsRunning) _statistics ??= Algorithm.MapToStatisticsView();
                var result = _statistics;
                _statistics = null;
                return result;
            }
            private set => _statistics = value;
        }

        public void Initialize(
            FitnessFunctions fitness, Heuristics heuristic,
            int geneCount = 100,
            int mu = 1,
            int lambda = 1,
            double learningRate = 0.05D,
            int mutationRate = 2,
            int observationPhase = 10,
            double repairChance = 1,
            double beta = 1.5,
            int jump = 1)
        {
            Pause();
            Algorithm = new BitEvolutionaryAlgorithm<IEndogenousBitIndividual>
            {
                OnGenerationProgress = algorithm =>
                {
                    if (!_requestStatistics) return;
                    _requestStatistics = false;
                    Statistics = Algorithm.MapToStatisticsView();
                }
            };
            Algorithm.UsingParameters(new Parameters
            {
                Mu = mu,
                Lambda = lambda,
                GeneCount = geneCount,
                MutationRate = mutationRate
            });
            Algorithm.UsingStatistics(
                new BasicEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool>());
            Algorithm.UsingEvaluation(CreateFitness(fitness, jump));
            Algorithm.UsingEndogenousRandomPopulation(mutationRate);
            Algorithm.UsingHeuristic(CreateHeuristic(heuristic, learningRate, mutationRate, observationPhase,
                repairChance, beta));
        }

        public bool Run(ITermination<IEndogenousBitIndividual, BitArray, bool> termination)
        {
            if (IsRunning) return false;
            Pause();
            Algorithm.EvolveAsync(termination);
            return true;
        }

        public void Pause()
        {
            if (!IsRunning) return;
            Algorithm.Terminate();
            while (Algorithm.IsRunning)
                Task.Delay(1).GetAwaiter().GetResult();
        }

        private static IHyperHeuristic<IEndogenousBitIndividual, BitArray, bool> CreateHeuristic(Heuristics heuristic,
            double learningRate = 0.05D,
            int mutationRate = 2,
            int observationPhase = 10,
            double repairChance = 1,
            double beta = 1.5) => heuristic switch
        {
            Asymmetric => new SimpleHeuristic<IEndogenousBitIndividual, BitArray, bool>(
                new AsymmetricGenerationGenerator(learningRate, observationPhase)),
            Repair => new SimpleHeuristic<IEndogenousBitIndividual, BitArray, bool>(
                new OneLambdaLambdaGenerationGenerator((int) learningRate, repairChance)),
            SingleEndogenous => new SimpleHeuristic<IEndogenousBitIndividual, BitArray, bool>(
                new EndogenousGenerationGenerator((int) learningRate)),
            MultiEndogenous => new SimpleHeuristic<IEndogenousBitIndividual, BitArray, bool>(
                new MultiEndogenousGenerationGenerator((int) learningRate)),
            HeavyTail => new SimpleHeuristic<IEndogenousBitIndividual, BitArray, bool>(
                new HeavyTailGenerationGenerator(beta)),
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
using System;
using System.Collections;
using System.Collections.Generic;
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
using EvolutionaryAlgorithm.Template.FitnessFunctions;
using EvolutionaryAlgorithm.Template.FitnessFunctions.Graph;
using EvolutionaryAlgorithm.Template.FitnessFunctions.SatisfiabilityProblem;
using EvolutionaryAlgorithm.Template.Mutations;
using static EvolutionaryAlgorithm.GUI.Models.Enums.FitnessFunctions;
using static EvolutionaryAlgorithm.GUI.Models.Enums.Heuristics;

namespace EvolutionaryAlgorithm.GUI.Models.Services
{
    public interface IEvolutionaryAlgorithmService
    {
        public bool IsRunning { get; }
        public IBitEvolutionaryAlgorithm<IBitIndividual> Algorithm { get; }
        public StatisticsView Statistics { get; }
        public IEnumerable<Node> Nodes { get; }
        IEnumerable<Edge> Edges { get; }

        public bool Run(
            Termination termination,
            int seconds = 10,
            double fitness = 100,
            int generations = 1000);

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
            int jump = 1,
            int nodes = 20,
            double edgeChance = 0.5,
            int formulas = 20,
            int variables = 60,
            int formulaSize = 3,
            int limitFactor = 1);
    }

    public class EvolutionaryAlgorithmService : IEvolutionaryAlgorithmService
    {
        private SimpleGraph _graph;
        private SatisfiabilityProblem _problem;
        private StatisticsView _statistics;
        private bool _requestStatistics;
        public IBitEvolutionaryAlgorithm<IBitIndividual> Algorithm { get; private set; }

        public bool IsRunning => Algorithm?.IsRunningAsync ?? false;

        public StatisticsView Statistics
        {
            get
            {
                _requestStatistics = true;
                while (_requestStatistics && IsRunning)
                    Task.Delay(1).GetAwaiter().GetResult();
                _requestStatistics = false;
                if (!IsRunning) _statistics ??= Algorithm.MapToStatisticsView(_graph);
                var result = _statistics;
                _statistics = null;
                return result;
            }
            private set => _statistics = value;
        }

        public IEnumerable<Node> Nodes => _graph?.Nodes;
        public IEnumerable<Edge> Edges => _graph?.Edges;

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
            int jump = 1,
            int nodes = 20,
            double edgeChance = 0.5,
            int formulas = 20,
            int variables = 60,
            int formulaSize = 3,
            int limitFactor = 1)
        {
            Pause();
            Algorithm = new BitEvolutionaryAlgorithm<IBitIndividual>
            {
                OnGenerationProgress = algorithm =>
                {
                    if (!_requestStatistics) return;
                    _requestStatistics = false;
                    Statistics = Algorithm.MapToStatisticsView(_graph);
                }
            };
            _graph = null;
            Algorithm.UsingEvaluation(CreateFitness(fitness, jump, nodes, edgeChance, formulas, variables,
                formulaSize));
            // If we created a graph we have genes to match edges
            if (_graph != null) geneCount = _graph.Edges.Count;

            Algorithm.UsingParameters(new Parameters
            {
                Mu = mu,
                Lambda = lambda,
                GeneCount = geneCount,
                MutationRate = mutationRate
            });
            Algorithm.UsingBasicStatistics();
            Algorithm.UsingRandomPopulation(mutationRate);
            Algorithm.UsingHeuristic(CreateHeuristic(heuristic, learningRate, mutationRate, observationPhase,
                repairChance, beta, limitFactor));
        }

        public bool Run(
            Termination termination,
            int seconds = 10,
            double fitness = 100,
            int generations = 1000)
        {
            if (IsRunning) return false;
            Pause();
            Algorithm.EvolveAsync(CreateTermination(termination,
                seconds: seconds,
                fitness: fitness,
                generations: generations));
            return true;
        }

        public void Pause()
        {
            if (!IsRunning) return;
            Algorithm.Terminate();
            while (Algorithm.IsRunningAsync)
                Task.Delay(1).GetAwaiter().GetResult();
        }

        private static ITermination<IBitIndividual, BitArray, bool> CreateTermination(Termination termination,
            double fitness = 100,
            int generations = 100,
            int seconds = 10) => termination switch
        {
            Termination.Fitness => new FitnessTermination<IBitIndividual, BitArray, bool>(fitness),
            Termination.Time => new TimeoutTermination<IBitIndividual, BitArray, bool>(
                TimeSpan.FromSeconds(seconds)),
            Termination.Generations => new GenerationTermination<IBitIndividual, BitArray, bool>(generations),
            Termination.Stagnation => new StagnationTermination<IBitIndividual, BitArray, bool>(generations),
            _ => throw new ArgumentOutOfRangeException(nameof(termination), termination, null)
        };

        private static IHyperHeuristic<IBitIndividual, BitArray, bool> CreateHeuristic(Heuristics heuristic,
            double learningRate = 0.05D,
            int mutationRate = 2,
            int observationPhase = 10,
            double repairChance = 1,
            double beta = 1.5,
            int limitFactor = 1) => heuristic switch
        {
            Asymmetric => new SimpleHeuristic<IBitIndividual, BitArray, bool>(
                PresetGenerator.Asymmetric(learningRate, observationPhase)),
            Repair => new SimpleHeuristic<IBitIndividual, BitArray, bool>(
                PresetGenerator.Repair((int) learningRate, repairChance)),
            SingleEndogenous => new SimpleHeuristic<IBitIndividual, BitArray, bool>(
                PresetGenerator.SingleEndogenous((int) learningRate)),
            MultiEndogenous => new SimpleHeuristic<IBitIndividual, BitArray, bool>(
                PresetGenerator.MultiEndogenous((int) learningRate)),
            HeavyTail => new SimpleHeuristic<IBitIndividual, BitArray, bool>(
                PresetGenerator.HeavyTail((int) learningRate, beta)),
            StagnationDetection => PresetGenerator.StagnationDetection(mutationRate, limitFactor, (int) learningRate),
            _ => throw new ArgumentOutOfRangeException(nameof(heuristic), heuristic, null)
        };

        private IBitFitness<IBitIndividual> CreateFitness(FitnessFunctions fitnessFunction,
            int jump = 0,
            int nodes = 20,
            double edgeChance = 0.5,
            int formulas = 20,
            int variables = 60,
            int formulaSize = 3
        )
        {
            switch (fitnessFunction)
            {
                case OneMax:
                    return new OneMaxFitness<IBitIndividual>();
                case Jump:
                    return new JumpFitness<IBitIndividual>(jump);
                case LeadingOnes:
                    return new LeadingOnesFitness<IBitIndividual>();
                case MinimumSpanningTree:
                    _graph = new SimpleGraph(nodes, edgeChance);
                    return new MinimumSpanningTreeFitness<IBitIndividual>(_graph);
                case Satisfiability:
                    _problem = SatisfiabilityProblem.Generate(variables, formulas, formulaSize);
                    return new SatisfiabilityProblemFitness<IBitIndividual>(_problem);
                default:
                    throw new ArgumentOutOfRangeException(nameof(fitnessFunction), fitnessFunction, null);
            }
        }
    }
}
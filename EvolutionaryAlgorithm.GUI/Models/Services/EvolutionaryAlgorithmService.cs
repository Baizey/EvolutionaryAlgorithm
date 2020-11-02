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
using EvolutionaryAlgorithm.Template.Asymmetric;
using EvolutionaryAlgorithm.Template.Basics.Fitness;
using EvolutionaryAlgorithm.Template.Endogenous;
using EvolutionaryAlgorithm.Template.HeavyTail;
using EvolutionaryAlgorithm.Template.MinimumSpanningTree;
using EvolutionaryAlgorithm.Template.MinimumSpanningTree.Graph;
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
            double edgeChance = 0.5);
    }

    public class EvolutionaryAlgorithmService : IEvolutionaryAlgorithmService
    {
        private SimpleGraph _graph;
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
                if (!IsRunning) _statistics ??= Algorithm.MapToStatisticsView(_graph);
                var result = _statistics;
                _statistics = null;
                return result;
            }
            private set => _statistics = value;
        }

        public IEnumerable<Node> Nodes => _graph.Nodes;
        public IEnumerable<Edge> Edges => _graph.Edges;

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
            double edgeChance = 0.5)
        {
            Pause();
            Algorithm = new BitEvolutionaryAlgorithm<IEndogenousBitIndividual>
            {
                OnGenerationProgress = algorithm =>
                {
                    if (!_requestStatistics) return;
                    _requestStatistics = false;
                    Statistics = Algorithm.MapToStatisticsView(_graph);
                }
            };
            _graph = null;
            Algorithm.UsingEvaluation(CreateFitness(fitness, jump, nodes, edgeChance));
            // If we created a graph we have genes to match edges
            if (_graph != null) geneCount = _graph.Edges.Count;

            Algorithm.UsingParameters(new Parameters
            {
                Mu = mu,
                Lambda = lambda,
                GeneCount = geneCount,
                MutationRate = mutationRate
            });
            Algorithm.UsingStatistics(
                new BasicEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool>());
            Algorithm.UsingEndogenousRandomPopulation(mutationRate);
            Algorithm.UsingHeuristic(CreateHeuristic(heuristic, learningRate, mutationRate, observationPhase,
                repairChance, beta));
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
            while (Algorithm.IsRunning)
                Task.Delay(1).GetAwaiter().GetResult();
        }

        private static ITermination<IEndogenousBitIndividual, BitArray, bool> CreateTermination(Termination termination,
            double fitness = 100,
            int generations = 100,
            int seconds = 10) => termination switch
        {
            Termination.Fitness => new FitnessTermination<IEndogenousBitIndividual, BitArray, bool>(fitness),
            Termination.Time => new TimeoutTermination<IEndogenousBitIndividual, BitArray, bool>(
                TimeSpan.FromSeconds(seconds)),
            Termination.Generations => new GenerationTermination<IEndogenousBitIndividual, BitArray, bool>(generations),
            Termination.Stagnation => new StagnationTermination<IEndogenousBitIndividual, BitArray, bool>(generations),
            _ => throw new ArgumentOutOfRangeException(nameof(termination), termination, null)
        };

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

        private IBitFitness<IEndogenousBitIndividual> CreateFitness(FitnessFunctions fitnessFunction,
            int jump = 0,
            int nodes = 20,
            double edgeChance = 0.5
        )
        {
            switch (fitnessFunction)
            {
                case OneMax:
                    return new OneMaxFitness<IEndogenousBitIndividual>();
                case Jump:
                    return new JumpFitness<IEndogenousBitIndividual>(jump);
                case LeadingOnes:
                    return new LeadingOnesFitness<IEndogenousBitIndividual>();
                case MinimumSpanningTree:
                    _graph = new SimpleGraph(nodes, edgeChance);
                    return new MinimumSpanningTreeFitness<IEndogenousBitIndividual>(_graph);
                case Satisfiability:
                    // TODO: this
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fitnessFunction), fitnessFunction, null);
            }
        }
    }
}
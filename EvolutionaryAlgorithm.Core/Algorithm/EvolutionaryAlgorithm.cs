using System;
using System.Threading;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Fitness;
using EvolutionaryAlgorithm.Core.HyperHeuristic;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Population;
using EvolutionaryAlgorithm.Core.Statistics;
using EvolutionaryAlgorithm.Core.Terminations;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private CancellationTokenSource _cancellationSource;

        public void Terminate() => _cancellationSource.Cancel();
        public bool IsRunningAsync => AsyncRunner != null && !AsyncRunner.IsCompleted;
        public Task AsyncRunner { get; private set; }
        public IParameters Parameters { get; set; }
        public IPopulation<TIndividual, TGeneStructure, TGene> Population { get; set; }
        public IFitness<TIndividual, TGeneStructure, TGene> Fitness { get; set; }
        public IHyperHeuristic<TIndividual, TGeneStructure, TGene> HyperHeuristic { get; set; }
        public IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> Statistics { get; set; }
        public Action<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>> OnInitialization { get; set; }
        public Action<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>> OnGenerationProgress { get; set; }
        public Action<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>> OnTermination { get; set; }
        public bool IsInitialized { get; set; }

        private void ArgumentValidation()
        {
            if (Parameters == null)
                throw new EvolutionaryAlgorithmArgumentException(
                    $"IEvolutionaryAlgorithm.{nameof(Parameters)} cannot by null");
            if (Statistics == null)
                throw new EvolutionaryAlgorithmArgumentException(
                    $"IEvolutionaryAlgorithm.{nameof(Statistics)} cannot by null");
            if (Population == null)
                throw new EvolutionaryAlgorithmArgumentException(
                    $"IEvolutionaryAlgorithm.{nameof(Population)} cannot by null");
            if (Fitness == null)
                throw new EvolutionaryAlgorithmArgumentException(
                    $"IEvolutionaryAlgorithm.{nameof(Fitness)} cannot by null");
            if (HyperHeuristic == null)
                throw new EvolutionaryAlgorithmArgumentException(
                    $"IEvolutionaryAlgorithm.{nameof(HyperHeuristic)} cannot by null");
        }

        public void Initialize()
        {
            IsInitialized = true;
            ArgumentValidation();

            Population.Algorithm = this;
            Fitness.Algorithm = this;
            Statistics.Algorithm = this;
            HyperHeuristic.Algorithm = this;

            Population.Initialize();
            Fitness.Initialize();
            Population.Individuals.ForEach(i => i.Fitness = Fitness.Evaluate(i));
            Statistics.Initialize();
            HyperHeuristic.Initialize();

            OnInitialization ??= _ => { };
            OnGenerationProgress ??= _ => { };
            OnTermination ??= _ => { };
            OnInitialization(this);
            OnGenerationProgress(this);
        }

        public void Update()
        {
            Statistics.Update();
            HyperHeuristic.Update();
        }

        public async Task EvolveOneGeneration() => Population.Individuals = await HyperHeuristic.Generate();

        public Task EvolveAsync(ITermination<TIndividual, TGeneStructure, TGene> termination)
        {
            _cancellationSource = new CancellationTokenSource();
            return AsyncRunner = Task.Run(async () => await Evolve(termination, _cancellationSource.Token),
                _cancellationSource.Token);
        }

        public async Task Evolve(ITermination<TIndividual, TGeneStructure, TGene> termination,
            CancellationToken cancellationToken)
        {
            termination.Algorithm = this;
            if (!IsInitialized) Initialize();

            while (!termination.ShouldTerminate())
            {
                await EvolveOneGeneration();
                Update();
                OnGenerationProgress(this);
                if (cancellationToken.IsCancellationRequested) break;
            }

            Statistics.Finish();
            OnTermination(this);
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Algorithm.Terminations;

namespace EvolutionaryAlgorithm.Core.Abstract.Core
{
    public interface IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        : IUpdates, IInitializes
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public void Terminate();
        public IParameters Parameters { get; set; }
        public IPopulation<TIndividual, TGeneStructure, TGene> Population { get; set; }
        public IHyperHeuristic<TIndividual, TGeneStructure, TGene> HyperHeuristic { get; set; }
        public IFitness<TIndividual, TGeneStructure, TGene> Fitness { get; set; }
        public IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> Statistics { get; set; }
        public Action<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>> OnGenerationProgress { get; set; }

        public TIndividual Best => Population.Best;
        public bool IsInitialized { get; set; }
        public Task EvolveOneGeneration();
        public Task Evolve(ITermination<TIndividual, TGeneStructure, TGene> termination);

        public Task Evolve(Func<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>, bool> termination) =>
            Evolve(new LambdaTermination<TIndividual, TGeneStructure, TGene>(termination));
    }

    public class EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private CancellationTokenSource _cancellationTokenSource;
        public void Terminate() => _cancellationTokenSource.Cancel();
        public IParameters Parameters { get; set; }
        public IPopulation<TIndividual, TGeneStructure, TGene> Population { get; set; }
        public IFitness<TIndividual, TGeneStructure, TGene> Fitness { get; set; }
        public IHyperHeuristic<TIndividual, TGeneStructure, TGene> HyperHeuristic { get; set; }
        public IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> Statistics { get; set; }
        public Action<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>> OnGenerationProgress { get; set; }
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

            OnGenerationProgress ??= _ => { };
            OnGenerationProgress(this);
        }

        public void Update()
        {
            Statistics.Update();
            HyperHeuristic.Update();
        }

        public async Task EvolveOneGeneration() => Population.Individuals = await HyperHeuristic.Generate();

        public async Task Evolve(ITermination<TIndividual, TGeneStructure, TGene> termination)
        {
            termination.Algorithm = this;
            var token = (_cancellationTokenSource = new CancellationTokenSource()).Token;
            if (!IsInitialized) Initialize();

            while (!termination.ShouldTerminate())
            {
                await EvolveOneGeneration();
                Update();
                OnGenerationProgress(this);
                if (!token.IsCancellationRequested) continue;
                Statistics.Finish();
                return;
            }

            Statistics.Finish();
        }
    }
}
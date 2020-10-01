using System;
using System.Threading;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Core.Abstract.Core
{
    public interface IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public void Cancel() => CancellationTokenSource.Cancel();
        public IParameters<TIndividual, TGeneStructure, TGene> Parameters { get; set; }
        public IPopulation<TIndividual, TGeneStructure, TGene> Population { get; set; }
        public IHyperHeuristic<TIndividual, TGeneStructure, TGene> HyperHeuristic { get; set; }
        public IFitness<TIndividual, TGeneStructure, TGene> Fitness { get; set; }
        public ITermination<TIndividual, TGeneStructure, TGene> Termination { get; set; }
        public IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> Statistics { get; set; }
        public Action<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>> OnGenerationProgress { get; set; }

        public TIndividual Best => Population.Best;
        public bool IsInitialized { get; set; }
        public void Initialize();
        public void Update();
        public Task EvolveOneGeneration();
        public Task Evolve();
    }

    public class EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public ITermination<TIndividual, TGeneStructure, TGene> Termination { get; set; }
        public IParameters<TIndividual, TGeneStructure, TGene> Parameters { get; set; }
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
            if (Termination == null)
                throw new EvolutionaryAlgorithmArgumentException(
                    $"IEvolutionaryAlgorithm.{nameof(Termination)} cannot by null");
        }

        public void Initialize()
        {
            IsInitialized = true;
            ArgumentValidation();

            Population.Algorithm = this;
            Fitness.Algorithm = this;
            Statistics.Algorithm = this;
            Parameters.Algorithm = this;
            HyperHeuristic.Algorithm = this;
            Termination.Algorithm = this;

            Population.Initialize();
            Fitness.Initialize();
            Population.Individuals.ForEach(i => i.Fitness = Fitness.Evaluate(i));
            Statistics.Initialize();
            Parameters.Initialize();
            HyperHeuristic.Initialize();
            Termination.Initialize();

            OnGenerationProgress ??= _ => { };
            OnGenerationProgress(this);
        }

        public void Update()
        {
            Population.Update();
            Fitness.Update();
            Statistics.Update();
            Parameters.Update();
            HyperHeuristic.Update();
            Termination.Update();
        }

        public async Task EvolveOneGeneration() => Population.Individuals = await HyperHeuristic.Generate();

        public async Task Evolve()
        {
            CancellationTokenSource = new CancellationTokenSource();
            var token = CancellationTokenSource.Token;
            if (!IsInitialized) Initialize();

            while (!Termination.ShouldTerminate())
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
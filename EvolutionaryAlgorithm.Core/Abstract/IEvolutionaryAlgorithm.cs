using System;
using System.Threading;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IIndividualStorage<TIndividual, TGeneStructure, TGene> Storage { set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public ITermination<TIndividual, TGeneStructure, TGene> Termination { get; set; }
        public IParameters<TIndividual, TGeneStructure, TGene> Parameters { get; set; }
        public IPopulation<TIndividual, TGeneStructure, TGene> Population { get; set; }
        public IFitness<TIndividual, TGeneStructure, TGene> Fitness { get; set; }
        public IMutator<TIndividual, TGeneStructure, TGene> Mutator { get; set; }
        public IGenerationFilter<TIndividual, TGeneStructure, TGene> GenerationFilter { get; set; }
        public TIndividual Best { get; }
        public IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> Statistics { get; set; }
        public Task EvolveOneGeneration();
        public void Cancel() => CancellationTokenSource.Cancel();
        public bool IsInitialized { get; protected set; }

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
            if (Mutator == null)
                throw new EvolutionaryAlgorithmArgumentException(
                    $"IEvolutionaryAlgorithm.{nameof(Mutator)} cannot by null");
            if (GenerationFilter == null)
                throw new EvolutionaryAlgorithmArgumentException(
                    $"IEvolutionaryAlgorithm.{nameof(GenerationFilter)} cannot by null");
            if (Termination == null)
                throw new EvolutionaryAlgorithmArgumentException(
                    $"IEvolutionaryAlgorithm.{nameof(Termination)} cannot by null");
        }

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Initialize()
        {
            IsInitialized = true;
            ArgumentValidation();

            Population.Initialize();
            Fitness.Initialize();
            Population.Individuals.ForEach(i => i.Fitness = Fitness.Evaluate(i));
            Statistics.Initialize();
            Parameters.Initialize();
            Mutator.Initialize();
            GenerationFilter.Initialize();
            Termination.Initialize();

            Storage = new IndividualStorage<TIndividual, TGeneStructure, TGene>(Population[0]);
            return this;
        }

        private void Update()
        {
            Population.Update();
            Fitness.Update();
            Statistics.Update();
            Parameters.Update();
            Mutator.Update();
            GenerationFilter.Update();
            Termination.Update();
        }


        public async Task Evolve()
        {
            CancellationTokenSource = new CancellationTokenSource();
            var token = CancellationTokenSource.Token;
            if (!IsInitialized) Initialize();

            while (!Termination.ShouldTerminate())
            {
                await EvolveOneGeneration();
                Update();
                if (!token.IsCancellationRequested) continue;
                Statistics.Finish();
                return;
            }

            Statistics.Finish();
        }
    }
}
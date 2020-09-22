using System;
using System.Threading;
using System.Threading.Tasks;
using Dasync.Collections;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public static EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Construct =>
            new EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>();

        private IFitness<TIndividual, TGeneStructure, TGene> _fitness;
        private IPopulation<TIndividual, TGeneStructure, TGene> _population;
        private IMutator<TIndividual, TGeneStructure, TGene> _mutator;
        private IGenerationFilter<TIndividual, TGeneStructure, TGene> _generationFilter;
        private IParameters<TIndividual, TGeneStructure, TGene> _parameters;
        private IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> _statistics;
        private ITermination<TIndividual, TGeneStructure, TGene> _termination;

        public EvolutionaryAlgorithm() =>
            CancellationTokenSource = new CancellationTokenSource();

        public TIndividual Best => Population.Best;
        bool IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>.IsInitialized { get; set; }

        public IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> Statistics
        {
            get => _statistics;
            set
            {
                _statistics = value;
                _statistics.Algorithm = this;
            }
        }

        public IIndividualStorage<TIndividual, TGeneStructure, TGene> Storage { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public ITermination<TIndividual, TGeneStructure, TGene> Termination
        {
            get => _termination;
            set
            {
                _termination = value;
                _termination.Algorithm = this;
            }
        }

        public IParameters<TIndividual, TGeneStructure, TGene> Parameters
        {
            get => _parameters;
            set
            {
                _parameters = value;
                _parameters.Algorithm = this;
            }
        }

        public IPopulation<TIndividual, TGeneStructure, TGene> Population
        {
            get => _population;
            set
            {
                _population = value;
                _population.Algorithm = this;
            }
        }

        public IFitness<TIndividual, TGeneStructure, TGene> Fitness
        {
            get => _fitness;
            set
            {
                _fitness = value;
                _fitness.Algorithm = this;
            }
        }

        public IMutator<TIndividual, TGeneStructure, TGene> Mutator
        {
            get => _mutator;
            set
            {
                _mutator = value;
                _mutator.Algorithm = this;
            }
        }

        public IGenerationFilter<TIndividual, TGeneStructure, TGene> GenerationFilter
        {
            get => _generationFilter;
            set
            {
                _generationFilter = value;
                _generationFilter.Algorithm = this;
            }
        }

        public async Task EvolveOneGeneration()
        {
            var newIndividuals = Storage.Get(Parameters.Lambda);

            await Mutator.Mutate(newIndividuals);

            await newIndividuals.ParallelForEachAsync(async i => i.Fitness = Fitness.Evaluate(i));

            var result = GenerationFilter.Filter(Population, newIndividuals);

            Population.Individuals = result.NextGeneration;
            Storage.Dump(result.Discarded);
        }
    }
}
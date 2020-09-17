using System;
using System.Threading.Tasks;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IPopulation<TIndividual, TGeneStructure, TGene> Population { get; set; }
        public IFitness<TIndividual, TGeneStructure, TGene> Fitness { get; set; }
        public IMutator<TIndividual, TGeneStructure, TGene> Mutator { get; set; }
        public IGenerationFilter<TIndividual, TGeneStructure, TGene> GenerationFilter { get; set; }
        public TIndividual Best { get; }
        public IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> Statistics { get; set; }
        
        void Initiate()
        {
            if (Population != null && Fitness != null)
                Population.Individuals.ForEach(i => i.Fitness = Fitness.Evaluate(i));
        }

        IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> UsingPopulation(
            IPopulation<TIndividual, TGeneStructure, TGene> initialPopulation)
        {
            Population = initialPopulation;
            Initiate();
            return this;
        }

        IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> UsingStatistics(
            IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> statistics)
        {
            Statistics = statistics;
            return this;
        }

        IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> UsingFitness(IFitness<TIndividual, TGeneStructure, TGene> fitness)
        {
            Fitness = fitness;
            Fitness.Algorithm = this;
            Initiate();
            return this;
        }

        IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> UsingMutator(
            IMutator<TIndividual, TGeneStructure, TGene> mutator)
        {
            Mutator = mutator;
            Mutator.Algorithm = this;
            return this;
        }

        IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> UsingGenerationFilter(
            IGenerationFilter<TIndividual, TGeneStructure, TGene> generationFilter)
        {
            GenerationFilter = generationFilter;
            GenerationFilter.Algorithm = this;
            return this;
        }

        Task EvolveUntil(ITermination<TIndividual, TGeneStructure, TGene> termination);
    }
}
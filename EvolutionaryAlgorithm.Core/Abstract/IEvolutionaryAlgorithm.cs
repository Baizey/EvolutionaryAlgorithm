using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IEvolutionaryAlgorithm<TGeneStructure, TGene> where TGeneStructure : ICloneable
    {
        public IPopulation<TGeneStructure, TGene> Population { get; set; }
        public IFitness<TGeneStructure, TGene> Fitness { get; set; }
        public IMutator<TGeneStructure, TGene> Mutator { get; set; }
        public IGenerationFilter<TGeneStructure, TGene> GenerationFilter { get; set; }
        public IIndividual<TGeneStructure, TGene> Best { get; }
        public IEvolutionaryStatistics<TGeneStructure, TGene> Statistics { get; set; }

        void Initiate()
        {
            if (Population != null && Fitness != null)
                Population.Individuals.ForEach(i => i.Fitness = Fitness.Evaluate(i));
        }

        IEvolutionaryAlgorithm<TGeneStructure, TGene> UsingPopulation(
            IPopulation<TGeneStructure, TGene> initialPopulation)
        {
            Population = initialPopulation;
            Initiate();
            return this;
        }

        IEvolutionaryAlgorithm<TGeneStructure, TGene> UsingStatistics(
            IEvolutionaryStatistics<TGeneStructure, TGene> statistics)
        {
            Statistics = statistics;
            return this;
        }

        IEvolutionaryAlgorithm<TGeneStructure, TGene> UsingFitness(IFitness<TGeneStructure, TGene> fitness)
        {
            Fitness = fitness;
            Initiate();
            return this;
        }

        IEvolutionaryAlgorithm<TGeneStructure, TGene> UsingMutator(IMutator<TGeneStructure, TGene> mutator)
        {
            Mutator = mutator;
            return this;
        }

        IEvolutionaryAlgorithm<TGeneStructure, TGene> UsingGenerationFilter(
            IGenerationFilter<TGeneStructure, TGene> generationFilter)
        {
            GenerationFilter = generationFilter;
            return this;
        }

        Task Evolve();
    }
}
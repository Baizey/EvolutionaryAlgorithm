using System;
using System.Threading.Tasks;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IEvolutionaryAlgorithm<TGeneStructure, TGene> where TGeneStructure : ICloneable
    {
        public IPopulation<TGeneStructure, TGene> Population { get; set; }
        public IFitness<TGeneStructure, TGene> Fitness { get; set; }
        public IMutator<TGeneStructure, TGene> Mutator { get; set; }
        public IGenerationFilter<TGeneStructure, TGene> GenerationFilter { get; set; }
        public IIndividual<TGeneStructure, TGene> Best { get; }

        IEvolutionaryAlgorithm<TGeneStructure, TGene> UsingPopulation(IPopulation<TGeneStructure, TGene> initialPopulation)
        {
            Population = initialPopulation;
            Population.Individuals.ForEach(i => Fitness.Evaluate(i));
            return this;
        }

        IEvolutionaryAlgorithm<TGeneStructure, TGene> UsingFitness(IFitness<TGeneStructure, TGene> initialPopulation)
        {
            Fitness = initialPopulation;
            return this;
        }

        IEvolutionaryAlgorithm<TGeneStructure, TGene> UsingMutator(IMutator<TGeneStructure, TGene> initialPopulation)
        {
            Mutator = initialPopulation;
            return this;
        }

        IEvolutionaryAlgorithm<TGeneStructure, TGene> UsingGenerationFilter(IGenerationFilter<TGeneStructure, TGene> initialPopulation)
        {
            GenerationFilter = initialPopulation;
            return this;
        }

        Task Evolve();
    }
}
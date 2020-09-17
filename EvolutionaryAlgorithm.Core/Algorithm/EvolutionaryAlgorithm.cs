using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class
        EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> 
        : IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable 
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IPopulation<TIndividual, TGeneStructure, TGene> Population { get; set; }
        public IFitness<TIndividual, TGeneStructure, TGene> Fitness { get; set; }
        public IMutator<TIndividual, TGeneStructure, TGene> Mutator { get; set; }
        public IGenerationFilter<TIndividual, TGeneStructure, TGene> GenerationFilter { get; set; }
        public IIndividual<TGeneStructure, TGene> Best => Population.Best;
        public IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> Statistics { get; set; }

        public static EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Construct =>
            new EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>();

        protected EvolutionaryAlgorithm()
        {
        }

        private void EvolveOneGeneration()
        {
            var newGeneration = Mutator.Create(Population);
            newGeneration.ForEach(i => i.Fitness = Fitness.Evaluate(i));
            GenerationFilter.Filter(Population, newGeneration);
            Statistics?.Update(this);
        }

        public async Task Evolve()
        {
            Statistics?.Start(this);
            EvolveOneGeneration();
            Statistics?.Finish(this);
        }
    }
}
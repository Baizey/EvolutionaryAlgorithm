using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class EvolutionaryAlgorithm<TGeneStructure, TGene> : IEvolutionaryAlgorithm<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public IPopulation<TGeneStructure, TGene> Population { get; set; }
        public IFitness<TGeneStructure, TGene> Fitness { get; set; }
        public IMutator<TGeneStructure, TGene> Mutator { get; set; }
        public IGenerationFilter<TGeneStructure, TGene> GenerationFilter { get; set; }
        public IIndividual<TGeneStructure, TGene> Best => Population.Best;
        public IEvolutionaryStatistics<TGeneStructure, TGene> Statistics { get; set; }

        public static EvolutionaryAlgorithm<TGeneStructure, TGene> Construct =>
            new EvolutionaryAlgorithm<TGeneStructure, TGene>();

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
using System;
using System.Threading.Tasks;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IParameters<TIndividual, TGeneStructure, TGene> Parameters { get; set; }
        public IPopulation<TIndividual, TGeneStructure, TGene> Population { get; set; }
        public IFitness<TIndividual, TGeneStructure, TGene> Fitness { get; set; }
        public IMutator<TIndividual, TGeneStructure, TGene> Mutator { get; set; }
        public IGenerationFilter<TIndividual, TGeneStructure, TGene> GenerationFilter { get; set; }
        public TIndividual Best { get; }
        public IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> Statistics { get; set; }
        public Task EvolveOneGeneration();

        public async Task EvolveUntil(ITermination<TIndividual, TGeneStructure, TGene> termination)
        {
            Statistics.Start(this);

            while (!termination.ShouldTerminate(this))
                await EvolveOneGeneration();

            Statistics.Finish(this);
        }
    }
}
using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Fitness;
using EvolutionaryAlgorithm.Core.HyperHeuristic;
using EvolutionaryAlgorithm.Core.Infrastructure;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Population;
using EvolutionaryAlgorithm.Core.Statistics;
using EvolutionaryAlgorithm.Core.Terminations;

namespace EvolutionaryAlgorithm.Core.Algorithm
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
}
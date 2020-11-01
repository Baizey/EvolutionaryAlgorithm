using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.GenerationFilter;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation;
using EvolutionaryAlgorithm.Core.Infrastructure;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator
{
    public class GenerationGenerator<TIndividual, TGeneStructure, TGene>
        : IGenerationGenerator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private IndividualStorage<TIndividual, TGeneStructure, TGene> _storage;
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        public IMutator<TIndividual, TGeneStructure, TGene> Mutator { get; set; }
        public IGenerationFilter<TIndividual, TGeneStructure, TGene> Filter { get; set; }

        public TIndividual Best => Algorithm.Population.Best;

        public virtual void Initialize()
        {
            _storage = new IndividualStorage<TIndividual, TGeneStructure, TGene>(Algorithm);
            Mutator.Algorithm = Algorithm;
            Mutator.Initialize();
            Filter.Algorithm = Algorithm;
            Filter.Initialize();
        }

        public virtual void Update()
        {
            Mutator.Update();
            Filter.Update();
        }

        public virtual async Task<List<TIndividual>> Generate()
        {
            var bodies = _storage.Get(0, Algorithm.Parameters.Lambda);
            await Mutator.Mutate(bodies);

            bodies.ForEach(b => b.Fitness = Algorithm.Fitness.Evaluate(b));

            var result = Filter.Filter(bodies);
            _storage.Dump(0, result.Discarded);
            return result.NextGeneration;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Core.Abstract.MutationPhase
{
    public interface IGenerationGenerator<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IMutator<TIndividual, TGeneStructure, TGene> Mutator { get; set; }

        public IGenerationFilter<TIndividual, TGeneStructure, TGene> Filter { get; set; }

        public Task<List<TIndividual>> Generate(int amount);
    }

    public class GenerationGenerator<TIndividual, TGeneStructure, TGene>
        : IGenerationGenerator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private IIndividualStorage<TIndividual, TGeneStructure, TGene> _storage;
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        public IMutator<TIndividual, TGeneStructure, TGene> Mutator { get; set; }
        public IGenerationFilter<TIndividual, TGeneStructure, TGene> Filter { get; set; }

        public void Initialize()
        {
            _storage = new IndividualStorage<TIndividual, TGeneStructure, TGene>(Algorithm);
            Mutator.Algorithm = Algorithm;
            Mutator.Initialize();
            Filter.Algorithm = Algorithm;
            Filter.Initialize();
        }

        public void Update()
        {
            Mutator.Update();
            Filter.Update();
        }

        public async Task<List<TIndividual>> Generate(int amount)
        {
            var bodies = _storage.Get(0, amount);
            await Mutator.Mutate(bodies);

            bodies.ForEach(b => b.Fitness = Algorithm.Fitness.Evaluate(b));

            var result = await Filter.Filter(bodies);
            _storage.Dump(0, result.Discarded);
            return result.NextGeneration;
        }
    }
}
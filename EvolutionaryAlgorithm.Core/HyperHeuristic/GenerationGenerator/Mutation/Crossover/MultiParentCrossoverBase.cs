using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Crossover
{
    public abstract class MultiParentCrossoverBase<TIndividual, TGeneStructure, TGene>
        : IMutation<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        public IMultiParentSelector<TIndividual, TGeneStructure, TGene> ParentsSelector { get; }

        public MultiParentCrossoverBase(IMultiParentSelector<TIndividual, TGeneStructure, TGene> parentsSelector) =>
            ParentsSelector = parentsSelector;

        public void Initialize()
        {
            ParentsSelector.Initialize();
            ParentsSelector.Algorithm = Algorithm;
        }

        public void Update() => ParentsSelector.Update();

        public abstract Task Crossover(TIndividual child, List<TIndividual> parents);

        public async Task Mutate(int index, TIndividual child) =>
            await Crossover(child, ParentsSelector.Select(index));
    }
}
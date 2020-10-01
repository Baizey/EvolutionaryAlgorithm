using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase.Helpers;

namespace EvolutionaryAlgorithm.Core.Algorithm.Crossover
{
    public abstract class SingleParentCrossoverBase<TIndividual, TGeneStructure, TGene>
        : IMutation<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        public ISingleParentSelector<TIndividual, TGeneStructure, TGene> ParentSelector { get; set; }

        public SingleParentCrossoverBase(ISingleParentSelector<TIndividual, TGeneStructure, TGene> parentsSelector) =>
            ParentSelector = parentsSelector;

        public virtual void Initialize()
        {
            ParentSelector.Algorithm = Algorithm;
            ParentSelector.Initialize();
        }

        public virtual void Update() => ParentSelector.Update();

        public abstract Task Crossover(int index, TIndividual child, TIndividual parent);

        public async Task Mutate(int index, TIndividual child) =>
            await Crossover(index, child, ParentSelector.Select(index, Algorithm.Population));
    }
}
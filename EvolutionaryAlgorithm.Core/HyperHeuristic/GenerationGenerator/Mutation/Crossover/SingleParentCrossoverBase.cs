using System;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Crossover
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

        public abstract void Crossover(int index, TIndividual child, TIndividual parent);
        
        public void Mutate(int index, TIndividual child) => 
            Crossover(index, child, ParentSelector.Select(index));
    }
}
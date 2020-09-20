using System;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Crossover
{
    public abstract class SingleParentCrossoverBase<TIndividual, TGeneStructure, TGene>
        : IMutation<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        private IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> _algorithm;

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm
        {
            get => _algorithm;
            set
            {
                _algorithm = value;
                ParentSelector.Algorithm = Algorithm;
            }
        }

        private IParentSelector<TIndividual, TGeneStructure, TGene> ParentSelector { get; }

        public SingleParentCrossoverBase(IParentSelector<TIndividual, TGeneStructure, TGene> parentsSelector) =>
            ParentSelector = parentsSelector;

        public void Initialize() => ParentSelector.Initialize();

        public abstract void Crossover(TIndividual child, TIndividual parent);

        public void Mutate(int index, TIndividual child) =>
            Crossover(child, ParentSelector.Select(Algorithm.Population));
    }
}
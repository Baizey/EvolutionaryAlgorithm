using System;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Crossover
{
    public class CloneParent<TIndividual, TGeneStructure, TGene> 
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
                ParentsSelector.Algorithm = Algorithm;
            }
        }

        public IParentSelector<TIndividual, TGeneStructure, TGene> ParentsSelector { get; set; }

        public CloneParent(IParentSelector<TIndividual, TGeneStructure, TGene> parentsSelector) =>
            ParentsSelector = parentsSelector;

        public void Initialize() => ParentsSelector.Initialize();

        public void Mutate(TIndividual child) => ParentsSelector.Select(Algorithm.Population).CloneGenesTo(child);
    }
}
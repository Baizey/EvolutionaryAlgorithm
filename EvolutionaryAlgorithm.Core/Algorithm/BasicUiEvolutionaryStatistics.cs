using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class BasicUiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : BasicEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>,
            IUiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<TIndividual> History { get; } = new List<TIndividual>();

        public new void Start(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo)
        {
            base.Start(algo);
            History.Add(Best);
        }

        public new void Update(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo)
        {
            base.Start(algo);
            History.Add((TIndividual) algo.Best.Clone());
        }
    }
}
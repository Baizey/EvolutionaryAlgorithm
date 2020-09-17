using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public TIndividual Best { get; }
        public int StagnantGeneration { get; }
        public long Generations { get; }

        void Start(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo);
        void Update(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo);
        void Finish(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo);
    }

    public interface IUiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<TIndividual> History { get; }
    }
}
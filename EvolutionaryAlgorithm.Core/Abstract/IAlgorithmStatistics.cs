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
        public List<TIndividual> Generations { get; }
        public TIndividual Best { get; }
        public TIndividual Last { get; }
        public int StagnantGeneration { get; }
        public int CurrentGeneration { get; }

        void Start(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo);
        void Update(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo);
        void Finish(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo);
    }
}
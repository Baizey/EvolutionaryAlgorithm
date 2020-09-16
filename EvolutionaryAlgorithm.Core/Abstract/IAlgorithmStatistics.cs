using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IEvolutionaryStatistics<TGeneStructure, TGene> where TGeneStructure : ICloneable
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public List<IIndividual<TGeneStructure, TGene>> Generations { get; }
        public IIndividual<TGeneStructure, TGene> Best { get; }
        public IIndividual<TGeneStructure, TGene> Last { get; }
        public int StagnantGeneration { get; }
        public int CurrentGeneration { get; }

        void Start(IEvolutionaryAlgorithm<TGeneStructure, TGene> algo);
        void Update(IEvolutionaryAlgorithm<TGeneStructure, TGene> algo);
        void Finish(IEvolutionaryAlgorithm<TGeneStructure, TGene> algo);
    }
}
using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Core.Abstract.Infrastructure
{
    public interface IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public TIndividual Best { get; }
        public TIndividual Current { get; }
        public TIndividual Previous { get; }
        public long StagnantGeneration { get; }
        public long Generations { get; }
        TimeSpan RunTime => EndTime - StartTime;
        void Finish();
    }
}
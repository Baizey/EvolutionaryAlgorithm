using System;
using EvolutionaryAlgorithm.Core.Infrastructure;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.Statistics
{
    public interface IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public DateTime StartTime { get; }
        public DateTime? EndTime { get; }
        public TIndividual Best { get; }
        public TIndividual Current { get; }
        public TIndividual Previous { get; }

        public bool ImprovedFitness => Current.Fitness > Previous.Fitness;
        public long StagnantGeneration { get; }
        public long Generations { get; }
        TimeSpan RunTime => (EndTime ?? DateTime.Now) - StartTime;
        void Finish();
    }
}
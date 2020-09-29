using System;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Core.Algorithm.Terminations
{
    public class TimeoutTermination<TIndividual, TGeneStructure, TGene>
        : ITermination<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        private readonly TimeSpan _limit;

        public TimeoutTermination(TimeSpan limit) => _limit = limit;

        public bool ShouldTerminate() => Algorithm.Statistics.RunTime >= _limit;

        public void Initialize()
        {
        }

        public void Update()
        {
        }
    }
}
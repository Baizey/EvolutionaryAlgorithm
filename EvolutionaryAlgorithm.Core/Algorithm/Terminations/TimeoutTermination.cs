using System;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Terminations
{
    public class TimeoutTermination<TIndividual, TGeneStructure, TGene>
        : ITermination<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private readonly TimeSpan _limit;

        public TimeoutTermination(TimeSpan limit) => _limit = limit;

        public bool IsDone(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algorithm) =>
            algorithm.Statistics.RunTime >= _limit;
    }
}
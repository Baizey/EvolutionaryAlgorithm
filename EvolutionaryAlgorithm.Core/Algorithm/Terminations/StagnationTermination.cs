using System;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Terminations
{
    public class StagnationTermination<TIndividual, TGeneStructure, TGene>
        : ITermination<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        private readonly Func<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>, double> _limit;

        public StagnationTermination(double limit) => _limit = _ => limit;

        public StagnationTermination(Func<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>, double> limit) =>
            _limit = limit;

        public bool ShouldTerminate() =>
            Algorithm.Statistics.StagnantGeneration >= _limit.Invoke(Algorithm);

        public void Initialize()
        {
        }

        public void Update()
        {
        }
    }
}
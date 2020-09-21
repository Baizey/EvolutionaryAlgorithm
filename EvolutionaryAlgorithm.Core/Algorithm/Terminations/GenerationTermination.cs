using System;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Terminations
{
    public class GenerationTermination<TIndividual, TGeneStructure, TGene>
        : ITermination<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        private readonly Func<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>, long> _limit;

        public GenerationTermination(long limit) => _limit = _ => limit;

        public GenerationTermination(Func<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>, long> limit) =>
            _limit = limit;

        public bool ShouldTerminate() => Algorithm.Statistics.Generations >= _limit.Invoke(Algorithm);

        public void Initialize()
        {
        }

        public void Update()
        {
        }
    }
}
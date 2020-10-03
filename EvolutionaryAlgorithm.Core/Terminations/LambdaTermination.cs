using System;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.Terminations
{
    public class LambdaTermination<TIndividual, TGeneStructure, TGene>
        : ITermination<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        private readonly Func<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>, bool> _function;

        public LambdaTermination(Func<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>, bool> limit) =>
            _function = limit;

        public bool ShouldTerminate() => _function.Invoke(Algorithm);
    }
}
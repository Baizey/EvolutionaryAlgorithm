using System;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.Infrastructure
{
    public interface IKeepsReference<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
    }

    public interface IInitializes
    {
        public void Initialize();
    }

    public interface IUpdates
    {
        public void Update();
    }

    public interface ICopyTo<in T>
    {
        public void CopyTo(T other);
    }

    public interface IEvolutionary<TIndividual, TGeneStructure, TGene>
        : IKeepsReference<TIndividual, TGeneStructure, TGene>, IInitializes, IUpdates
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
    }
}
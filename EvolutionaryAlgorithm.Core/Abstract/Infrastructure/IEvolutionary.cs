﻿using System;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Core.Abstract.Infrastructure
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

    public interface IEvolutionary<TIndividual, TGeneStructure, TGene>
        : IKeepsReference<TIndividual, TGeneStructure, TGene>, IInitializes, IUpdates
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
    }
}
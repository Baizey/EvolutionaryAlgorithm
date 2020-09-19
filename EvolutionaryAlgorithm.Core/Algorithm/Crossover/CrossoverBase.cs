﻿using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Crossover
{
    public abstract class
        CrossoverBase<TIndividual, TGeneStructure, TGene> : IMutation<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        private IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> _algorithm;

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm
        {
            get => _algorithm;
            set
            {
                _algorithm = value;
                ParentsSelector.Algorithm = Algorithm;
            }
        }

        private IMultiParentSelector<TIndividual, TGeneStructure, TGene> ParentsSelector { get; }

        public CrossoverBase(IMultiParentSelector<TIndividual, TGeneStructure, TGene> parentsSelector) =>
            ParentsSelector = parentsSelector;

        public void Initialize() => ParentsSelector.Initialize();

        public abstract void Crossover(TIndividual child, List<TIndividual> parents);

        public void Mutate(TIndividual child) => Crossover(child, ParentsSelector.Select(Algorithm.Population));
    }
}
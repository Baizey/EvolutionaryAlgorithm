﻿using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IParentSelector<TIndividual, TGeneStructure, TGene> 
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        public TIndividual Select(List<TIndividual> population);
    }
}
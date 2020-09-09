﻿using System;
using EvolutionaryAlgorithm.Core;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class NoParentSelector<T> : IParentSelector<T> where T : ICloneable
    {
        public IIndividual<T> Select(IPopulation<T> population) => null;
    }
}
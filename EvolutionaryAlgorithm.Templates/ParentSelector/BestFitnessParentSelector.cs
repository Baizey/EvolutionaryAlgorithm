﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class BestFitnessParentSelector : IBitParentSelector
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public IBitIndividual Select(IPopulation<IBitIndividual, BitArray, bool> population) =>
            population.Best;
    }
}
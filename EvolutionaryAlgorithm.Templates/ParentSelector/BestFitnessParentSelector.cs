﻿using System.Linq;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class BestFitnessParentSelector : IBitParentSelector
    {
        public IBitIndividual Select(IBitPopulation population) =>
            (IBitIndividual) population.Individuals.Aggregate((a, b) => a.Fitness > b.Fitness ? a : b);
    }
}
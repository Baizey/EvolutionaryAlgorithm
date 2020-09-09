using System;
using System.Collections;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Template.Population
{
    public class BitPopulation : Population<BitArray>
    {
        public BitPopulation(List<IIndividual<BitArray>> value) : base(value)
        {
        }

        public BitPopulation(int size, Func<IIndividual<BitArray>> generator) : base(size, generator)
        {
        }
    }
}
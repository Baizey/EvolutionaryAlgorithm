using System;
using System.Collections.Generic;
using GeneticAlgorithm.Gene;
using GeneticAlgorithm.Individual;

namespace GeneticAlgorithm.Population
{
    public abstract class Population<T> : AbstractPopulation<Individual<Gene<T>>>
    {
        public Population(List<Individual<Gene<T>>> value) : base(value)
        {
        }

        public Population(Func<List<Individual<Gene<T>>>> generator) : base(generator)
        {
        }

        public Population(int size, Func<Individual<Gene<T>>> generator) : base(size, generator)
        {
        }
    }
}
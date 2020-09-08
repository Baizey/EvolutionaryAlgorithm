using System;
using System.Collections.Generic;
using GeneticAlgorithm.Individual;

namespace GeneticAlgorithm.Population
{
    public abstract class ComplexPopulation<T> : AbstractPopulation<ComplexIndividual<T>>
    {
        public ComplexPopulation(List<ComplexIndividual<T>> value) : base(value)
        {
        }

        public ComplexPopulation(Func<List<ComplexIndividual<T>>> generator) : base(generator)
        {
        }

        public ComplexPopulation(int size, Func<ComplexIndividual<T>> generator) : base(size, generator)
        {
        }
    }
}
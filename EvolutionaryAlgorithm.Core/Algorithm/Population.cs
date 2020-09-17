using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class Population<TIndividual, TGeneStructure, TGene> : IPopulation<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public int Count => Individuals.Count;
        public List<TIndividual> Individuals { get; set; }

        public TIndividual Best =>
            Individuals.Aggregate((a, b) => a.Fitness > b.Fitness ? a : b);

        public Population(List<TIndividual> value) => Individuals = value;

        public object Clone() =>
            new Population<TIndividual, TGeneStructure, TGene>(Individuals
                .Select(i => (TIndividual) i.Clone()).ToList());

        public IEnumerator<TIndividual> GetEnumerator() => Individuals.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Individuals.GetEnumerator();
        public override string ToString() => Individuals.ToString();
    }
}
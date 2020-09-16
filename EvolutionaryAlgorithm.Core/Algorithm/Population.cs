using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class Population<TGeneStructure, TGene> : IPopulation<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public int Count => Individuals.Count;
        public int Generation { get; set; } = 0;
        public List<IIndividual<TGeneStructure, TGene>> Individuals { get; set; }

        public IIndividual<TGeneStructure, TGene> Best =>
            Individuals.Aggregate((a, b) => a.Fitness > b.Fitness ? a : b);

        public Population(List<IIndividual<TGeneStructure, TGene>> value) => Individuals = value;

        public object Clone() =>
            new Population<TGeneStructure, TGene>(Individuals
                .Select(i => (IIndividual<TGeneStructure, TGene>) i.Clone()).ToList());

        public IEnumerator<IIndividual<TGeneStructure, TGene>> GetEnumerator() => Individuals.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Individuals.GetEnumerator();
        public override string ToString() => Individuals.ToString();
    }
}
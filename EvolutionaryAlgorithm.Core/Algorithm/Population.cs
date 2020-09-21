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
        private readonly Func<int, TIndividual> _generator;
        private TIndividual _best;
        private List<TIndividual> _individuals;
        public int Count => Individuals.Count;

        public List<TIndividual> Individuals
        {
            get => _individuals;
            set
            {
                _individuals = value;
                _best = default;
            }
        }

        public TIndividual Best => _best ??= Individuals.Aggregate((a, b) => a.Fitness > b.Fitness ? a : b);

        public Population(Func<int, TIndividual> value) => _generator = value;

        public Population(IPopulation<TIndividual, TGeneStructure, TGene> other) =>
            Individuals = other.Individuals.Select(i => (TIndividual) i.Clone()).ToList();

        public object Clone() => new Population<TIndividual, TGeneStructure, TGene>(this);

        public IEnumerator<TIndividual> GetEnumerator() => Individuals.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Individuals.GetEnumerator();
        public override string ToString() => Individuals.ToString();
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public void Initialize() => Individuals = Enumerable
            .Range(0, Algorithm.Parameters.Mu)
            .Select(_ => _generator.Invoke(Algorithm.Parameters.GeneCount))
            .ToList();

        public void Update()
        {
        }
    }
}
using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.Infrastructure
{
    public class IndividualStorage<TIndividual, TGeneStructure, TGene>
        : IIndividualStorage<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private readonly Dictionary<int, List<TIndividual>> _reserves = new Dictionary<int, List<TIndividual>>();
        private readonly TIndividual _example;

        public IndividualStorage(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> example)
            : this(example.Population)
        {
        }

        public IndividualStorage(IPopulation<TIndividual, TGeneStructure, TGene> example)
            : this(example[0])
        {
        }

        public IndividualStorage(TIndividual example) => _example = example;

        private void Refill(int key, int amount)
        {
            if (!_reserves.ContainsKey(key))
                _reserves[key] = new List<TIndividual>(amount);
            var missing = amount - _reserves[key].Count;
            for (var i = 0; i < missing; i++)
                _reserves[key].Add((TIndividual) _example.Clone());
        }

        public List<TIndividual> Get(int key, int amount)
        {
            Refill(key, amount);
            if (_reserves[key].Count == amount)
            {
                var used = _reserves[key];
                _reserves[key] = null;
                return used;
            }
            else
            {
                var used = _reserves[key].GetRange(0, amount);
                _reserves[key] = _reserves[key].GetRange(amount, _reserves.Count - amount);
                return used;
            }
        }

        public void Dump(int key, List<TIndividual> bodies)
        {
            if (_reserves[key] == null)
                _reserves[key] = bodies;
            else
                _reserves[key].AddRange(bodies);
        }
    }
}
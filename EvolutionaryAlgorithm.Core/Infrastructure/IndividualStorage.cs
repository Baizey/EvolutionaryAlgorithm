using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.Infrastructure
{
    public class IndividualStorage<TIndividual, TGeneStructure, TGene>
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
            var reserves = _reserves.TryGetValue(key, out var list)
                ? list
                : new List<TIndividual>(amount);

            var missing = amount - reserves.Count;
            for (var i = 0; i < missing; i++)
                reserves.Add((TIndividual) _example.Clone());
            _reserves[key] = reserves;
        }

        public List<TIndividual> Get(int key, int amount)
        {
            Refill(key, amount);
            if (_reserves[key].Count == amount)
            {
                var used = _reserves[key];
                _reserves.Remove(key);
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
            if (_reserves.ContainsKey(key))
                _reserves[key].AddRange(bodies);
            else
                _reserves[key] = bodies;
        }
    }
}